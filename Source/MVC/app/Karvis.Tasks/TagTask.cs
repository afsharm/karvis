using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Karvis.Domain.Dto;
using Karvis.Domain.Queries;
using Karvis.Domain.Tasks;

namespace Karvis.Tasks
{
   public class TagTask : ITagTask
   {
       private readonly IJobTask _jobTask;

       public TagTask(IJobTask jobTask)
       {
           _jobTask = jobTask;
       }

       #region Implementation of ITagTask

       public IList<TagDto> GetAllTags()
       {
           var tags =  _jobTask.GetQueryable().GetAllActiveTags().ToList();

           return tags;
       }

       #endregion
    }
}
