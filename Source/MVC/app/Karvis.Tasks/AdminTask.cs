using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Karvis.Domain;
using Karvis.Domain.Tasks;
using Karvis.Domain.ViewModels;

namespace Karvis.Tasks
{
  public class AdminTask :IAdminTask 
    {
      #region Implementation of IAdminTask

      public AdminViewModel GetRawModel()
      {
          var model= new AdminViewModel {ExtractJobViewModel = GetExtractJobViewModel()};

          return model;
      }

      private ExtractJobViewModel GetExtractJobViewModel()
      {
          IList<SelectListItem> adSources = Enum.GetValues(typeof(AdSource))
        .Cast<AdSource>()
        .Select(x => new SelectListItem
        {
            Text = JobSummeryViewModel.GetAdSourceDescription(x),
            Value = x.ToString()
        }).Where(x=> IsAdSourceSite(x.Value)).ToList();
          

          
         return new ExtractJobViewModel()
                    {
                        SearchSource = adSources
                    };
      }

      private static bool IsAdSourceSite(string value)
      {
          var exceptions = new List<string>
                               {
                                   AdSource.All.ToString(),
                                   AdSource.Misc.ToString(),
                                   AdSource.Email.ToString(),
                                   AdSource.karvis_ir.ToString()
                               };
          return !exceptions.Contains(value);
      }

      #endregion
    }
}
