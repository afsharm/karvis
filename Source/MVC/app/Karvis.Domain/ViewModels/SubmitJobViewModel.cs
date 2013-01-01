using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Karvis.Domain.ViewModels
{
    public class SubmitJobViewModel
    {
        [DisplayName("عنوان")]
        [Required(ErrorMessage = "لطفا عنوان آگهی را وارد کتید.")]
        public string Title { get; set; }

        [DisplayName("شرح")]
        [Required(ErrorMessage = "لطفا توضیحاتی درباره آگهی وارد کنید.")]
        public string Description { get; set; }

        [DisplayName("لینک")]
        public string Link { get; set; }

        [DisplayName("تگ")]
        [Required(ErrorMessage = "لطفا تگ را وارد کنید")]
        public string Tag { get; set; }
    }
}