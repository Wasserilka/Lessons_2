using System;
using System.Collections.Generic;

namespace Lesson_5
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
            public Tree MyTree;
            public int Value { get; set; }
            public int ExpectedException { get; set; }
            public TestCase(Tree _MyTree, int _Value, int _ExpectedException)
            {
                MyTree = _MyTree;
                Value = _Value;
                ExpectedException = _ExpectedException;
            }
        }
        static void Main(string[] args)
        {
            var MyTree = new Tree();
            var queue = new Queue<TreeNode>();
            var stack = new Stack<TreeNode>();

            FillTheTree(MyTree);
            MyTree.PrintTree();

            var treeHelper = TreeHelper.GetRootInLine(MyTree.GetRoot());
            var testNode = treeHelper[treeHelper.Length / 2].Node;

            TestBFS(new TestCase(MyTree, testNode.Value, (int)ExceptionHResult.NoException));
            TestBFS(new TestCase(MyTree, 20, (int)ExceptionHResult.NoException));
            TestBFS(new TestCase(MyTree, 20, (int)ExceptionHResult.UnexistentNodeException));

            TestDFS(new TestCase(MyTree, testNode.Value, (int)ExceptionHResult.NoException));
            TestDFS(new TestCase(MyTree, 20, (int)ExceptionHResult.NoException));
            TestDFS(new TestCase(MyTree, 20, (int)ExceptionHResult.UnexistentNodeException));
        }
        static void FillTheTree(Tree tree)
        {
            var rnd = new Random();
            var root = rnd.Next(5, 10);
            tree.AddItem(root);
            for (int i = 0; i < 15; i++)
            {
                tree.AddItem(rnd.Next(0, 16));
            }
        }
        static TreeNode DFS(TreeNode root, int value)
        {
            Console.WriteLine($"\nИскомый эдемент: {value}");
            var stack = new Stack<TreeNode>();
            stack.Push(root);
            Console.WriteLine($"Элемент {root.Value} добавлен в стек");
            while (stack.Count > 0)
            {
                var node = stack.Pop();
                Console.WriteLine($"Элемент {node.Value} взят из стека");
                if (node.Value == value)
                {
                    Console.WriteLine($"Элемент {node.Value} является искомым!");
                    return node;
                }
                if (node.RightChild != null)
                {
                    stack.Push(node.RightChild);
                    Console.WriteLine($"Элемент {node.RightChild.Value} добавлен в стек");
                }
                if (node.LeftChild != null)
                {
                    stack.Push(node.LeftChild);
                    Console.WriteLine($"Элемент {node.LeftChild.Value} добавлен в стек");
                }
            }
            Console.WriteLine($"Элемент {value} не был найден!");
            throw new UnexistentNodeException();
        }
        static TreeNode BFS(TreeNode root, int value)
        {
            Console.WriteLine($"\nИскомый эдемент: {value}");
            var queue = new Queue<TreeNode>();
            queue.Enqueue(root);
            Console.WriteLine($"Элемент {root.Value} добавлен в очередь");
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                Console.WriteLine($"Элемент {node.Value} взят из очереди");
                if (node.Value == value)
                {
                    Console.WriteLine($"Элемент {node.Value} является искомым!");
                    return node;
                }
                if (node.LeftChild != null)
                {
                    queue.Enqueue(node.LeftChild);
                    Console.WriteLine($"Элемент {node.LeftChild.Value} добавлен в очередь");
                }
                if (node.RightChild != null)
                {
                    queue.Enqueue(node.RightChild);
                    Console.WriteLine($"Элемент {node.RightChild.Value} добавлен в очередь");
                }
            }
            Console.WriteLine($"Элемент {value} не был найден!");
            throw new UnexistentNodeException();
        }
        static void TestBFS(TestCase testCase)
        {
            try
            {
                BFS(testCase.MyTree.GetRoot(), testCase.Value);
                Console.WriteLine("ТЕСТ ПРОЙДЕН");
            }
            catch (Exception ex)
            {
                if (ex.HResult == testCase.ExpectedException)
                {
                    Console.WriteLine("ТЕСТ ПРОЙДЕН");
                }
                else
                {
                    Console.WriteLine("ТЕСТ НЕ ПРОЙДЕН");
                }
            }
        }
        static void TestDFS(TestCase testCase)
        {
            try
            {
                DFS(testCase.MyTree.GetRoot(), testCase.Value);
                Console.WriteLine("ТЕСТ ПРОЙДЕН");
            }
            catch (Exception ex)
            {
                if (ex.HResult == testCase.ExpectedException)
                {
                    Console.WriteLine("ТЕСТ ПРОЙДЕН");
                }
                else
                {
                    Console.WriteLine("ТЕСТ НЕ ПРОЙДЕН");
                }
            }
        }
    }
    enum ExceptionHResult
    {
        NoException = 0,
        UnexistentNodeException = -2147483646
    }
}
