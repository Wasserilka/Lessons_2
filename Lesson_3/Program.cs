using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Lesson_3
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
        }
    }
    public class PointClass
    {
        public int X;
        public int Y;
    }
    public struct PointStruct
    {
        public int X;
        public int Y;
    }
    public class BechmarkClass
    {
        static PointClass[] PointClassArray = {
                new PointClass{ X = -1, Y = 11},
                new PointClass{ X = 2, Y = 0},
                new PointClass{ X = 4, Y = -7},
                new PointClass{ X = 8, Y = 13},
                new PointClass{ X = 10, Y = 5} };
        static PointStruct[] PointStructArray = {
                new PointStruct{ X = -1, Y = 11},
                new PointStruct{ X = 2, Y = 0},
                new PointStruct{ X = 4, Y = -7},
                new PointStruct{ X = 8, Y = 13},
                new PointStruct{ X = 10, Y = 5} };
        public float PointDistanceStruct(PointStruct pointOne, PointStruct pointTwo)
        {
            float x = pointOne.X - pointTwo.X;
            float y = pointOne.Y - pointTwo.Y;
            return MathF.Sqrt((x * x) + (y * y));
        }
        public float PointDistanceClass(PointClass pointOne, PointClass pointTwo)
        {
            float x = pointOne.X - pointTwo.X;
            float y = pointOne.Y - pointTwo.Y;
            return MathF.Sqrt((x * x) + (y * y));
        }
        public double PointDistanceDouble(PointStruct pointOne, PointStruct pointTwo)
        {
            double x = pointOne.X - pointTwo.X;
            double y = pointOne.Y - pointTwo.Y;
            return Math.Sqrt((x * x) + (y * y));
        }
        public float PointDistanceShort(PointStruct pointOne, PointStruct pointTwo)
        {
            float x = pointOne.X - pointTwo.X;
            float y = pointOne.Y - pointTwo.Y;
            return (x * x) + (y * y);
        }
        [Benchmark]
        public void TestPointDistanceStruct()
        {
            try
            {
                foreach (PointStruct point_1 in PointStructArray)
                {
                    foreach (PointStruct point_2 in PointStructArray)
                    {
                        PointDistanceStruct(point_1, point_2);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Benchmark]
        public void TestPointDistanceClass()
        {
            try
            {
                foreach (PointClass point_1 in PointClassArray)
                {
                    foreach (PointClass point_2 in PointClassArray)
                    {
                        PointDistanceClass(point_1, point_2);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Benchmark]
        public void TestPointDistanceDouble()
        {
            try
            {
                foreach (PointStruct point_1 in PointStructArray)
                {
                    foreach (PointStruct point_2 in PointStructArray)
                    {
                        PointDistanceDouble(point_1, point_2);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Benchmark]
        public void PointDistanceShort()
        {
            try
            {
                foreach (PointStruct point_1 in PointStructArray)
                {
                    foreach (PointStruct point_2 in PointStructArray)
                    {
                        PointDistanceShort(point_1, point_2);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
