using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day20
{
    internal class DayClass
    {
        public DayClass()
        {
            LoadData();
        }

        public void Part1()
        {
            List<Particle> particles = LoadData();

            List<int> nearest = new();

            for (int i = 0; i < 500; i++)
            {
                foreach (Particle p in particles)
                {
                    p.DoTick();
                }
                particles.Sort((a, b) => a.Manhattan < b.Manhattan ? -1 : 1);
                nearest.Add(particles[0].Id);
            }
            var rslts = from manhattan in nearest
                        group manhattan by manhattan into grp
                        let count = grp.Count()
                        orderby count descending
                        select new { Value = grp.Key, Count = count };

            var rslt = rslts.First();

            Console.WriteLine("Part1: {0}", rslt.Value);
        }

        public void Part2()
        {
            List<Particle> particles = LoadData();
            int particleCount = particles.Count();

            for (int i = 0; i < 50; i++)
            {
                foreach (Particle p in particles)
                {
                    p.DoTick();
                }
                // eliminate collisions
                for (int p = 0; p < particleCount - 1; p++)
                {
                    if (particles[p].IsDuplicate == false)
                    {
                        for (int j = p + 1; j < particleCount; j++)
                        {
                            if (particles[j].IsDuplicate == false)
                            {
                                if (particles[p].IsEqualTo(particles[j]))
                                {
                                    particles[p].IsDuplicate = true;
                                    particles[j].IsDuplicate = true;
                                }
                            }
                        }
                    }
                }
            }

            int rslt = particles.Count(p => p.IsDuplicate == false);

            Console.WriteLine("Part1: {0}", rslt);
        }

        private List<Particle> LoadData()
        {
            List<Particle> particles = new();

            string inputFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\input.txt";

            if (File.Exists(inputFile))
            {
                char[] delimiters = new char[] { '<', ',', '>' };
                int id = 0;
                string? line;
                StreamReader file = new StreamReader(inputFile);
                while ((line = file.ReadLine()) != null)
                {
                    string[] parts = line.Split(delimiters);
                    particles.Add(new Particle(id++, PartsToVector(parts, 1), PartsToVector(parts, 6), PartsToVector(parts, 11)));
                }

                file.Close();
            }

            return particles;
        }

        private Vector PartsToVector(string[] parts, int startIndex)
        {
            long x = long.Parse(parts[startIndex++]);
            long y = long.Parse(parts[startIndex++]);
            long z = long.Parse(parts[startIndex++]);

            return new Vector(x, y, z);
        }

    }
}
