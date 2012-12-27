using System.Collections.Generic;
using System.ComponentModel;

namespace Karvis.Domain.ViewModels
{
    public class JobViewModel
    {
        public IList<JobSummeryViewModel> Jobs { get; set; }
        [DisplayName("تعداد مشاغل ثبت شده فعال")]
        public int TotalJobsCount { get; set; }
    }
}