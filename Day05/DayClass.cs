using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day05
{
    internal class DayClass
    {

        public DayClass()
        {
            LoadData();
        }

        public void Part1()
        {
            List<int> jumps = LoadData();
            int jumpCount = 0;
            int index = 0;
            while (index >= 0 && index < jumps.Count)
            {
                index += jumps[index]++;
                jumpCount++;
            }

            Console.WriteLine("Part1: {0}", jumpCount);
        }

        public void Part2()
        {
            List<int> jumps = LoadData();
            int jumpCount = 0;
            int index = 0;
            while (index >= 0 && index < jumps.Count)
            {
                int oldIndex = index;
                index += jumps[index];
                jumps[oldIndex] += jumps[oldIndex] >= 3 ? -1 : 1;
                jumpCount++;
            }

            Console.WriteLine("Part1: {0}", jumpCount);
        }

        private List<int> LoadData()
        {
            string inputFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\input.txt";

            List<int> jumps = new();

            if (File.Exists(inputFile))
            {
                string? line;
                StreamReader file = new StreamReader(inputFile);
                while ((line = file.ReadLine()) != null)
                {
                    jumps.Add(int.Parse(line));
                }

                file.Close();
            }

            return jumps;
        }

    }
}
