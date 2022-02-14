using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day22
{
    internal class DayClass
    {
        public enum DirectionType
        {
            Up,
            Down,
            Left,
            Right
        };

        public void Part1()
        {
            Dictionary<(int row, int col), char> nodes = LoadData();
            int size = nodes.Max(pos => pos.Key.row) + 1;
            int mid = size / 2;
            (int row, int col) currentPos = (mid, mid);
            DirectionType currentDirection = DirectionType.Up;
            int infected = 0;

            for (int i = 0; i < 10000; i++)
            {
                if (nodes[currentPos] == '#')
                {
                    currentDirection = Turn(currentDirection, true); // true == right
                    nodes[currentPos] = '.';
                }
                else
                {
                    currentDirection = Turn(currentDirection, false); // false == left
                    nodes[currentPos] = '#';
                    infected++;
                }
                currentPos = Move(currentPos, currentDirection);
                if (nodes.ContainsKey(currentPos) == false)
                {
                    nodes[currentPos] = '.';
                }
            }

            Console.WriteLine("Part1: {0}", infected);
        }

        public void Part2()
        {
            Dictionary<(int row, int col), char> nodes = LoadData();
            int size = nodes.Max(pos => pos.Key.row) + 1;
            int mid = size / 2;
            (int row, int col) currentPos = (mid, mid);
            DirectionType currentDirection = DirectionType.Up;
            int infected = 0;

            for (int i = 0; i < 10000000; i++)
            {
                switch (nodes[currentPos])
                {
                    case '#':
                        currentDirection = Turn(currentDirection, true); // true == right
                        nodes[currentPos] = 'F';
                        break;
                    case '.':
                        currentDirection = Turn(currentDirection, false); // true == right
                        nodes[currentPos] = 'W';
                        break;
                    case 'F':
                        currentDirection = Turn(currentDirection, null); // null == reverse
                        nodes[currentPos] = '.';
                        break;
                    case 'W':
                        nodes[currentPos] = '#';
                        infected++;
                        break;
                }
                currentPos = Move(currentPos, currentDirection);
                if (nodes.ContainsKey(currentPos) == false)
                {
                    nodes[currentPos] = '.';
                }
            }

            Console.WriteLine("Part1: {0}", infected);
        }

        private DirectionType Turn(DirectionType currentDirection, bool? turnRight)
        {
            DirectionType newDirection = currentDirection;

            if (turnRight.HasValue)
            {
                switch (currentDirection)
                {
                    case DirectionType.Up:
                        newDirection = turnRight.Value ? DirectionType.Right : DirectionType.Left;
                        break;
                    case DirectionType.Down:
                        newDirection = turnRight.Value ? DirectionType.Left : DirectionType.Right;
                        break;
                    case DirectionType.Left:
                        newDirection = turnRight.Value ? DirectionType.Up : DirectionType.Down;
                        break;
                    case DirectionType.Right:
                        newDirection = turnRight.Value ? DirectionType.Down : DirectionType.Up;
                        break;
                }
            }
            else
            {
                switch (currentDirection)
                {
                    case DirectionType.Up:
                        newDirection = DirectionType.Down;
                        break;
                    case DirectionType.Down:
                        newDirection = DirectionType.Up;
                        break;
                    case DirectionType.Left:
                        newDirection = DirectionType.Right;
                        break;
                    case DirectionType.Right:
                        newDirection = DirectionType.Left;
                        break;
                }
            }

            return newDirection;
        }

        private (int row, int col) Move((int row, int col) currentPos, DirectionType currentDirection)
        {
            (int row, int col) newPos = currentPos;

            switch (currentDirection)
            {
                case DirectionType.Up:
                    newPos = (currentPos.row - 1, currentPos.col);
                    break;
                case DirectionType.Down:
                    newPos = (currentPos.row + 1, currentPos.col);
                    break;
                case DirectionType.Left:
                    newPos = (currentPos.row, currentPos.col - 1);
                    break;
                case DirectionType.Right:
                    newPos = (currentPos.row, currentPos.col + 1);
                    break;
            }

            return newPos;
        }

        private Dictionary<(int row, int col), char> LoadData()
        {
            string inputFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\input.txt";

            Dictionary<(int row, int col), char> nodes = new();
            if (File.Exists(inputFile))
            {
                int row = 0;
                string? line;
                StreamReader file = new StreamReader(inputFile);
                while ((line = file.ReadLine()) != null)
                {
                    for (int col = 0; col < line.Length; col++)
                    {
                        nodes[(row, col)] = line[col];
                    }
                    row++;
                }

                file.Close();
            }

            return nodes;
        }

    }
}
