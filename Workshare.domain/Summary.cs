using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshare.domain
{
    public class Summary
    {
        public string MostUsedIp { get; set; }
        public string MostUsedUrl { get; set; }
        public List<StatusCodeFrequency> StatusCodeFrequency{get;set;}
        public string DayWithHeaviestTraffic { get; set; }
    }
}
