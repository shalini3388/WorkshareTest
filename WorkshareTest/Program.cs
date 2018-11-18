using System;
using System.Collections.Generic;
using System.Configuration;
using Workshare.services;

namespace WorkshareTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = ConfigurationManager.AppSettings["SiteLogPath"];

            var summary = new FileReader().GetSummary(path);

            Console.WriteLine("Most used IP : {0}", summary.MostUsedIp);
            Console.WriteLine("Most used Url : {0}", summary.MostUsedUrl);
            Console.WriteLine("Date with heaviest traffic : {0}", summary.DayWithHeaviestTraffic);
            
            foreach(var status in summary.StatusCodeFrequency)
            {
                Console.WriteLine("Frequency for {0} : {1}", status.StatusCode, status.Frequency);
            }

            Console.ReadLine();

        }
    }
}
