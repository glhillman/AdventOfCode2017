using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day03
{
    internal class DayClass
    {
        public void Part1()
        {
            (int x, int y) pos = SpiralToTarget(265149);

            Console.WriteLine("Part1: {0}", Math.Abs(pos.x) + Math.Abs(pos.y));
        }

        public void Part2()
        {
            int value = SpiralAdjacentSumsToTarget(265149);

            Console.WriteLine("Part2: {0}", value);
        }

        private (int x, int y) SpiralToTarget(int target)
        {
            int leftX = 0;
            int rightX = 1;
            int topY = 0;
            int bottomY = 0;
            int value = 1;

            while (value != target)
            {
                // move counterclockwise, starting to the right
                for (int x = leftX; x != rightX; x++, value++) // go right along bottomX
                {
                    if (value == target)
                    {
                        return (x, bottomY);
                    }
                }
                topY++;
                for (int y = bottomY; y != topY; y++, value++) // go up along rightX
                {
                    if (value == target)
                    {
                        return (rightX, y);
                    }
                }
                leftX--;
                for (int x = rightX; x != leftX; x--, value++)// go left along topY
                {
                    if (value == target)
                    {
                        return (x, topY);
                    }
                }
                bottomY--;
                for (int y  = topY; y != bottomY; y--, value++) // go down along leftX
                {
                    if (value == target)
                    {
                        return (leftX, y);
                    }
                }
                rightX++;
            }

            return (0, 0); // won't get here. Make compiler happy
        }

        private int SpiralAdjacentSumsToTarget(int target)
        {
            int leftX = 0;
            int rightX = 1;
            int topY = 0;
            int bottomY = 0;
            int value = 1;
            Dictionary<(int x, int y), int> map = new();

            map[(0, 0)] = 1;

            while (value != target)
            {
                // move counterclockwise, starting to the right
                for (int x = leftX+1; x <= rightX; x++) // go right along bottomX
                {
                    if (value >= target)
                    {
                        return value;
                    }
                    value = SumNeighbors(map, x, bottomY);
                    map[(x, bottomY)] = value;
                }
                topY++;
                for (int y = bottomY+1; y <= topY; y++) // go up along rightX
                {
                    if (value >= target)
                    {
                        return value;
                    }
                    value = SumNeighbors(map, rightX, y);
                    map[(rightX, y)] = value;
                }
                leftX--;
                for (int x = rightX-1; x >= leftX; x--)// go left along topY
                {
                    if (value >= target)
                    {
                        return value;
                    }
                    value = SumNeighbors(map, x, topY);
                    map[(x, topY)] = value;
                }
                bottomY--;
                for (int y = topY-1; y >= bottomY; y--) // go down along leftX
                {
                    if (value >= target)
                    {
                        return value;
                    }
                    value = SumNeighbors(map, leftX, y);
                    map[(leftX, y)] = value;
                }
                rightX++;
            }

            return 0; // won't get here. Make compiler happy
        }

        private int SumNeighbors(Dictionary<(int x, int y), int> map, int anchorX, int anchorY)
        {
            int sum = 0;
            int value;

            for (int y = anchorY+1; y >= anchorY - 1; y--)
            {
                for (int x = anchorX-1; x <= anchorX + 1; x++)
                {
                    if (!(x == anchorX && y == anchorY))
                    {
                        if (map.TryGetValue((x, y), out value))
                        {
                            sum += value;
                        }
                    }
                }
            }

            //Console.WriteLine("Summing Neighbors for {0},{1} Sum: {2}", anchorX, anchorY, sum);

            return sum;
        }
    }
}
