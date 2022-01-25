using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day06
{
    internal class DayClass
    {
        public void Part1()
        {
            List<List<int>> history = new();
            List<int> blocks = LoadData();
            int redistCount = 0;
            bool infiniteDetected = false;

            while (infiniteDetected == false)
            {
                int index = blocks.IndexOf(blocks.Max());
                int block = blocks[index];
                blocks[index] = 0;
                while (block > 0)
                {
                    index = (index + 1) % blocks.Count;
                    blocks[index]++;
                    block--;
                }
                redistCount++;
                foreach (List<int> histBlock in history)
                {
                    if (histBlock.SequenceEqual(blocks))
                    {
                        infiniteDetected = true;
                        break;
                    }
                }
                history.Add(new List<int>(blocks));
            }

            Console.WriteLine("Part1: {0}", redistCount);
        }

        public void Part2()
        {
            List<List<int>> history = new();
            List<int> blocks = LoadData();
            int redistCount = 0;
            bool infiniteDetected = false;
            bool firstPass = true;

            while (infiniteDetected == false)
            {
                int index = blocks.IndexOf(blocks.Max());
                int block = blocks[index];
                blocks[index] = 0;
                while (block > 0)
                {
                    index = (index + 1) % blocks.Count;
                    blocks[index]++;
                    block--;
                }
                redistCount++;
                foreach (List<int> histBlock in history)
                {
                    if (histBlock.SequenceEqual(blocks))
                    {
                        if (firstPass)
                        {
                            history.Clear();
                            redistCount = 0;
                            firstPass = false;
                            break;
                        }
                        else
                        {
                            infiniteDetected = true;
                            break;
                        }
                    }
                }
                history.Add(new List<int>(blocks));
            }

            Console.WriteLine("Part1: {0}", redistCount);
        }

        private List<int> LoadData()
        {
            string inputFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\input.txt";
            List<int> blocks = new();
            if (File.Exists(inputFile))
            {
                string? line;
                StreamReader file = new StreamReader(inputFile);
                line = file.ReadLine();
                string[] parts = line.Replace('\t', ' ').Split(' ');
                foreach (string part in parts)
                {
                    blocks.Add(int.Parse(part));
                }

                file.Close();
            }

            return blocks;
        }

    }
}
