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
        public string RegistredDate { get; set; }

        [DisplayName("تعداد مشاهده")]
        public string VisitsCount { get; set; }

        [DisplayName("منبع")]
        public string Source { get; set; }
    }
}