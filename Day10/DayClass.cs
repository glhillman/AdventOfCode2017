using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day10
{
    internal class DayClass
    {
        public int NElements = 256;
        public List<byte> _lengths = new();
        public string _lengthsString;
        public DayClass()
        {
            LoadData();
        }

        public void Part1()
        {
            byte[] elements = new byte[NElements];

            for (int i = 0; i < NElements; i++)
            {
                elements[i] = (byte)i;
            }

            int cursor = 0;
            int skipSize = 0;
            (cursor, skipSize) = Twister(elements, _lengths, cursor, skipSize);

            int rslt = (int)elements[0] * (int)elements[1];

            Console.WriteLine("Part1: {0}", rslt);
        }

        public void Part2()
        {
            byte[] elements = new byte[NElements];

            List<byte> lengths = Encoding.ASCII.GetBytes(_lengthsString).ToList();
            lengths.Add(17);
            lengths.Add(31);
            lengths.Add(73);
            lengths.Add(47);
            lengths.Add(23);

            for (int i = 0; i < NElements; i++)
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

            string rslt = sb.ToString();

            Console.WriteLine("Part2: {0}", rslt);
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
                _lengthsString = file.ReadLine();
                string[] parts = _lengthsString.Split(',');
                foreach (string part in parts)
                {
                    _lengths.Add(byte.Parse(part));
                }

                file.Close();
            }
        }

    }
}
