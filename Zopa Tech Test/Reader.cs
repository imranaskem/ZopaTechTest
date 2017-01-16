using System;
using System.Collections.Generic;
using System.IO;

namespace ZopaTechTest
{
    class Reader
    {
        public string Filename { get; private set; }
        public string FullPath { get; private set; }
        public List<string> RawData { get; private set; }

        public Reader(string filename)
        {
            this.Filename = filename;
            this.FullPath = Path.GetFullPath(filename);
            this.RawData = new List<string>();
            this.ReadFile();
        }

        private void ReadFile()
        {
            if (!File.Exists(this.FullPath))
            {
                Console.WriteLine();
                Console.WriteLine("File does not exist, program exiting...");                
                Environment.Exit(0);
                
            }

            using (StreamReader reader = new StreamReader(this.FullPath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    this.RawData.Add(line);                     
                }
            }            
        }

        public List<Lender> CreateLenders()
        {
            List<Lender> lenderlist = new List<Lender>();

            var count = 0;

            foreach(string line in this.RawData)
            {
                if (count != 0) // Skipping header row
                {
                    var splits = line.Split(',');
                    string name = splits[0];

                    double rate;

                    if (!double.TryParse(splits[1], out rate))
                    {
                        Console.WriteLine();
                        Console.WriteLine("File has incorrect format, program exiting...");
                        Environment.Exit(0);
                    }

                    int available;

                    if (!int.TryParse(splits[2], out available))
                    {
                        Console.WriteLine();
                        Console.WriteLine("File has incorrect format, program exiting...");
                        Environment.Exit(0);
                    }

                    lenderlist.Add(new Lender(name, rate, available));
                }

                count++;
            }
            return lenderlist;
        }

        public List<Lender> CreateData() // Creates test data, not used in solution
        {
            List<Lender> lenderlist = new List<Lender>();

            Lender Bob = new Lender("Bob", 0.075, 640);
            Lender Jane = new Lender("Jane", 0.069, 480);
            Lender Fred = new Lender("Fred", 0.071, 520);
            Lender Mary = new Lender("Mary", 0.104, 170);
            Lender John = new Lender("John", 0.081, 320);
            Lender Dave = new Lender("Dave", 0.074, 140);
            Lender Angela = new Lender("Angela", 0.071, 60);

            lenderlist.Add(Bob);
            lenderlist.Add(Jane);
            lenderlist.Add(Fred);
            lenderlist.Add(Mary);
            lenderlist.Add(John);
            lenderlist.Add(Dave);
            lenderlist.Add(Angela);

            return lenderlist;
        }
    }
}
