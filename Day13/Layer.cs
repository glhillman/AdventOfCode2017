using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day13
{
    internal class Layer
    {
        public Layer(int depth, int range)
        {
            Depth = depth;
            Range = range;
        }

        public int Depth { get; private set; }
        public int Range { get; private set; }
        public bool IsCaught(int step)
        {
            return (step + Depth)  % ((Range - 1) * 2) == 0;
        }
        public override string ToString()
        {
            return String.Format("Depth: {0}, Range: {1}", Depth, Range);
        }
    }
}
