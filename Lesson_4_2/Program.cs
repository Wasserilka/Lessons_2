using System;

namespace Lesson_4_2
{
    class Program
    {
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
            var rnd = new Random();
            var root = rnd.Next(10, 21);
            MyTree.AddItem(root);
            for (int i = 0; i < 30; i++)
            {
                try
                {
                    MyTree.AddItem(rnd.Next(0, 31));
                }
                catch (Exception)
                {
                }
            }
            MyTree.PrintTree();
            Console.WriteLine();

            TestGetRoot(new TestCase(MyTree, root, (int)ExceptionHResult.NoException));
            TestGetRoot(new TestCase(MyTree, 40, (int)ExceptionHResult.NoException));
            Console.WriteLine();
            TestAddItem(new TestCase(MyTree, 40, (int)ExceptionHResult.NoException));
            TestAddItem(new TestCase(MyTree, 40, (int)ExceptionHResult.NoException));
            TestAddItem(new TestCase(MyTree, 40, (int)ExceptionHResult.AlreadyExistentNodeException));
            Console.WriteLine();
            TestRemoveItem(new TestCase(MyTree, 40, (int)ExceptionHResult.NoException));
            TestRemoveItem(new TestCase(MyTree, 40, (int)ExceptionHResult.NoException));
            TestRemoveItem(new TestCase(MyTree, 40, (int)ExceptionHResult.UnexistentNodeException));
            Console.WriteLine();
            TestGetNodeByValue(new TestCase(MyTree, root, (int)ExceptionHResult.NoException));
            TestGetNodeByValue(new TestCase(MyTree, 40, (int)ExceptionHResult.NoException));
            TestGetNodeByValue(new TestCase(MyTree, 40, (int)ExceptionHResult.UnexistentNodeException));
        }
        static void TestGetRoot(TestCase testCase)
        {
            try
            {
                var result = testCase.MyTree.GetRoot().Value;

                if (result == testCase.Value)
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
        static void TestAddItem(TestCase testCase)
        {
            try
            {
                testCase.MyTree.AddItem(testCase.Value);
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
        static void TestRemoveItem(TestCase testCase)
        {
            try
            {
                testCase.MyTree.RemoveItem(testCase.Value);
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
        static void TestGetNodeByValue(TestCase testCase)
        {
            try
            {
                var result = testCase.MyTree.GetNodeByValue(testCase.Value);

                if (result.Value == testCase.Value)
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
        AlreadyExistentNodeException = -2147483647,
        UnexistentNodeException = -2147483646
    }
}
