using System.ComponentModel;

namespace Karvis.Domain.ViewModels
{
    public class JobDescriptionViewModel
    {
        public JobSummery JobSummery { get; set; }

        [DisplayName("شرح")]
        public string Description { get; set; }

        [DisplayName("تعداد مشاهده از طریق فید")]
        public string FeedVisitsCount { get; set; }

        [DisplayName("لینک")]
        public string Link { get; set; }
    }
}