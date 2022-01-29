using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day12
{
    internal class DayClass
    {
        List<Node> _nodes = new List<Node>();
        public DayClass()
        {
            LoadData();
            ResolveConnections(_nodes);
        }

        public void Part1()
        {
            int nPrograms = CountPrograms(_nodes, 0).Count;

            Console.WriteLine("Part1: {0}", nPrograms);
        }

        public void Part2()
        {
            HashSet<string> groups = new();

            for (int i = 0; i < _nodes.Count; i++)
            {
                HashSet<int> programs = CountPrograms(_nodes, i);
                int[] array = programs.ToArray();
                Array.Sort(array);
                string group = String.Join(",", array);
                groups.Add(group);
            }

            Console.WriteLine("Part2: {0}", groups.Count);
        }

        private void ResolveConnections(List<Node> nodes)
        {
            foreach (Node node in nodes)
            {
                foreach (Node node2 in nodes)
                {
                    for (int i = 0; i < node2.Connected.Count; i++)
                    {
                        if (node2.Connected[i].Id == node.Id)
                        {
                            node2.Connected[i] = node;
                        }
                    }
                }
            }
        }
        private HashSet<int> CountPrograms(List<Node> nodes, int id)
        {
            HashSet<int> connectedNodes = new();
            bool nodesAdded;

            do
            {
                nodesAdded = false;
                foreach (Node node in nodes)
                {
                    if (node.Id == id)
                    {
                        // add everyting left & right for the target id
                        nodesAdded = nodesAdded || connectedNodes.Contains(node.Id) == false;
                        connectedNodes.Add(node.Id);
                        foreach (Node node2 in node.Connected)
                        {
                            nodesAdded = nodesAdded || connectedNodes.Contains(node2.Id) == false;
                            connectedNodes.Add(node2.Id);
                        }
                    }
                    else
                    {
                        foreach (Node node2 in node.Connected)
                        {
                            if (connectedNodes.Contains(node2.Id))
                            {
                                nodesAdded = nodesAdded || connectedNodes.Contains(node.Id) == false;
                                connectedNodes.Add(node.Id);
                                nodesAdded = nodesAdded || connectedNodes.Contains(node.Id) == false;
                                connectedNodes.Add(node2.Id);
                            }
                        }
                    }
                }
            } while (nodesAdded);

            return connectedNodes;
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
                    string[] parts = line.Split("<->");
                    Node node = new Node(int.Parse(parts[0]));
                    string[] parts2 = parts[1].Split(',');
                    foreach (string strId in parts2)
                    {
                        int id = int.Parse(strId);
                        Node node2 = new Node(id);
                        node.Connected.Add(node2);
                    }
                    _nodes.Add(node);
                }

                file.Close();
            }
        }

    }
}
