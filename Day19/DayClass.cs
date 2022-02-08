using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day19
{
    internal class DayClass
    {
        private Dictionary<(int row, int col), char> _map = new();
        public DayClass()
        {
            LoadData();
        }

        public void Part1And2()
        {
            bool finished = false;
            char direction = 'D';
            StringBuilder sb = new StringBuilder();
            int steps = 0;

            (int row, int col) current = _map.FirstOrDefault(s => s.Key.row == 0).Key;
            while (!finished)
            {
                if (_map.ContainsKey(current))
                {
                    steps++;
                    switch (_map[current])
                    {
                        case '|':
                        case '-':
                            if (direction == 'D')
                            {
                                current = (current.row + 1, current.col);
                            }
                            else if (direction == 'U')
                            {
                                current = (current.row - 1, current.col);
                            }
                            else if (direction == 'R')
                            {
                                current = (current.row, current.col + 1);
                            }
                            else // direction == 'L'
                            {
                                current = (current.row, current.col - 1);
                            }
                            break;
                        case '+':
                            if (direction == 'D' || direction == 'U')
                            {
                                // must go Left or Right
                                if (_map.ContainsKey((current.row, current.col + 1)))
                                {
                                    current = (current.row, current.col + 1);
                                    direction = 'R';
                                }
                                else if (_map.ContainsKey((current.row, current.col - 1)))
                                {
                                    current = (current.row, current.col - 1);
                                    direction = 'L';
                                }
                                else
                                {
                                    finished = true;
                                }
                            }
                            else // direction is 'L' or 'R'
                            {
                                // must go Up or Down
                                if (_map.ContainsKey((current.row + 1, current.col)))
                                {
                                    current = (current.row + 1, current.col);
                                    direction = 'D';
                                }
                                else if (_map.ContainsKey((current.row - 1, current.col)))
                                {
                                    current = (current.row - 1, current.col);
                                    direction = 'U';
                                }
                                else
                                {
                                    finished = true;
                                }
                            }
                            break;
                        default:
                            if (char.IsLetter(_map[current]))
                            {
                                sb.Append(_map[current]);
                                switch (direction)
                                {
                                    case 'U':
                                        current = (current.row - 1, current.col);
                                        break;
                                    case 'D':
                                        current = (current.row + 1, current.col);
                                        break;
                                    case 'R':
                                        current = (current.row, current.col + 1);
                                        break;
                                    case 'L':
                                        current = (current.row, current.col - 1);
                                        break;
                                }
                            }
                            else
                            {
                                finished = true;
                            }
                            break;
                    }
                }
                else
                {
                    finished = true;
                }
            }

            string rslt = sb.ToString();

            Console.WriteLine("Part1: {0}", rslt);
            Console.WriteLine("Part2: {0}", steps);
        }

        private void LoadData()
        {
            string inputFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\input.txt";

            if (File.Exists(inputFile))
            {
                int row = 0;
                string? line;
                StreamReader file = new StreamReader(inputFile);
                while ((line = file.ReadLine()) != null)
                {
                    for (int col = 0; col < line.Length; col++)
                    {
                        if (line[col] != ' ')
                        {
                            _map[(row, col)] = line[col];
                        }
                    }
                    row++;
                }

                file.Close();
            }
        }

    }
}
