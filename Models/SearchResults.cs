using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobBoard.Engines.Models
{
    public class SearchResults
    {
        public int Count { get; set; }
        public List<Job> Jobs { get; set; }
        public SearchResults()
        {
            Jobs = new List<Job>();
        }
    }
}
