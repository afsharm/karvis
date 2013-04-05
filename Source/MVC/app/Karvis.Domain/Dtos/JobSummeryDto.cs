using System;

namespace Karvis.Domain.Dto
{
    public class JobSummeryDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Tag { get; set; }

        public DateTime? AddedDate { get; set; }

        public int VisitCount { get; set; }

        public AdSource Source { get; set; }
    }
}