using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Karvis.Domain.Dto;
using Karvis.Domain.Queries;
using Karvis.Domain.Tasks;
using Karvis.Domain.ViewModels;

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

       public IList<TagCloudViewModel> GetTagCloud()
       {
           var tags = GetAllTags();
           var tagCloudList = tags.Select(x => new TagCloudViewModel()
                                                   {
                                                       TagName = x.TagName,
                                                       RepeatCount = tags.Count(z => z.TagName == x.TagName)
                                                   }).Distinct(new TagComparer()).ToList();
         
           return tagCloudList;



       }

       #endregion
    }
    public class TagComparer : IEqualityComparer<TagCloudViewModel>
    {
        #region Implementation of IEqualityComparer<in TagCloudViewModel>

        public bool Equals(TagCloudViewModel x, TagCloudViewModel y)
        {
            return x.TagName == y.TagName;
        }

        public int GetHashCode(TagCloudViewModel obj)
        {
            return obj.TagName.GetHashCode();
        }

        #endregion
    }
}
