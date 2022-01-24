using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day01
{
    internal class DayClass
    {
        public string _input;
        public DayClass()
        {
            LoadData();
        }

        public void Part1()
        {
            int sum = 0;
            for (int i = 0; i < _input.Length; i++)
            {
                if (_input[i] == _input[(i+1) % _input.Length])
                {
                    sum += int.Parse(_input[i].ToString());
                }
            }

            Console.WriteLine("Part1: {0}", sum);
        }

        public void Part2()
        {
            int sum = 0;
            int bias = _input.Length / 2;
            for (int i = 0; i < _input.Length; i++)
            {
                if (_input[i] == _input[(i + bias) % _input.Length])
                {
                    sum += int.Parse(_input[i].ToString());
                }
            }

            Console.WriteLine("Part1: {0}", sum);
        }

        private void LoadData()
        {
            string inputFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\input.txt";

            if (File.Exists(inputFile))
            {
                StreamReader file = new StreamReader(inputFile);
                _input = file.ReadLine();
                file.Close();
            }
        }

    }
}
