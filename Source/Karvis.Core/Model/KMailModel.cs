using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using Fardis;
using NHibernate.Linq;
using System.Net.Mail;
using System.Net;

namespace Karvis.Core
{
    public class KMailModel : IKMailModel
    {
        ISessionFactory _sessionFactory;
        NHibernateRepository<KMail> _mailRepository;
        IScheduleInfoModel _scheduleInfoModel;
        IKDispatcher _kdispatcher;
        IKGlobalModel _kglobalModel;

        public KMailModel()
        {
            _sessionFactory = NHHelper.Instance.GetSessionFactory();
            _mailRepository = new NHibernateRepository<KMail>(_sessionFactory);
            _scheduleInfoModel = new ScheduleInfoModel();
            _kglobalModel = new KGlobalModel();
            _kdispatcher = new KDispatcher(_kglobalModel.GetValue(KGlobalConstants.SmtpHost),
                _kglobalModel.GetValue(KGlobalConstants.SmtpPort),
                _kglobalModel.GetValue(KGlobalConstants.SmtpUserName),
                _kglobalModel.GetValue(KGlobalConstants.SmtpPassword));
        }

        public KMailModel(ISessionFactory sessionFactory, IScheduleInfoModel scheduleInfoModel,
            IKDispatcher kdispatcher)
        {
            _sessionFactory = sessionFactory;
            _scheduleInfoModel = scheduleInfoModel;
            _mailRepository = new NHibernateRepository<KMail>(_sessionFactory);
            _kdispatcher = kdispatcher;
        }


        public void DoSchedule()
        {
            DateTime start = DateTime.Now;

            //do the real work

            var unsent = GetUnSentItems();
            int sendSuccessCount = 0;
            int sendFailCount = 0;
            foreach (var item in unsent)
            {
                MailMessage mailMessage = ConvertToMailMessage(item);
                bool sendResult = SendMailMessage(item, mailMessage);

                if (sendResult)
                    sendSuccessCount++;
                else
                    sendFailCount++;
            }

            string scheduleResult = string.Format("{0} success try and {1} fail try", sendSuccessCount, sendFailCount);

            DateTime end = DateTime.Now;
            _scheduleInfoModel.SaveScheduleInfo(KConstants.MailSchedule, scheduleResult, start, end);
        }

        private bool SendMailMessage(KMail kmail, MailMessage mailMessage)
        {
            bool sendResult = _kdispatcher.Send(mailMessage);

            if (sendResult)
                SaveSuccess(kmail);

            IncreaseTry(kmail);

            return sendResult;
        }

        private static MailMessage ConvertToMailMessage(KMail item)
        {
            MailMessage mail = new MailMessage
            {
                From = new MailAddress(item.FromAddress, item.FromDescription, new UTF8Encoding()),
                Subject = item.Subject,
                Body = item.Description,
            };

            foreach (var receiver in item.Receivers.Split(','))
                if (!string.IsNullOrEmpty(receiver))
                    mail.To.Add(receiver);

            return mail;
        }


        public void Qeue(string subject, string description, string fromAddress, string fromDescription,
            string receivers, int relatedReferenceId)
        {
            KMail mail = new KMail
            {
                Subject = subject,
                Description = description,
                FromAddress = fromAddress,
                FromDescription = fromDescription,
                RelatedReferenceId = relatedReferenceId,
                AddDate = DateTime.Now,
                Receivers = receivers,
                IsSent = false,
                TryCounter = 0
            };

            _mailRepository.SaveOrUpdate(mail);
        }

        public IList<KMail> GetSentItems()
        {
            return _mailRepository.QueryOver().Where(item => item.IsSent).List();
        }

        public IList<KMail> GetUnSentItems()
        {
            return _mailRepository.QueryOver().Where(item => !item.IsSent).List();
        }

        public void IncreaseTry(KMail mail)
        {
            var item = _mailRepository.Load(mail.Id);
            item.TryCounter++;
            _mailRepository.SaveOrUpdate(item);
        }

        public void SaveSuccess(KMail mail)
        {
            var item = _mailRepository.Load(mail.Id);
            item.IsSent = true;
            _mailRepository.SaveOrUpdate(item);
        }
    }
}
