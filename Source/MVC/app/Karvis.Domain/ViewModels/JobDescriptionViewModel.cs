using System.Collections.Generic;
using System.ComponentModel;
using Karvis.Domain.Dto;

namespace Karvis.Domain.ViewModels
{
    public class JobDescriptionViewModel
    {
        public JobSummeryViewModel JobSummeryViewModel { get; set; }

        [DisplayName("شرح")]
        public string Description { get; set; }

        [DisplayName("تعداد مشاهده از طریق فید")]
        public string FeedVisitsCount { get; set; }

        [DisplayName("لینک")]
        public string Link { get; set; }

        [DisplayName("تگ ها")]
        public IEnumerable<TagDto> Tags { get; set; }
    }
}