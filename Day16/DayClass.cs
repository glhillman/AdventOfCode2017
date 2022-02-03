using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day16
{
    internal class DayClass
    {
        record move (char dance, int indexA, int indexB, char charA, char charB);
        private move[] _moves;
        public DayClass()
        {
            LoadData();
        }

        public void Part1()
        {
            (string rslt, int repeatsAt) = DoDance(1, false);

            Console.WriteLine("Part1: {0}", rslt);
        }

        public void Part2()
        {

            (string rslt, int repeatsAt) values = DoDance(1_000_000_000, true);
            int trueLoops = 1_000_000_000 % values.repeatsAt;
            values = DoDance(trueLoops, false);

            Console.WriteLine("Part2: {0}", values.rslt);
        }

        private (string rslt, int repeatsAt) DoDance(int loops, bool returnOnRepeat)
        {
            char[] progs = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p' };
            string original = new string(progs);
            int len = progs.Length;
            char[] temp = new char[len];
            int i = 0;

            while (i++ < loops)
            {
                foreach (move m in _moves)
                {
                    switch (m.dance)
                    {
                        case 's':
                            {
                                int n = m.indexA;
                                Array.Copy(progs, len - n, temp, 0, n);
                                Array.Copy(progs, 0, progs, n, len - n);
                                Array.Copy(temp, 0, progs, 0, n);
                            }
                            break;
                        case 'x':
                            (progs[m.indexA], progs[m.indexB]) = (progs[m.indexB], progs[m.indexA]);
                            break;
                        case 'p':
                            {
                                int indexA = 0;
                                while (progs[indexA] != m.charA)
                                {
                                    indexA++;
                                }
                                int indexB = 0;
                                while (progs[indexB] != m.charB)
                                {
                                    indexB++;
                                }
                                (progs[indexA], progs[indexB]) = (progs[indexB], progs[indexA]);
                            }
                            break;
                    }
                }
                if (new string(progs) == original)
                {
                    return (" ", i); // return number of loops required before the original order is restored
                }
            }

            return (new string(progs), 0);
        }
        private void LoadData()
        {
            string inputFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\input.txt";

            if (File.Exists(inputFile))
            {
                List<move> moves = new();
                StreamReader file = new StreamReader(inputFile);
                string[] raw = file.ReadLine().Split(',');
                foreach (string s in raw)
                {
                    switch (s[0])
                    {
                        case 's':
                            {
                                int n = int.Parse(s.Substring(1));
                                moves.Add(new move('s', n, 0, ' ', ' '));
                            }
                            break;
                        case 'x':
                            {
                                string[] parts = s.Substring(1).Split('/');
                                int indexA = int.Parse(parts[0]);
                                int indexB = int.Parse(parts[1]);
                                moves.Add(new move('x', indexA, indexB, ' ', ' '));
                            }
                            break;
                        case 'p':
                            {
                                string[] parts = s.Substring(1).Split('/');
                                moves.Add(new move('p', 0, 0, parts[0][0], parts[1][0]));
                            }
                            break;
                    }
                    file.Close();

                    _moves = moves.ToArray();
                }
            }
        }

    }
}
