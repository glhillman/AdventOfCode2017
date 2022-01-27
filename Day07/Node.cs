using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day07
{
    internal class Node
    {
        public Node(string name)
        {
            Name = name;
            Children = new List<Node>();
            Parent = null;
        }
        public int Weight { get; set; }
        public string Name { get; private set; }
        public Node? Parent { get; set; }
        public List<Node> Children { get; set;}
        public int ChildrenWeight { get { return Children.Sum(c => c.TotalWeight); } }
        public int TotalWeight { get { return Weight + ChildrenWeight; } }
        public bool IsBalanced
        {
            get
            {
                var groups = Children.GroupBy(x => x.TotalWeight);
                return groups.Count() == 1;
            }
        }
        public (Node node, int targetWeight) GetUnbalancedChild()
        {
            // we know coming in that this is an unbalanced group
            // group by totalweight
            var groups = Children.GroupBy(x => x.TotalWeight);
            // get the weight value of majority group (there will be only one or two groups)
            var targetWeight = groups.First(x => x.Count() > 1).Key;
            // get the group with only one entry (this is the unbalanced one)
            var unbalancedChild = groups.First(x => x.Count() == 1).First();

            // return the unbalanced node with the expected weight value
            return (unbalancedChild, targetWeight);
        }
        public override string ToString()
        {
            string tostring = String.Format("Name: {0}, Weight: {1}, Parent: {2}, N Children: {3}, Children Weight: {4}, Total Weight: {5}",
                                             Name, Weight, Parent == null ? "NULL" : Parent.Name, Children.Count, ChildrenWeight, TotalWeight);
            if (Children.Count > 0)
            {
                tostring += ", IsBalanced: " + IsBalanced.ToString();
            }
            return tostring;
        }
    }
}
