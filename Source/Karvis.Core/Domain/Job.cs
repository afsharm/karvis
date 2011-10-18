using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iesi.Collections.Generic;
using Iesi.Collections;
using System.Collections;
using Fardis;

namespace Karvis.Core
{
    public class Job : Entity
    {
        public Job()
        {
        }

        public virtual string Title { set; get; }
        public virtual string Description { set; get; }
        public virtual int VisitCount { set; get; }
        public virtual int FeedCount { set; get; }
        public virtual string Tag { set; get; }
        public virtual DateTime? DateAdded { set; get; }
        public virtual DateTime? OriginalDate { set; get; }
        public virtual string Url { set; get; }
        public virtual string Emails { get; set; }
        public virtual AdSource AdSource { set; get; }
        public virtual bool IsActive { set; get; }

        public virtual string DateAddedPersian
        {
            get
            {
                IDateTimeHelper dateTimeHelper = new DateTimeHelper();
                return dateTimeHelper.ConvertToPersianDatePersianDigit(DateAdded);
            }
        }

        public virtual int PreSavedJobId { get; set; }

        /// <summary>
        /// a utility property to help extracting items. this property help to prevent duplicating.
        /// </summary>
        public virtual string ExtractionText { set; get; }

        public virtual string AdSourceDescription
        {
            get { return GetAdSourceDescription(AdSource); }
        }

        public static string GetAdSourceDescription(AdSource siteSource)
        {
            switch (siteSource)
            {
                case AdSource.rahnama_com:
                    return "نیازمندی‌های ‌همشهری";
                case AdSource.irantalent_com:
                    return "IranTalent.com";
                case AdSource.Email:
                    return "ایمیل";
                case AdSource.developercenter_ir:
                    return "DeveloperCenter.ir";
                case AdSource.Misc:
                    return "متفرقه";
                case AdSource.All:
                    return "مهم نیست";
                case AdSource.karvis_ir:
                    return "سایت کارویس";
                case AdSource.itjobs_ir:
                    return "ITJobs.ir";
                case AdSource.agahi_ir:
                    return "Agahi.ir";
                case AdSource.istgah_com:
                    return "Istgah.com";
                case AdSource.nofa_ir:
                    return "Nofa.ir";
                case AdSource.unp_ir:
                    return "UNP.ir";
                case AdSource.banki_ir:
                    return "Banki.ir";
                case AdSource.estekhtam_com:
                    return "Estekhtam.com";
                case AdSource.barnamenevis_org:
                    return "barnamenevis.org";
                default:
                    return "N/A";
            }
        }
    }
}
