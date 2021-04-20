using System;

namespace Lesson_1_3
{
    class Program
    {
        public class TestCase
        {
            public int N { get; set; }
            public int Expected { get; set; }
            public int ExpectedException { get; set; }
            public TestCase(int _N, int _Expected, int _ExpectedException)
            {
                N = _N;
                Expected = _Expected;
                ExpectedException = _ExpectedException;
            }
        }
        static void Main(string[] args)
        {
            TestFibonacciRecurse(new TestCase(1, 1, (int)ExceptionHResult.NoException));
            TestFibonacciRecurse(new TestCase(2, 1, (int)ExceptionHResult.NoException));
            TestFibonacciRecurse(new TestCase(8, 21, (int)ExceptionHResult.NoException));
            TestFibonacciRecurse(new TestCase(-6, -8, (int)ExceptionHResult.NoException));
            TestFibonacciRecurse(new TestCase(5, 11, (int)ExceptionHResult.NoException));
            TestFibonacciRecurse(new TestCase(-5, -5, (int)ExceptionHResult.NoException));
            Console.WriteLine();
            TestFibonacciLoop(new TestCase(1, 1, (int)ExceptionHResult.NoException));
            TestFibonacciLoop(new TestCase(2, 1, (int)ExceptionHResult.NoException));
            TestFibonacciLoop(new TestCase(8, 21, (int)ExceptionHResult.NoException));
            TestFibonacciLoop(new TestCase(-6, -8, (int)ExceptionHResult.NoException));
            TestFibonacciLoop(new TestCase(5, 11, (int)ExceptionHResult.NoException));
            TestFibonacciLoop(new TestCase(-5, -5, (int)ExceptionHResult.NoException));
        }
        static int FibonacciRecurse(int n)
        {
            return (int)Math.Pow(Math.Sign(n), n + 1) * FibonacciRecurse(Math.Abs(n), out _);
        }
        static int FibonacciRecurse(int n, out int n1)
        {
            n1 = 0;
            if (n == 0)
            {
                return 0;
            }
            else if (n == 1)
            {
                return 1;
            }
            else
            {
                int n2;
                n1 = FibonacciRecurse(n - 1, out n2);
                return n1 + n2;
            }
        }
        static int FibonacciLoop(int n)
        {
            var nresult = 0;
            if (n == 0)
            {
                nresult = 0;
            }
            else if (n == 1)
            {
                nresult = 1;
            }
            else
            {
                for (int i = 0, n1, n2 = 1; i < Math.Abs(n); i++)
                {
                    n1 = nresult;
                    nresult += n2;
                    n2 = n1;
                }
            }
            return (int)Math.Pow(Math.Sign(n), n + 1) * nresult;
        }
        static void TestFibonacciRecurse(TestCase testCase)
        {
            try
            {
                var result = FibonacciRecurse(testCase.N);

                if (result == testCase.Expected)
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
        static void TestFibonacciLoop(TestCase testCase)
        {
            try
            {
                var result = FibonacciLoop(testCase.N);

                if (result == testCase.Expected)
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
        NoException = 0
    }
}
