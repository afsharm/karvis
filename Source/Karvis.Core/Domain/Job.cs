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


        public virtual string AdSourceDescription
        {
            get
            {
                switch (AdSource)
                {
                    case AdSource.Hamshahri:
                        return "‌همشهری";
                    case AdSource.IranTalent:
                        return "IranTalent";
                    case AdSource.Email:
                        return "ایمیل";
                    case AdSource.DeveloperCenter:
                        return "DeveloperCenter";
                    case AdSource.Misc:
                        return "متفرقه";
                    case AdSource.All:
                        return "مهم نیست";
                    case AdSource.Karvis:
                        return "سایت کارویس";
                    default:
                        return "N/A";
                }
            }
        }
    }
}
