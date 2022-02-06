using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day17
{
    internal class DayClass
    {
        public void Part1()
        {
            (int afterMaxValue, int afterZeroValue) = SpinLock(369, 2017);

            Console.WriteLine("Part1: {0}", afterMaxValue);
        }

        public void Part2()
        {
            /// this version only needs to keep track of the next value after 0
            /// Catch it each time it occurs and the last value stored will be
            ///   the final answer
            int index = 0;
            int answer = 0;
            for (int i = 0; i < 50_000_000; i++)
            {
                int nextIndex = (index + 369) % (i + 1);
                if (nextIndex == 0)
                {
                    answer = i + 1;
                }
                index = nextIndex + 1;
            }

            Console.WriteLine("Part2: {0}", answer);
        }

        private (int afterMaxValue, int afterZero) SpinLock(int stepSize, int maxValue)
        {
            int currentId = 0;
            Node zeroNode = new Node(0); // takes over 220 seconds to find part 2. New approach devised for that one.
            Node currentNode = zeroNode;
            while (currentId < maxValue)
            {
                int adjustedSteps = stepSize;
                ++currentId;
                if (currentId < stepSize)
                {
                    adjustedSteps = stepSize % currentId;
                }
                if (adjustedSteps > currentId / 2)
                {
                    adjustedSteps = currentId - adjustedSteps;
                    for (int i = 0; i < adjustedSteps; i++)
                    {
                        currentNode = currentNode.Left;
                    }
                }
                else
                {
                    for (int i = 0; i < adjustedSteps; i++)
                    {
                        currentNode = currentNode.Right;
                    }
                }
                Node newNode = new Node(currentId);
                newNode.Left = currentNode;
                newNode.Right = currentNode.Right;
                currentNode.Right = newNode;
                newNode.Right.Left = newNode;
                currentNode = newNode;
            }

            return (currentNode.Right.Id, zeroNode.Right.Id);
        }
    }
}
