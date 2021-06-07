namespace Lesson_8
{
    class Block
    {
        public int[] array;
        public int min;
        public int max;
        public Block next = null;
        public Block(int _min, int _max)
        {
            min = _min;
            max = _max;
            array = new int[0];
        }
        public void AddValue(int value)
        {
            int[] new_array = new int[array.Length + 1];
            array.CopyTo(new_array, 0);
            new_array[new_array.Length - 1] = value;
            array = new_array;
        }
    }
    static class BucketSortClass
    {
        static public int[] Sort(int[] array)
        {
            var resultArray = new int[array.Length];
            var maxValue = GetMaxValue(array);
            var startBlock = GetBlocks(maxValue);
            FillBlocks(startBlock, array);
            BlockSort(startBlock);
            BlockMerge(startBlock, resultArray);
            return resultArray;
        }
        static private int GetMaxValue(int[] array)
        {
            var max = 0;
            for (int j = 0; j < array.Length; j++)
            {
                if (array[j] > max)
                {
                    max = array[j];
                }
            }
            return max;
        }
        static private Block GetBlocks(int max)
        {
            var i = 0;
            var cap = max / 5;
            var startBlock = new Block(i * cap, (i + 1) * cap - 1);
            var activeBlock = startBlock;

            while ((i + 1) * cap <= max)
            {
                i++;
                var block = new Block(i * cap, (i + 1) * cap - 1);
                activeBlock.next = block;
                activeBlock = block;
            }
            
            return startBlock;
        }
        static private void FillBlocks(Block startBlock, int[] array)
        {
            var activeBlock = startBlock;
            for (int j = 0; j < array.Length; j++)
            {
                while (activeBlock != null)
                {
                    if (array[j] >= activeBlock.min && array[j] <= activeBlock.max)
                    {
                        activeBlock.AddValue(array[j]);
                        break;
                    }
                    activeBlock = activeBlock.next;
                }
                activeBlock = startBlock;
            }
        }
        static private void BlockSort(Block startBlock)
        {
            var activeBlock = startBlock;
            while (activeBlock != null)
            {
                if (activeBlock.array.Length > 0)
                {
                    QuickSort(activeBlock.array, 0, activeBlock.array.Length - 1);
                }
                activeBlock = activeBlock.next;
            }
        }
        static private void QuickSort(int[] array, int first, int last)
        {
            int i = first, j = last, x = array[(first + last) / 2];

            do
            {
                while (array[i] < x)
                    i++;
                while (array[j] > x)
                    j--;

                if (i <= j)
                {
                    if (array[i] > array[j])
                    {
                        var tmp = array[i];
                        array[i] = array[j];
                        array[j] = tmp;
                    }

                    i++;
                    j--;
                }
            } while (i <= j);

            if (i < last)
                QuickSort(array, i, last);
            if (first < j)
                QuickSort(array, first, j);
        }
        static private void BlockMerge(Block startBlock, int[] array)
        {
            var activeBlock = startBlock;
            var i = 0;
                while (activeBlock != null)
                {
                activeBlock.array.CopyTo(array, i);
                i += activeBlock.array.Length;
                activeBlock = activeBlock.next;
                }
        }
    }
}
