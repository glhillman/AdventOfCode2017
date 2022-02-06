using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day17
{
    internal class Node
    {
        public Node(int id)
        {
            Id = id;
            Left = this;
            Right = this;
        }
        public int Id { get; private set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public override string ToString()
        {
            return String.Format("Id: {0}, Left: {1}, Right: {2}", Id, Left.Id, Right.Id);
        }
    }
}
