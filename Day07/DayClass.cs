using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day07
{
    internal class DayClass
    {
        List<Node> _nodes = new List<Node>();
        public DayClass()
        {
            LoadData();
            _nodes.Sort(delegate (Node x, Node y) { return x.Weight - y.Weight;});
        }

        public void Part1()
        {
            Node root = _nodes.First(n => n.Parent == null);

            Console.WriteLine("Part1: {0}", root.Name);
        }

        public void Part2()
        {
            Node node = _nodes.First(n => n.Parent == null);
            int targetWeight = 0;

            // follow the unbalanced node until it is balanced
            while (!node.IsBalanced)
            {
                (node, targetWeight) = node.GetUnbalancedChild();
            }
            
            int adjustedWeight = node.Weight - (node.TotalWeight - targetWeight);

            Console.WriteLine("Part2: {0}", adjustedWeight);
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
                    string[] parts = line.Split("->");
                    // process left side
                    string[] parts2 = parts[0].Split(new char[] {' ', '(', ')'});
                    string name = parts2[0].Trim();
                    Node? parent = _nodes.FirstOrDefault(n => n.Name == name);
                    if (parent == null)
                    {
                        parent = new Node(name);
                        _nodes.Add(parent);
                    }
                    parent.Weight = int.Parse(parts2[2]);

                    // process right side if any
                    if (parts.Length > 1)
                    {
                        parts2 = parts[1].Split(',');
                        foreach (string part in parts2)
                        {
                            name = part.Trim();
                            Node? child = _nodes.FirstOrDefault((n) => n.Name == name);
                            if (child == null)
                            {
                                child = new Node(name);
                                _nodes.Add(child);
                            }
                            child.Parent = parent;
                            parent.Children.Add(child);
                        }
                    }
                }

                file.Close();
            }
        }

    }
}
