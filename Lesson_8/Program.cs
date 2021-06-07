using System;

namespace Lesson_8
{
    class Program
    {
        public class TestCase
        {
            public int[] Array { get; set; }
            public int ExpectedException { get; set; }
            public TestCase(int[] _Array, int _ExpectedException)
            {
                Array = _Array;
                ExpectedException = _ExpectedException;
            }
        }
        static void Main(string[] args)
        {
            var rnd = new Random();

            int[] array_1 = new int[20];
            int[] array_2 = new int[10000];
            for (int i = 0; i < array_1.Length; i++)
            {
                array_1[i] = rnd.Next(100);
            }
            for (int i = 0; i < array_2.Length; i++)
            {
                array_2[i] = rnd.Next(1000000);
            }
            TestBucketSort(new TestCase(array_1, (int)ExceptionHResult.NoException));
            TestBucketSort(new TestCase(array_2, (int)ExceptionHResult.NoException));
        }
        static void TestBucketSort(TestCase testCase)
        {
            try
            {
                var newArray = BucketSortClass.Sort(testCase.Array);
                int controlValue = 0;
                for (int i = 0; i < newArray.Length; i++)
                {
                    if (newArray[i] < controlValue)
                    {
                        Console.WriteLine("ТЕСТ НЕ ПРОЙДЕН");
                        return;
                    }
                    controlValue = newArray[i];
                }
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
        NoException = 0
    }
}
