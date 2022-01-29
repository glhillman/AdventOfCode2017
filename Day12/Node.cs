using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day12
{
    internal class Node
    {
        public Node(int id)
        {
            Id = id;
            Connected = new List<Node>();
        }
        public int Id { get; private set; }
        public List<Node> Connected { get; private set; }
        public override string ToString()
        {
            return String.Format("Id: {0}, NConnected: {1}", Id, Connected.Count);
        }
    }
}
