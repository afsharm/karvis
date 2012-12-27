using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Karvis.Domain.ViewModels
{
   public class SearchViewModel
    {
       [Required]
       [DisplayName ("عنوان شغل")]
       public string     SearchTerm { get; set; }
       [DisplayName("تگ")]

       public string SearchTag { get; set; }
       [DisplayName ("منبع")]

       public string SearchSource { get; set; }
    }
}
