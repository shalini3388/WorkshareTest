using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshare.domain
{
    public class Log
    {
        public string Date { get; set; } 	
        public DateTime DateTime { get; set; }
        public string Url { get; set; } 	
        public string Method { get; set; } 	
        public string IP { get; set; }
        public int StatusCode { get; set; }

        public string Line { get; set; }
        public bool Error { get; set; }

        public static Log Build(string line)
        {
            var items = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            return new Log(items, line);
        }

        public Log(string[] items, string line)
        {
            try
            {
                Date = items[0];
                DateTime = DateTime.Parse(items[0] + " " + items[1]);
                Url = items[4];
                IP = items[8];
                Method = items[3];
                StatusCode = Convert.ToInt32(items[11]);
                Error = false;
            }
            catch (Exception ex)
            {
                Line = line;
                Error = true;
                Console.WriteLine("{0}Parser error can not parse {1}", Environment.NewLine, line);


            }
        }



    }
}
