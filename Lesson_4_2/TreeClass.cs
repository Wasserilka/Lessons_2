using System;
using System.Collections.Generic;

namespace Lesson_4_2
{
    public class AlreadyExistentNodeException : Exception
    {
        public AlreadyExistentNodeException()
        {
            HResult = (int)ExceptionHResult.AlreadyExistentNodeException;
        }
    }
    public class UnexistentNodeException : Exception
    {
        public UnexistentNodeException()
        {
            HResult = (int)ExceptionHResult.UnexistentNodeException;
        }
    }
    public class TreeNode
    {
        public int Value { get; set; }
        public TreeNode LeftChild { get; set; }
        public TreeNode RightChild { get; set; }
        public TreeNode(int _Value)
        {
            Value = _Value;
        }

        public override bool Equals(object obj)
        {
            var node = obj as TreeNode;

            if (node == null)
                return false;

            return node.Value == Value;
        }
    }
    public interface ITree
    {
        TreeNode GetRoot();
        void AddItem(int value); // добавить узел
        void RemoveItem(int value); // удалить узел по значению
        TreeNode GetNodeByValue(int value); //получить узел дерева по значению
        void PrintTree(); //вывести дерево в консоль
    }
    public class Tree : ITree
    {
        private TreeNode Root = null;
        public void AddItem(int value)
        {
            if (Root == null)
            {
                Root = new TreeNode(value);
                return;
            }
            var activeNode = Root;
            while (true)
            {
                if (value < activeNode.Value)
                {
                    if (activeNode.LeftChild == null)
                    {
                        activeNode.LeftChild = new TreeNode(value);
                        return;
                    }
                    activeNode = activeNode.LeftChild;
                }
                else if (value == activeNode.Value)
                {
                    throw new AlreadyExistentNodeException();
                }
                else
                {
                    if (activeNode.RightChild == null)
                    {
                        activeNode.RightChild = new TreeNode(value);
                        return;
                    }
                    activeNode = activeNode.RightChild;
                }
            }
        }

        public TreeNode GetNodeByValue(int value)
        {
            var activeNode = Root;
            while (true)
            {
                if (value < activeNode.Value)
                {
                    if (activeNode.LeftChild == null)
                    {
                        throw new UnexistentNodeException();
                    }
                    activeNode = activeNode.LeftChild;
                }
                else if (value == activeNode.Value)
                {
                    return activeNode;
                }
                else
                {
                    if (activeNode.RightChild == null)
                    {
                        throw new UnexistentNodeException();
                    }
                    activeNode = activeNode.RightChild;
                }
            }
        }

        public TreeNode GetRoot()
        {
            return Root;
        }

