using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Day15
{
    internal class DayClass
    {
        public void Part1()
        {
            long a = 277L;
            long b = 349L;
            int matches = 0;

            for (int i = 0; i < 40_000_000; i++)
            {
                a *= 16807;
                a %= 2147483647;
                b *= 48271;
                b %= 2147483647;
                matches += (a & 0xFFFF) == (b & 0xFFFF) ? 1 : 0;
            }

            Console.WriteLine("Part1: {0}", matches);
        }

        public void Part2()
        {
            long a = 277L;
            long b = 349L;
            int matches = 0;

            for (int i = 0; i < 5_000_000; i++)
            {
                a = NextA(a);
                b = NextB(b);
                matches += (a & 0xFFFF) == (b & 0xFFFF) ? 1 : 0;
            }

            Console.WriteLine("Part1: {0}", matches);
        }

        private long NextA(long a)
        {
            do
            {
                a *= 16807;
                a %= 2147483647;
                if (a % 4 == 0)
                {
                    return a;
                }
            } while (true);
        }

        private long NextB(long b)
        {
            do
            {
                b *= 48271;
                b %= 2147483647;
                if (b % 8 == 0)
                {
                    return b;
                }
            } while (true);
        }
    }
}
