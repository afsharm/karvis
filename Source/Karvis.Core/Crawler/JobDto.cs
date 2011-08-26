using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Karvis.Core
{
    public class JobDto
    {
        public JobDto()
        {
            Id = Guid.NewGuid();

            PossibleTags = new List<string>();
            SelectedTags = new List<string>();
            PossibleEmails = new List<string>();
            SelectedEmails = new List<string>();
        }

        public Guid Id { set; get; }
        public List<string> PossibleTags { set; get; }
        public List<string> PossibleEmails { set; get; }

        public List<string> SelectedTags { set; get; }
        public List<string> SelectedEmails { set; get; }

        public string Title { set; get; }
        public string Description { set; get; }
        public string Url { set; get; }

        public Job CastToJob()
        {
            Job job = new Job()
            {
                DateAdded = DateTime.Now,
                Description = Description,
                Title = Title,
                Url = Url
            };

            SelectedTags.ToList().ForEach(s => job.Tag += string.Format("{0}, ", s));
            job.Tag = job.Tag.TrimEnd();
            if (job.Tag.EndsWith(","))
                job.Tag = job.Tag.Remove(job.Tag.Length);
            SelectedEmails.ToList().ForEach(s => job.Description += string.Format(" {0}", s));

            return job;
        }
    }
}
