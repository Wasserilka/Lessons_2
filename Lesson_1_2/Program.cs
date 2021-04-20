using System;

namespace Lesson_1_2
{
    class Program
    {
        static void Main(string[] args)
        {

        }
        public static int StrangeSum(int[] inputArray)
        {
            int sum = 0; //O(1)
            for (int i = 0; i < inputArray.Length; i++) //O(N)
            {
                for (int j = 0; j < inputArray.Length; j++) //O(N)
                {
                for (int k = 0; k < inputArray.Length; k++) //O(N)
                    {
                        int y = 0; //O(1)
                        if (j != 0) //O(1)
                        {
                            y = k / j; //O(1)
                        }
                        sum += inputArray[i] + i + k + j + y; //O(1)
                    }
                }
            }
            return sum; //O(1)
        }
        //O(1 + N * N * N * (1 + 1 + 1 + 1) + 1) = O(2 + 4 * N ^ 3) = O(N ^ 3)
    }
}
