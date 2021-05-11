using System;

namespace Lesson_7
{
    class Program
    {
        public class TestCase
        {
            public int[,] Map { get; set; }
            public int N { get; set; }
            public int M { get; set; }
            public int Expected { get; set; }
            public int ExpectedException { get; set; }
            public TestCase(int[,] _Map, int _N, int _M, int _Expected, int _ExpectedException)
            {
                Map = _Map;
                N = _N;
                M = _M;
                Expected = _Expected;
                ExpectedException = _ExpectedException;
            }
        }
        public class TestCaseMap
        {
            public int K { get; set; }
            public int N { get; set; }
            public int M { get; set; }
            public int ExpectedException { get; set; }
            public TestCaseMap(int _N, int _M, int _K, int _ExpectedException)
            {
                K = _K;
                N = _N;
                M = _M;
                ExpectedException = _ExpectedException;
            }
        }
        static void Main(string[] args)
        {
            TestGetMap(new TestCaseMap(10, 10, 4, (int)ExceptionHResult.NoException));
            TestGetMap(new TestCaseMap(10, 10, 101, (int)ExceptionHResult.NoException));
            TestGetMap(new TestCaseMap(10, 10, 101, (int)ExceptionHResult.StackOverFlowException));
            Console.WriteLine();
            TestGetFieldPath(new TestCase(GetMap(10, 10, 0), 10, 10, 48620, (int)ExceptionHResult.NoException));
            TestGetFieldPath(new TestCase(GetMap(10, 10, 4), 10, 10, 48620, (int)ExceptionHResult.NoException));
            TestGetFieldPath(new TestCase(GetMap(10, 10, 4), 10, 10, 0, (int)ExceptionHResult.NoException));
        }
        static int[,] GetMap(int N, int M, int K)
        {
            if (K > N * M)
            {
                throw new StackOverflowException();
            }

            int[,] map = new int[N, M];
            var rnd = new Random();
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    map[i, j] = 1;
                }
            }
            for (int p = 0; p < K; p++)
            {
                while (true)
                {
                    int x = rnd.Next(N);
                    int y = rnd.Next(M);
                    if (map[x, y] == 1)
                    {
                        map[x, y] = 0;
                        break;
                    }
                }
            }
            return map;
        }
        static int[,] GetField(int[,] map, int N, int M)
        {
            int[,] field = new int[N, M];
            for (int j = 0; j < M; j++)
            {
                field[0, j] = map[0, j] == 1 ? 1 : 0;
            }
            for (int i = 1; i < N; i++)
            {
                field[i, 0] = map[i, 0] == 1 ? 1 : 0;
                for (int j = 1; j < M; j++)
                {
                    field[i, j] = map[i, j] == 1 ? field[i, j - 1] + field[i - 1, j] : 0;
                }
            }
            return field;
        }
        static int GetFieldPath(int[,] field, int n, int m)
        {
            return field[n - 1, m - 1];
        }
        static void TestGetMap(TestCaseMap testCase)
        {
            try
            {
                var resultMap = GetMap(testCase.N, testCase.M, testCase.K);

                var k = 0;
                for (int i = 0; i < testCase.N; i++)
                {
                    for (int j = 0; j < testCase.M; j++)
                    {
                        if (resultMap[i,j] == 0)
                        {
                            k++;
                        }
                    }
                }

                if (k == testCase.K)
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
        static void TestGetFieldPath(TestCase testCase)
        {
            try
            {
                var resultField = GetField(testCase.Map, testCase.N, testCase.M);
                var resultPath = GetFieldPath(resultField, testCase.N, testCase.M);
                Console.WriteLine($"Количество путей: {resultPath}");

                if (testCase.Expected <= resultPath)
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
        StackOverFlowException = -2147023895
    }
}
