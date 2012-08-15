using System.Collections.Generic;
using System.ComponentModel;

namespace Karvis.Domain.ViewModels
{
    public class JobViewModel
    {
        public IList<JobSummery> Jobs { get; set; }

        public int TotalJobsCount { get; set; }
    }

    public class JobSummery
    {
        public string Id { get; set; }

        [DisplayName("عنوان")]
        public string Title { get; set; }

        [DisplayName("تگ")]
        public string Tag { get; set; }

        [DisplayName("تاریخ ثبت")]
        public string AddedDate { get; set; }

        [DisplayName("تعداد مشاهده")]
        public string VisitsCount { get; set; }

        [DisplayName("منبع")]
        public AdSource AdSource { get; set; }

        public  string AdSourceDescription
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