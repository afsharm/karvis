using System;

namespace Karvis.Domain.Dto
{
    public class JobSummeryDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Tag { get; set; }

        public DateTime? RegistredDate { get; set; }

        public string VisitsCount { get; set; }

        public string Source { get; set; }
    }
}