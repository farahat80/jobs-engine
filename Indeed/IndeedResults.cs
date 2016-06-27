using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobBoard.Engines.Indeed
{
    public class IndeedResults
    {
       
        public string totalresults { get; set; }

        public List<IndeedJob> results { get; set; }
        
    }
}
