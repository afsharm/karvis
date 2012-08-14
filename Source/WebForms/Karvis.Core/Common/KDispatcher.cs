using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;

namespace Karvis.Core
{
    public class KDispatcher : IKDispatcher
    {
        string _host;
        string _userName;
        string _password;
        int _port;
        SmtpClient _client;
        int _sentCount;
        int _failCount;

        public KDispatcher()
        {
            //This empty constructor does not help anything. 
            //This has been added to help unit tests only.
        }

        public KDispatcher(string host, string port, string userName, string password)
        {

            _host = host;
            _port = Convert.ToInt32(port);
            _userName = userName;
            _password = password;

            _client = new SmtpClient(_host, _port);
            _client.Credentials = new NetworkCredential(_userName, _password);

            _sentCount = 0;
            _failCount = 0;
        }

        public int SentCount { get { return _sentCount; } }
        public int FailCount { get { return _failCount; } }
        public int TotalCount { get { return _sentCount + _failCount; } }

        public bool Send(MailMessage message)
        {
            bool retval = false;

            try
            {
                _client.Send(message);
                retval = true;
                _sentCount++;
            }
            catch
            {
                _failCount++;
            }

            return retval;
        }
    }
}
