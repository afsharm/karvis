using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Karvis.Domain.Dto;
using Karvis.Domain.ViewModels;

namespace Karvis.Domain.Tasks
{
   public interface ITagTask
   {
       IList<TagDto> GetAllTags();
       IList<TagCloudViewModel> GetTagCloud();
   }
}
