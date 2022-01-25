using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day04
{
    internal class DayClass
    {
        List<string> _phrases = new();
        public DayClass()
        {
            LoadData();
        }

        public void Part1()
        {
            int nValid = 0;

            HashSet<string> unique = new();
            foreach (string phrase in _phrases)
            {
                unique.Clear();
                string[] parts = phrase.Split(' ');
                foreach (string part in parts)
                {
                    unique.Add(part);
                }
                nValid += unique.Count == parts.Count() ? 1 : 0;
            }

            Console.WriteLine("Part1: {0}", nValid);
        }

        public void Part2()
        {
            int nValid = 0;

            HashSet<string> unique = new();
            foreach (string phrase in _phrases)
            {
                unique.Clear();
                string[] parts = phrase.Split(' ');
                foreach (string part in parts)
                {
                    char[] letters = part.ToCharArray();
                    Array.Sort(letters);
                    unique.Add(new string(letters));
                }
                nValid += unique.Count == parts.Count() ? 1 : 0;
            }

            Console.WriteLine("Part2: {0}", nValid);
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
                    _phrases.Add(line);
                }

                file.Close();
            }
        }

    }
}
