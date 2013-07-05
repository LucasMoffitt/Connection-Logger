using System;
using System.Collections.Generic;
using System.Net;
using System.Timers;

namespace Connection_Logger
{
    class Program
    {
        private static List<DateTime> Outages = new List<DateTime>();

        static void Main(string[] args)
        {
            Timer checkTimer = new Timer(5000);
            checkTimer.Elapsed += checkTimer_Elapsed;
            checkTimer.Enabled = true;

            Console.WriteLine("Starting Logging...");
            Console.ReadLine();

            checkTimer.Enabled = false;
            WriteOutputToFile();
            Console.ReadLine();
        }

        private static void WriteOutputToFile()
        {
            Console.WriteLine("Starting Output...");

            using (CsvFileWriter writer = new CsvFileWriter("Output.csv"))
            {
                for (int i = 0; i < Outages.Count; i++)
                {
                    CsvRow row = new CsvRow();

                    row.Add(Outages[i].ToString());
                    writer.WriteRow(row);
                }
            }

            Console.WriteLine("Finished Writing Output");
        }

        private static void checkTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (ActiveinternetConnection())
            {
                Console.WriteLine("Active: {0}", DateTime.Now);
            }
            else
            {
                Console.WriteLine("Not Active: {0}", DateTime.Now);
                Outages.Add(DateTime.Now);
            }
        }

        private static bool ActiveinternetConnection()
        {
            try
            {
                using (WebClient client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
