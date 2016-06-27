using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace JobBoard.Engines.Indeed
{
    public class IndeedJob
    {
       
        public string jobtitle { get; set; }
        public string company { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string formattedlocation { get; set; }
        public string source { get; set; }
        public string date { get; set; }
        public string snippet { get; set; }
        public string url { get; set; }
        public string jobkey { get; set; }
    }
}
