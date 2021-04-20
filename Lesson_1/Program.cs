using System;

namespace Lesson_1
{
    class Program
    {
        public class TestCase
        {
            public int N { get; set; }
            public bool Expected { get; set; }
            public int ExpectedException { get; set; }
            public TestCase(int _N, bool _Expected, int _ExpectedException)
            {
                N = _N;
                Expected = _Expected;
                ExpectedException = _ExpectedException;
            }
        }
        static void Main(string[] args)
        {
            TestIsPrime(new TestCase(2, true, (int)ExceptionHResult.NoException));
            TestIsPrime(new TestCase(5, true, (int)ExceptionHResult.NoException));
            TestIsPrime(new TestCase(10, false, (int)ExceptionHResult.NoException));
            TestIsPrime(new TestCase(-5, false, (int)ExceptionHResult.ArgumentException));
            TestIsPrime(new TestCase(4, true, (int)ExceptionHResult.NoException));
            TestIsPrime(new TestCase(11, false, (int)ExceptionHResult.NoException));
            TestIsPrime(new TestCase(-10, false, (int)ExceptionHResult.NoException));
        }
        static bool IsPrime(int n)
        {
            if (n < 0)
            {
                throw new ArgumentException("Число должно быть не меньше нуля");
            }

            var d = 0;
            var i = 2;
            while(i<n)
            {
                if (n % i == 0)
                {
                    d++;
                }
                i++;
            }
            if (d==0)
            {
                return true;
            }
            return false;
        }
        static void TestIsPrime(TestCase testCase)
        {
            try
            {
                var result = IsPrime(testCase.N);

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
        NoException = 0,
        ArgumentException = -2147024809
    }
}
