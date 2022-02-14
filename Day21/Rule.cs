using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day21
{
    internal class Rule
    {

        public static List<string> TransformPattern(string patternIn)
        {
            List<string> keys = new();
            string[] split = patternIn.Split('/');
            int size = split[0].Length;
            char[,] baseGrid = new char[size, size];
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    baseGrid[row, col] = split[row][col];
                }
            }

            keys.Add(GridToString(baseGrid, 0, 0, size));
            char[,] temp = Rotate(baseGrid);
            keys.Add(GridToString(temp, 0, 0, size));
            temp = Rotate(temp);
            keys.Add(GridToString(temp, 0, 0, size));
            temp = Rotate(temp);
            keys.Add(GridToString(temp, 0, 0, size));
            
            temp = Flip(baseGrid);
            keys.Add(GridToString(temp, 0, 0, size));
            temp = Rotate(temp);
            keys.Add(GridToString(temp, 0, 0, size));
            temp = Rotate(temp);
            keys.Add(GridToString(temp, 0, 0, size));
            temp = Rotate(temp);
            keys.Add(GridToString(temp, 0, 0, size));

            return keys;
        }
        public static char[,] Flip(char[,] input)
        {
            int max = input.GetLength(0) - 1;

            char[,] output = new char[max + 1, max + 1];

            for (int y = 0; y <= max; y++)
            {
                for (int x = 0; x <= max; x++)
                {
                    output[max - x, y] = input[x, y];
                }
            }

            return output;
        }
        public static char[,] Rotate(char[,] input)
        {
            int max = input.GetLength(0) - 1;

            char[,] output = new char[max + 1, max + 1];

            for (int y = 0; y <= max; y++)
            {
                for (int x = 0; x <= max; x++)
                {
                    output[y, max - x] = input[x, y];
                }
            }

            return output;
        }

        public static string GridToString(char[,] grid, int topRow, int leftCol, int size)
        {
            StringBuilder sb = new StringBuilder();

            for (int row = topRow; row < topRow + size; row++)
            {
                for (int col = leftCol; col < leftCol + size; col++)
                {
                    sb.Append(grid[row, col]);
                }
            }
            return sb.ToString();
        }

        public static string GridToString(Dictionary<(int row, int col), char> grid, int topRow, int leftCol, int size)
        {
            StringBuilder sb = new StringBuilder();

            for (int row = topRow; row < topRow + size; row++)
            {
                for (int col = leftCol; col < leftCol + size; col++)
                {
                    sb.Append(grid[(row, col)]);
                }
            }
            return sb.ToString();
        }

        public static void StringToGrid(string chars, Dictionary<(int row, int col), char> grid, int topRow, int leftCol)
        {
            // the input string is in ccc/ccc/ccc format, or cccc/cccc/cccc/cccc
            string[] split = chars.Split('/');
            int size = split.Length;
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    grid[(row + topRow, col + leftCol)] = split[row][col];
                }
            }
        }
    }
}
