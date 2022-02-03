using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day14
{
    internal class DayClass
    {
        Dictionary<(int row, int col), (bool used, int region)> grid = new();

        public DayClass()
        {
            LoadData();
        }

        public void Part1()
        {
            for (int row = 0; row < 128; row++)
            {
                string key = string.Format("{0}-{1}", Key, row);
                string hex = CreateHash(key);
                string bin = HexToBinary(hex);
                for (int col = 0; col < 128; col++)
                {
                    grid[(row, col)] = (bin[col] == '1', 0);
                }
            }

            int used = grid.Count(s => s.Value.used == true);

            Console.WriteLine("Part1: {0}", used);
        }

        public void Part2()
        {
            int currentRegion = 0;
            (int row, int col)? pos = GetRoot();
            while (pos != null)
            {
                RecurseFindRegion(pos.Value, ++currentRegion);
                pos = GetRoot();
            }

            Console.WriteLine("Part2: {0}", currentRegion);
        }

        private void RecurseFindRegion((int row, int col) pos, int currentRegion)
        {
            if (grid[pos] == (true, 0))
            {
                grid[pos] = (true, currentRegion);
                if (pos.row > 0)
                    RecurseFindRegion((pos.row - 1, pos.col), currentRegion);
                if (pos.row < 127)
                    RecurseFindRegion((pos.row + 1, pos.col), currentRegion);
                if (pos.col > 0)
                    RecurseFindRegion((pos.row, pos.col - 1), currentRegion);
                if (pos.col < 127)
                    RecurseFindRegion((pos.row, pos.col + 1), currentRegion);
            }
        }
        
        private (int row, int col)? GetRoot()
        {
            for (int row = 0; row < 128; row++)
            {
                for (int col = 0; col < 128; col++)
                {
                    if (grid[(row, col)] == (true, 0))
                    {
                        return (row, col);
                    }
                }
            }

            return null;
        }
        private string Key { get; set; }

        private string CreateHash(string key)
        {
            byte[] elements = new byte[256];

            List<byte> lengths = Encoding.ASCII.GetBytes(key).ToList();
            lengths.Add(17);
            lengths.Add(31);
            lengths.Add(73);
            lengths.Add(47);
            lengths.Add(23);

            for (int i = 0; i < 256; i++)
            {
                elements[i] = (byte)i;
            }

            int cursor = 0;
            int skipSize = 0;

            for (int i = 0; i < 64; i++)
            {
                (cursor, skipSize) = Twister(elements, lengths, cursor, skipSize);
            }

            // reduce the sparse hash (0..255 elements) to dense hash (16 elements)
            byte[] denseHash = new byte[16];
            for (int i = 0; i < 16; i++)
            {
                int index = i * 16;
                byte value = 0;
                for (int j = 0; j < 16; j++)
                {
                    value ^= elements[j + index];
                }
                denseHash[i] = value;
            }

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 16; i++)
            {
                sb.AppendFormat("{0:x2}", denseHash[i]);
            }

            return sb.ToString();
        }

        private string HexToBinary(string hex)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 32; i+=4)
            {
                string sub = hex.Substring(0, 4);
                Int32 i32 = Convert.ToInt32(sub, 16);
                string s = Convert.ToString(i32, 2);
                sb.Append(Convert.ToString(Convert.ToInt32(hex.Substring(i, 4), 16), 2).PadLeft(16, '0'));
            }
            return sb.ToString();
        }

        public (int, int) Twister(byte[] elements, List<byte> lengths, int cursor, int skipSize)
        {
            int nElements = elements.Length;
            byte[] temp = new byte[nElements];

            foreach (int length in lengths)
            {
                if (length <= nElements)
                {
                    if (cursor + (length - 1) < nElements)
                    {
                        Array.Reverse(elements, cursor, length);
                    }
                    else
                    {
                        int j = cursor;
                        for (int i = 0; i < length; i++)
                        {
                            temp[i] = elements[(j + i) % nElements];
                        }
                        Array.Reverse(temp, 0, length);
                        for (int i = 0; i < length; i++)
                        {
                            elements[(j + i) % nElements] = temp[i];
                        }
                    }
                    cursor = (cursor + length + skipSize) % nElements;
                    skipSize++;
                }
            }

            return (cursor, skipSize);
        }

        private void LoadData()
        {
            string inputFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\input.txt";

            if (File.Exists(inputFile))
            {
                string? line;
                StreamReader file = new StreamReader(inputFile);
                Key = file.ReadLine();
                file.Close();
            }
        }

    }
}
