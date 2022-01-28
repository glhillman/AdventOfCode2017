using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day09
{
    internal class DayClass
    {
        public string _stream;
        public DayClass()
        {
            LoadData();
        }

        public void Part1And2()
        {
            (int totalGroupScore, int garbageCount) = ProcessStream(_stream);
            Console.WriteLine("Part1: {0}", totalGroupScore);
            Console.WriteLine("Part2: {0}", garbageCount);
        }

        private (int, int) ProcessStream(string stream)
        {
            Stack<char> stack = new();
            int currentGroupScore = 0;
            int totalGroupScore = 0;
            bool inGarbage = false;
            int garbageCount = 0;

            for (int i = 0; i < stream.Length; i++)
            {
                char c = stream[i];

                switch (c)
                {
                    case '{':
                        if (inGarbage)
                        {
                            garbageCount++;
                        }
                        else
                        {
                            stack.Push(c);
                            currentGroupScore++;
                        }
                        break;
                    case '}':
                        if (inGarbage)
                        {
                            garbageCount++;
                        }
                        else
                        {
                            totalGroupScore += currentGroupScore;
                            currentGroupScore--;
                            stack.Pop();
                        }
                        break;
                    case '<':
                        if (inGarbage)
                        {
                            garbageCount++;
                        }
                        else
                        {
                            inGarbage = true;
                        }
                        break;
                    case '>':
                        if (inGarbage)
                        {
                            inGarbage = false;
                        }
                        break;
                    case '!':
                        if (inGarbage)
                        {
                            i++;
                        }
                        break;
                    default:
                        if (inGarbage)
                        {
                            garbageCount++;
                        }
                        break;
                }
            }
            return (totalGroupScore, garbageCount);
        }

        private void LoadData()
        {
            string inputFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\input.txt";

            if (File.Exists(inputFile))
            {
                StreamReader file = new StreamReader(inputFile);
                _stream = file.ReadLine();
                file.Close();
            }
        }

    }
}
