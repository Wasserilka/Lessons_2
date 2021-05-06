using System;
using System.Collections.Generic;

namespace Lesson_6
{
    public class Node 
    {
        public int Value { get; set; }
        public bool Visited { get; set; }
        public List<Edge> Edges { get; set; } 
        public Node(int _Value)
        {
            Value = _Value;
            Edges = new List<Edge>();
        }
    }

    public class Edge 
    {
        public int Weight { get; set; } 
        public Node Node { get; set; } 
        public Edge(int _Weight, Node _Node)
        {
            Weight = _Weight;
            Node = _Node;
        }
    }
}
