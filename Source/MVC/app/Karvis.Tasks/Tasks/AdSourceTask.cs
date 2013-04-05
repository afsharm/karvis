using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Karvis.Domain;
using Karvis.Domain.Dto;
using Karvis.Domain.Tasks;

namespace Karvis.Tasks
{
  public  class AdSourceTask : IAdSourceTask 
    {
      #region Implementation of IAdSourceTask

      public IEnumerable<AdSourceDto> GetAllAdSources()
      {
          throw new  NotImplementedException();



      }

      #endregion
    }
}
