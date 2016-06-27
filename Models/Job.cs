using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobBoard.Engines.Models
{
    public class Job
    {
        public string JobTitle { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Url { get; set; }
        public string Company { get; set; }
        public string Website { get; set; }
        public string Salary { get; set; }
        public string Country { get; set; }
        public string Location { get; set; }
        public string Api { get; set; }
        public string JobKey { get; set; }

    }
}
