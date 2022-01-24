using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day02
{
    internal class DayClass
    {
        List<List<int>> _data = new();
        public DayClass()
        {
            LoadData();
        }

        public void Part1()
        {
            int chkSum = 0;

            foreach (List<int> values in _data)
            {
                chkSum += values.Max() - values.Min();
            }

            Console.WriteLine("Part1: {0}", chkSum);
        }

        public void Part2()
        {
            int chkSum = 0;

            foreach (List<int> values in _data)
            {
                chkSum += (from item1 in values
                           from item2 in values
                           where item1 > item2 && (item1 / item2) * item2 == item1
                           select item1 / item2).First();
            }

            Console.WriteLine("Part2: {0}", chkSum);
        }

        private void LoadData()
        {
            string inputFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\input.txt";

            if (File.Exists(inputFile))
            {
                string? line;
                StreamReader file = new StreamReader(inputFile);
                while ((line = file.ReadLine()) != null)
                {
                    string[] parts = line.Replace('\t', ' ').Split(' '); ;
                    List<int> values = new();
                    foreach (string value in parts)
                    {
                        values.Add(int.Parse(value));
                    }
                    _data.Add(values);
                }

                file.Close();
            }
        }

    }
}