        public void PrintTree()
        {
            Console.WriteLine(IsBalanced() ? "Сбалансированное дерево" : "Несбалансированное дерево");
            BTreePrinter.PrintTree(Root); //взято с https://stackoverflow.com/questions/36311991/c-sharp-display-a-binary-search-tree-in-console
        }
        public void RemoveItem(int value)
        {
            var activeNode = Root;
            while (true)
            {
                if (value < activeNode.Value)
                {
                    if (activeNode.LeftChild == null)
                    {
                        throw new UnexistentNodeException();
                    }
                    if (activeNode.LeftChild.Value == value)
                    {
                        activeNode.LeftChild = null;
                        return;
                    }
                    activeNode = activeNode.LeftChild;
                }
                else if (value == activeNode.Value)
                {
                    Root = null;
                    return;
                }
                else
                {
                    if (activeNode.RightChild == null)
                    {
                        throw new UnexistentNodeException();
                    }
                    if (activeNode.RightChild.Value == value)
                    {
                        activeNode.RightChild = null;
                        return;
                    }
                    activeNode = activeNode.RightChild;
                }
            }
        }
        public bool IsBalanced()
        {
            var minDepth = 0;
            var maxDepth = 0;
            foreach (NodeInfo node in TreeHelper.GetRootInLine(Root))
            {
                if (node.Node.LeftChild == null && node.Node.RightChild == null)
                {
                    if (minDepth == 0 || node.Depth < minDepth)
                    {
                        minDepth = node.Depth;
                    }
                    if (maxDepth == 0 || node.Depth > maxDepth)
                    {
                        maxDepth = node.Depth;
                    }
                }
            }
            return maxDepth - minDepth <= 1;
        }
    }
    public static class TreeHelper
    {
        public static NodeInfo[] GetRootInLine(TreeNode _root)
        {
            var bufer = new Queue<NodeInfo>();
            var returnArray = new List<NodeInfo>();
            var root = new NodeInfo() { Node = _root };
            bufer.Enqueue(root);

            while (bufer.Count != 0)
            {
                var element = bufer.Dequeue();
                returnArray.Add(element);

                var depth = element.Depth + 1;

                if (element.Node.LeftChild != null)
                {
                    var left = new NodeInfo()
                    {
                        Node = element.Node.LeftChild,
                        Depth = depth,
                    };
                    bufer.Enqueue(left);
                }
                if (element.Node.RightChild != null)
                {
                    var right = new NodeInfo()
                    {
                        Node = element.Node.RightChild,
                        Depth = depth,
                    };
                    bufer.Enqueue(right);
                }
            }

            return returnArray.ToArray();
        }
    }
    public class NodeInfo
    {
        public int Depth { get; set; }
        public TreeNode Node { get; set; }
    }
    public static class BTreePrinter
    {
        class NodeInfo
        {
            public TreeNode Node;
            public string Text;
            public int StartPos;
            public int Size { get { return Text.Length; } }
            public int EndPos { get { return StartPos + Size; } set { StartPos = value - Size; } }
            public NodeInfo Parent, Left, Right;
        }
        public static void PrintTree(this TreeNode root, int topMargin = 1, int leftMargin = 2)
        {
            if (root == null) return;
            int rootTop = Console.CursorTop + topMargin;
            var last = new List<NodeInfo>();
            var next = root;
            for (int level = 0; next != null; level++)
            {
                var item = new NodeInfo { Node = next, Text = next.Value.ToString(" 0 ") };
                if (level < last.Count)
                {
                    item.StartPos = last[level].EndPos + 1;
                    last[level] = item;
                }
                else
                {
                    item.StartPos = leftMargin;
                    last.Add(item);
                }
                if (level > 0)
                {
                    item.Parent = last[level - 1];
                    if (next == item.Parent.Node.LeftChild)
                    {
                        item.Parent.Left = item;
                        item.EndPos = Math.Max(item.EndPos, item.Parent.StartPos);
                    }
                    else
                    {
                        item.Parent.Right = item;
                        item.StartPos = Math.Max(item.StartPos, item.Parent.EndPos);
                    }
                }
                next = next.LeftChild ?? next.RightChild;
                for (; next == null; item = item.Parent)
                {
                    Print(item, rootTop + 2 * level);
                    if (--level < 0) break;
                    if (item == item.Parent.Left)
                    {
                        item.Parent.StartPos = item.EndPos;
                        next = item.Parent.Node.RightChild;
                    }
                    else
                    {
                        if (item.Parent.Left == null)
                            item.Parent.EndPos = item.StartPos;
                        else
                            item.Parent.StartPos += (item.StartPos - item.Parent.EndPos) / 2;
                    }
                }
            }
            Console.SetCursorPosition(0, rootTop + 2 * last.Count - 1);
        }

        private static void Print(NodeInfo item, int top)
        {
            SwapColors();
            Print(item.Text, top, item.StartPos);
            SwapColors();
            if (item.Left != null)
                PrintLink(top + 1, "┌", "┘", item.Left.StartPos + item.Left.Size / 2, item.StartPos);
            if (item.Right != null)
                PrintLink(top + 1, "└", "┐", item.EndPos - 1, item.Right.StartPos + item.Right.Size / 2);
        }

        private static void PrintLink(int top, string start, string end, int startPos, int endPos)
        {
            Print(start, top, startPos);
            Print("─", top, startPos + 1, endPos);
            Print(end, top, endPos);
        }

        private static void Print(string s, int top, int left, int right = -1)
        {
            Console.SetCursorPosition(left, top);
            if (right < 0) right = left + s.Length;
            while (Console.CursorLeft < right) Console.Write(s);
        }

        private static void SwapColors()
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = Console.BackgroundColor;
            Console.BackgroundColor = color;
        }
    }
}
