using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshare.domain;

namespace Workshare.services
{
    public class FileReader:IFileReader
    {
        public Summary GetSummary(string path)
        {
            var logs = Read(path);
            var summary = new Summary();

            summary.MostUsedIp = (from i in logs
                                  group i by i.IP into grp
                                  orderby grp.Count() descending
                                  select grp.Key).First();

            summary.MostUsedUrl= (from i in logs
                                  group i by i.Url into grp
                                  orderby grp.Count() descending
                                  select grp.Key).First();

            summary.StatusCodeFrequency = BuildStatusCodeFrequency(logs);

            summary.DayWithHeaviestTraffic= (from i in logs
                                             group i by i.Date into grp
                                             orderby grp.Count() descending
                                             select grp.Key).First();

            return summary;
        }

        private List<StatusCodeFrequency> BuildStatusCodeFrequency(List<Log> logs)
        {
            var statuses = logs.Select(l => l.StatusCode).Distinct();
            var list = new List<StatusCodeFrequency>();

            foreach(var status in statuses)
            {
                list.Add(new StatusCodeFrequency()
                {
                    StatusCode = status,
                    Frequency = logs.Count(f => f.StatusCode == status)
                });
            }
            return list;
        }

        private  List<Log> Read(string path)
        {
            var files = Directory.GetFiles(path);
            var logs = new List<Log>();

            foreach(var file in files)
            {
                logs.AddRange(ReadFromFile(file));
            }
            return logs;
        }

        private List<Log> ReadFromFile(string path)
        {
            var list = new List<Log>();
            try
            {
                var line = string.Empty;
                using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (var reader = new StreamReader(fileStream))
                    {
                        while (!reader.EndOfStream)
                        {
                            line = reader.ReadLine();
                            var log = Log.Build(line);

                            if (!log.Error)
                            {
                                list.Add(log);
                            }
                        }
                    }
                }
            }
            catch
            {
                return new List<Log>();
            }
            return list;
        }
    }
}
