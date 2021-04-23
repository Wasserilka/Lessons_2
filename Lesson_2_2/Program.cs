using System;

namespace Lesson_2_2
{
    public class UnexistentElementException : Exception
    {
        public UnexistentElementException()
        {
            HResult = (int)ExceptionHResult.UnexistentElementException;
        }
    }
    class Program
    {
        public class TestCase
        {
            public int[] Array { get; set; }
            public int Value { get; set; }
            public int Expected { get; set; }
            public int ExpectedException { get; set; }
            public TestCase(int[] _Array, int _Value, int _Expected, int _ExpectedException)
            {
                Array = _Array;
                Value = _Value;
                Expected = _Expected;
                ExpectedException = _ExpectedException;
            }
        }
        static void Main(string[] args)
        {
            TestBinarySearch(new TestCase(new[] { 12, 33, 2, 41, 15, 20, 7, 11 }, 41, 3, (int)ExceptionHResult.NoException));
            TestBinarySearch(new TestCase(new[] { 12, 33, 2, 41, 15, 20, 7, 11 }, 12, 0, (int)ExceptionHResult.NoException));
            TestBinarySearch(new TestCase(new[] { 12, 33, 2, 41, 15, 20, 7, 11 }, 109, 1, (int)ExceptionHResult.UnexistentElementException));
            TestBinarySearch(new TestCase(new[] { 12, 33, 2, 41, 15, 20, 7, 11 }, 33, 10, (int)ExceptionHResult.NoException));
            TestBinarySearch(new TestCase(new[] { 12, 33, 2, 41, 15, 20, 7, 11 }, 50, 2, (int)ExceptionHResult.NoException));
        }
        static void TestBinarySearch(TestCase testCase)
        {
            try
            {
                var result = BinarySearch(testCase.Array, testCase.Value);

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
        public static int BinarySearch(int[] inputArray, int searchValue)
        {
            int min = 0; 
            int max = inputArray.Length - 1; 
            while (min <= max) 
            {
                int mid = (min + max) / 2;
                if (searchValue == inputArray[mid]) 
                {
                    return mid; 
                }
                else if (searchValue < inputArray[mid]) 
                {
                    max = mid - 1; 
                }
                else
                {
                    min = mid + 1; 
                }
            }
            throw new UnexistentElementException();

            //Длина массива n уменьшается в два раза m количество раз, пока не достигнет 1 (в худшем случае)
            //n / 2 ^ m = 1
            //n = 2 ^ m
            //m = log(n)
            //Сложность алгоритма - O(Log(N))
        }
    }
    enum ExceptionHResult
    {
        NoException = 0,
        UnexistentElementException = -2147483646
    }
}
