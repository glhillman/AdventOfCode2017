using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day13
{
    internal class DayClass
    {
        List<Layer> _layers = new();
        public DayClass()
        {
            LoadData();
        }

        public void Part1()
        {
            int severity = 0;
            foreach (Layer layer in _layers)
            {
                if (layer.IsCaught(0))
                {
                    severity += layer.Depth * layer.Range;
                }
            }

            Console.WriteLine("Part1: {0}", severity);
        }

        public void Part2()
        {
            int step = 0;
            bool caught;
            do
            {
                step++;
                caught = false;

                for (int i = 0; !caught && i < _layers.Count; i++)
                {
                    if (_layers[i].IsCaught(step))
                    {
                        caught = true;
                        break;
                    }
                }
            } while (caught);

            Console.WriteLine("Part2: {0}", step);
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
                    string[] parts = line.Split(':');
                    int depth = int.Parse(parts[0]);
                    int range = int.Parse(parts[1]);
                    _layers.Add(new Layer(depth, range));
                }

                file.Close();
            }
        }

    }
}
