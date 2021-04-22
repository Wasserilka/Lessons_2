using System;

namespace Lesson_2
{
    class Program
    {
            public class TestCase
            {
                public int Value { get; set; }
                public int ExpectedException { get; set; }
                public TestCase(int _Value,  int _ExpectedException)
                {
                    Value = _Value;
                    ExpectedException = _ExpectedException;
                }
            }
        public class TestCaseNode
        {
            public int Value { get; set; }
            public int ExpectedValue { get; set; }
            public int ExpectedException { get; set; }
            public TestCaseNode(int _Value, int _ExpectedValue, int _ExpectedException)
            {
                Value = _Value;
                ExpectedValue = _ExpectedValue;
                ExpectedException = _ExpectedException;
            }
        }
        static void Main(string[] args)
        {
            var NewLinkedList = new LinkedList();
            TestCount(new TestCase(0, (int)ExceptionHResult.NoException), NewLinkedList);
            TestCount(new TestCase(4, (int)ExceptionHResult.NoException), NewLinkedList); //не пройден
            Console.WriteLine();
            TestAddNodeAfter(new TestCaseNode(12, 10, (int)ExceptionHResult.BlankListException), NewLinkedList); //тест на пустой список
            TestAddNodeAfter(new TestCaseNode(12, 10, (int)ExceptionHResult.NoException), NewLinkedList); //тест на пустой список, не пройден
            Console.WriteLine();
            TestAddNode(new TestCase(10, (int)ExceptionHResult.NoException), NewLinkedList);
            TestAddNode(new TestCase(20, (int)ExceptionHResult.NoException), NewLinkedList);
            TestAddNode(new TestCase(30, (int)ExceptionHResult.NoException), NewLinkedList);
            Console.WriteLine();
            TestAddNodeAfter(new TestCaseNode(12, 10, (int)ExceptionHResult.NoException), NewLinkedList);
            TestAddNodeAfter(new TestCaseNode(14, 12, (int)ExceptionHResult.NoException), NewLinkedList);
            TestAddNodeAfter(new TestCaseNode(18, 16, (int)ExceptionHResult.UnexistentNodeException), NewLinkedList);
            TestAddNodeAfter(new TestCaseNode(18, 16, (int)ExceptionHResult.NoException), NewLinkedList); //не пройден
            Console.WriteLine();
            TestFindNode(new TestCase(10, (int)ExceptionHResult.NoException), NewLinkedList);
            TestFindNode(new TestCase(30, (int)ExceptionHResult.NoException), NewLinkedList);
            TestFindNode(new TestCase(12, (int)ExceptionHResult.NoException), NewLinkedList);
            TestFindNode(new TestCase(5, (int)ExceptionHResult.UnexistentNodeException), NewLinkedList);
            TestFindNode(new TestCase(5, (int)ExceptionHResult.NoException), NewLinkedList); //не пройден
            Console.WriteLine();
            TestRemoveNode(new TestCase(12, (int)ExceptionHResult.NoException), NewLinkedList);
            TestRemoveNode(new TestCase(10, (int)ExceptionHResult.NoException), NewLinkedList);
            TestRemoveNode(new TestCase(30, (int)ExceptionHResult.NoException), NewLinkedList);
            TestRemoveNode(new TestCase(30, (int)ExceptionHResult.UnexistentNodeException), NewLinkedList);
            TestRemoveNode(new TestCase(30, (int)ExceptionHResult.NoException), NewLinkedList); //не пройден
            Console.WriteLine();
            TestRemoveNodeFromIndex(new TestCase(1, (int)ExceptionHResult.NoException), NewLinkedList);
            TestRemoveNodeFromIndex(new TestCase(5, (int)ExceptionHResult.NoException), NewLinkedList); //не пройден
            TestRemoveNodeFromIndex(new TestCase(0, (int)ExceptionHResult.NoException), NewLinkedList);
            TestRemoveNodeFromIndex(new TestCase(0, (int)ExceptionHResult.NoException), NewLinkedList); //не пройден
            TestRemoveNodeFromIndex(new TestCase(0, (int)ExceptionHResult.IndexOutOfRangeException), NewLinkedList);
        }
        static void TestCount(TestCase testCase, LinkedList linkedList)
        {
            try
            {
                var result = linkedList.GetCount();

                if (result == testCase.Value)
                {
                    Console.WriteLine("ТЕСТ ПРОЙДЕН");
                }
                else
                {
                    Console.WriteLine("ТЕСТ НЕ ПРОЙДЕН");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("ТЕСТ НЕ ПРОЙДЕН");
            }
        }
        static void TestAddNode(TestCase testCase, LinkedList linkedList)
        {
            try
            {
                linkedList.AddNode(testCase.Value);
                var result = linkedList.GetEndNode().Value;

                if (result == testCase.Value)
                {
                    Console.WriteLine("ТЕСТ ПРОЙДЕН");
                }
                else
                {
                    Console.WriteLine("ТЕСТ НЕ ПРОЙДЕН");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("ТЕСТ НЕ ПРОЙДЕН");
            }
        }
        static void TestAddNodeAfter(TestCaseNode testCase, LinkedList linkedList)
        {
            try
            {
                var prevNode = linkedList.FindNode(testCase.ExpectedValue);
                linkedList.AddNodeAfter(prevNode, testCase.Value);

                if (prevNode.NextNode.Value == testCase.Value)
                {
                    Console.WriteLine("ТЕСТ ПРОЙДЕН");
                }
                else
                {
                    Console.WriteLine("ТЕСТ НЕ ПРОЙДЕН");
                }
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
        static void TestFindNode(TestCase testCase, LinkedList linkedList)
        {
            try
            {
                linkedList.FindNode(testCase.Value);

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
        static void TestRemoveNode(TestCase testCase, LinkedList linkedList)
        {
            try
            {
                var delNode = linkedList.FindNode(testCase.Value);
                var nextNode = delNode.NextNode;
                var prevNode = delNode.PrevNode;
                linkedList.RemoveNode(delNode);

                if (delNode.NextNode == null && delNode.PrevNode == null && (nextNode == null || nextNode.PrevNode == prevNode) && (prevNode == null || prevNode.NextNode == nextNode))
                {
                    Console.WriteLine("ТЕСТ ПРОЙДЕН");
                }
                else
                {
                    Console.WriteLine("ТЕСТ НЕ ПРОЙДЕН");
                }
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
        static void TestRemoveNodeFromIndex(TestCase testCase, LinkedList linkedList)
        {
            try
            {
                var delNode = linkedList.FindNodeFromIndex(testCase.Value);
                var nextNode = delNode.NextNode;
                var prevNode = delNode.PrevNode;
                linkedList.RemoveNode(testCase.Value);

                if (delNode.NextNode == null && delNode.PrevNode == null && (nextNode == null || nextNode.PrevNode == prevNode) && (prevNode == null || prevNode.NextNode == nextNode))
                {
                    Console.WriteLine("ТЕСТ ПРОЙДЕН");
                }
                else
                {
                    Console.WriteLine("ТЕСТ НЕ ПРОЙДЕН");
                }
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
        BlankListException = -2147483647,
        UnexistentNodeException = -2147483646,
        IndexOutOfRangeException = -2146233080
    }
}
