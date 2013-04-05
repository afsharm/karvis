using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Karvis.Domain;
using Karvis.Domain.Constants;
using Karvis.Domain.JobExtract;
using Karvis.Domain.Tasks;
using Karvis.Domain.ViewModels;

namespace Karvis.Tasks
{
  public class AdminTask :IAdminTask 
    {

      private readonly IExtractJobs _extractJobs;
        private readonly IFeedExtractor _feedExtractor;

        public AdminTask(IFeedExtractor feedExtractor, IExtractJobs extractJobs)
      {
          this._feedExtractor = feedExtractor;
          this._extractJobs = extractJobs;
      }

      #region Implementation of IAdminTask

      public AdminViewModel GetRawModel()
      {
          var model= new AdminViewModel {ExtractJobViewModel = GetExtractJobViewModel()};

          return model;
      }

      public AdminExtractJobResultViewModel ExtractJobs(AdSource targetSource)
      {
          List<Job> jobs = null;

          switch (targetSource)
          {
              case AdSource.rahnama_com:
              case AdSource.irantalent_com:
              case AdSource.Email:
              case AdSource.Misc:
              case AdSource.All:
              case AdSource.karvis_ir:
              case AdSource.itjobs_ir:
              case AdSource.agahi_ir:
              case AdSource.istgah_com:
              case AdSource.nofa_ir:
              case AdSource.unp_ir:
                  jobs = _extractJobs.ExtractJobs(targetSource, JobExtractorConstant.DayLimit, JobExtractorConstant.RecordLimit);
                  break;
              case AdSource.developercenter_ir:
              case AdSource.banki_ir:
              case AdSource.barnamenevis_org:
              case AdSource.estekhtam_com:
                  jobs = _feedExtractor.ExtractFeed(targetSource);
                  break;
              default:
                  throw new ApplicationException("unkown error");
            }
          return new AdminExtractJobResultViewModel()
                     {
                         Jobs=jobs
                     };
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
