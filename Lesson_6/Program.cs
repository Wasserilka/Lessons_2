using System;
using System.Collections.Generic;

namespace Lesson_6
{
    class Program
    {
        public class UnexistentNodeException : Exception
        {
            public UnexistentNodeException()
            {
                HResult = (int)ExceptionHResult.UnexistentNodeException;
            }
        }
        public class TestCase
        {
            public Node GraphTopNode;
            public int Value { get; set; }
            public int ExpectedException { get; set; }
            public TestCase(Node _GraphTopNode, int _Value, int _ExpectedException)
            {
                GraphTopNode = _GraphTopNode;
                Value = _Value;
                ExpectedException = _ExpectedException;
            }
        }
        static void Main(string[] args)
        {
            int[,] graphTable = {
                { 0, 3, 0, 4, 0, 0, 0},
                { 0, 0, 9, 0, 0, 5, 0},
                { 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 2, 0, 0},
                { 0, 0, 0, 0, 0, 3, 4},
                { 8, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0}};
            var graphNodes = GetGraphFromTable(graphTable);

            Console.WriteLine("BFS");
            TestBFS(new TestCase(graphNodes[0], 2, (int)ExceptionHResult.NoException));
            ClearTheGraph(graphNodes);
            TestBFS(new TestCase(graphNodes[3], 6, (int)ExceptionHResult.NoException));
            ClearTheGraph(graphNodes);
            TestBFS(new TestCase(graphNodes[0], 10, (int)ExceptionHResult.NoException));
            ClearTheGraph(graphNodes);
            TestBFS(new TestCase(graphNodes[0], 10, (int)ExceptionHResult.UnexistentNodeException));
            ClearTheGraph(graphNodes);

            Console.WriteLine("DFS");
            TestDFS(new TestCase(graphNodes[0], 2, (int)ExceptionHResult.NoException));
            ClearTheGraph(graphNodes);
            TestDFS(new TestCase(graphNodes[3], 6, (int)ExceptionHResult.NoException));
            ClearTheGraph(graphNodes);
            TestDFS(new TestCase(graphNodes[0], 10, (int)ExceptionHResult.NoException));
            ClearTheGraph(graphNodes);
            TestDFS(new TestCase(graphNodes[0], 10, (int)ExceptionHResult.UnexistentNodeException));
        }
        static Node[] GetGraphFromTable(int[,] table)
        {
            var nodes = new Node[table.GetLength(0)];
            for (int i = 0; i < table.GetLength(0); i++)
            {
                nodes[i] = new Node(i);
            }
            for (int i = 0; i < table.GetLength(0); i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    if (table[i, j] != 0)
                    {
                        var edge = new Edge(table[i, j], nodes[j]);
                        nodes[i].Edges.Add(edge);
                    }
                }
            }
            return nodes;
        }
        static Node BFS(Node top, int value)
        {
            Console.WriteLine($"Искомый эдемент: {value}");
            var queue = new Queue<Node>();
            queue.Enqueue(top);
            top.Visited = true;
            Console.WriteLine($"Элемент {top.Value} добавлен в очередь");
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                Console.WriteLine($"Элемент {node.Value} взят из очереди");
                if (node.Value == value)
                {
                    Console.WriteLine($"Элемент {node.Value} является искомым!");
                    return node;
                }
                foreach (Edge edge in node.Edges)
                {
                    if (!edge.Node.Visited)
                    {
                        queue.Enqueue(edge.Node);
                        edge.Node.Visited = true;
                        Console.WriteLine($"Элемент {edge.Node.Value} добавлен в очередь");
                    }
                    else
                    {
                        Console.WriteLine($"Элемент {edge.Node.Value} уже был добавлен в очередь!");
                    }
                }
            }
            Console.WriteLine($"Элемент {value} не был найден!");
            throw new UnexistentNodeException();
        }
        static Node DFS(Node top, int value)
        {
            Console.WriteLine($"Искомый эдемент: {value}");
            var stack = new Stack<Node>();
            stack.Push(top);
            top.Visited = true;
            Console.WriteLine($"Элемент {top.Value} добавлен в стек");
            while (stack.Count > 0)
            {
                var node = stack.Pop();
                Console.WriteLine($"Элемент {node.Value} взят из стека");
                if (node.Value == value)
                {
                    Console.WriteLine($"Элемент {node.Value} является искомым!");
                    return node;
                }
                for (int i = node.Edges.Count - 1; i >= 0; i--)
                {
                    if (!node.Edges[i].Node.Visited)
                    {
                        stack.Push(node.Edges[i].Node);
                        node.Edges[i].Node.Visited = true;
                        Console.WriteLine($"Элемент {node.Edges[i].Node.Value} добавлен в стек");
                    }
                    else
                    {
                        Console.WriteLine($"Элемент {node.Edges[i].Node.Value} уже был добавлен в стек!");
                    }
                }
            }
            Console.WriteLine($"Элемент {value} не был найден!");
            throw new UnexistentNodeException();
        }
        static void TestBFS(TestCase testCase)
        {
            try
            {
                BFS(testCase.GraphTopNode, testCase.Value);
                Console.WriteLine("ТЕСТ ПРОЙДЕН\n");
            }
            catch (Exception ex)
            {
                if (ex.HResult == testCase.ExpectedException)
                {
                    Console.WriteLine("ТЕСТ ПРОЙДЕН\n");
                }
                else
                {
                    Console.WriteLine("ТЕСТ НЕ ПРОЙДЕН\n");
                }
            }
        }
        static void TestDFS(TestCase testCase)
        {
            try
            {
                DFS(testCase.GraphTopNode, testCase.Value);
                Console.WriteLine("ТЕСТ ПРОЙДЕН\n");
            }
            catch (Exception ex)
            {
                if (ex.HResult == testCase.ExpectedException)
                {
                    Console.WriteLine("ТЕСТ ПРОЙДЕН\n");
                }
                else
                {
                    Console.WriteLine("ТЕСТ НЕ ПРОЙДЕН\n");
                }
            }
        }
        static void ClearTheGraph(Node[] nodes)
        {
            foreach (Node node in nodes)
            {
                node.Visited = false;
            }
        }
    }
    enum ExceptionHResult
    {
        NoException = 0,
        UnexistentNodeException = -2147483646
    }
}
