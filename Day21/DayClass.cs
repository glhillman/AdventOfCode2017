using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day21
{
    internal class DayClass
    {
        public void Part1And2()
        {
            Dictionary<string, string> patterns = LoadData();
            Dictionary<(int row, int col), char> grid1 = new();
            Dictionary<(int row, int col), char> grid2 = new();
            Dictionary<(int row, int col), char> src = grid1;
            Dictionary<(int row, int col), char> dst = grid2;

            // starting grid
            // .#.
            // ..#
            // ###
            src[(0, 0)] = '.';
            src[(0, 1)] = '#';
            src[(0, 2)] = '.';
            src[(1, 0)] = '.';
            src[(1, 1)] = '.';
            src[(1, 2)] = '#';
            src[(2, 0)] = '#';
            src[(2, 1)] = '#';
            src[(2, 2)] = '#';

            int size = 3;
            int srcIncrement;
            int dstIncrement;

            for (int i = 0; i < 18; i++)
            {
                if (i == 5)
                {
                    // part1
                    Console.WriteLine("Part1: {0}", src.Count(c => c.Value == '#'));
                }

                if (size % 2 == 0)
                {
                    srcIncrement = 2;
                }
                else
                {
                    srcIncrement = 3;
                }
                dstIncrement = srcIncrement + 1;
                int srcRow = 0;
                int dstRow = 0;
                while (srcRow < size)
                {
                    int srcCol = 0;
                    int dstCol = 0;
                    while (srcCol < size)
                    {
                        string key = Rule.GridToString(src, srcRow, srcCol, srcIncrement);
                        string targetString = patterns[key];
                        Rule.StringToGrid(targetString, dst, dstRow, dstCol);
                        srcCol += srcIncrement;
                        dstCol += dstIncrement;
                    }
                    srcRow += srcIncrement;
                    dstRow += dstIncrement;
                }
                size = dstRow;
                (src, dst) = (dst, src);
            }

            long rslt = src.Count(c => c.Value == '#');

            Console.WriteLine("Part2: {0}", rslt);
        }

        private Dictionary<string, string> LoadData()
        {
            string inputFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\input.txt";
            Dictionary<string, string> patterns = new();
            if (File.Exists(inputFile))
            {
                string? line;
                StreamReader file = new StreamReader(inputFile);
                while ((line = file.ReadLine()) != null)
                {
                    string[] parts = line.Split(" => ");
                    List<string> rotations = Rule.TransformPattern(parts[0]);
                    foreach (string rotation in rotations)
                    {
                        patterns[rotation] = parts[1];
                    }
                }

                file.Close();
            }

            return patterns;
        }
    }
}
