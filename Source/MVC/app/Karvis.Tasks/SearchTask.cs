using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Karvis.Domain.Tasks;
using Karvis.Domain.ViewModels;

namespace Karvis.Tasks
{
 public     class SearchTask :ISearchTask 
    {
     #region Implementation of ISearchTask

     public SearchViewModel GetRawModel()
     {
         return new SearchViewModel();
     }

     public SearchResultViewModel Search(SearchViewModel searchViewModel)
     {
         throw new NotImplementedException();
     }

     #endregion
    }
}
