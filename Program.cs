using System;

namespace QuickSortDemo
{
    class Program
    {
        private static Random random = new Random((Int32)DateTime.Now.Ticks);

        private static Double[] Generate(Int32 size, Double min = Double.MinValue, Double max = Double.MaxValue)
        {
            if (size <= 0)
                throw new ArgumentException("size must be greater then zero", nameof(size));

            var array = new Double[size];
            for (var i = 0; i < size; ++i)
                array[i] = min + random.NextDouble() * (max - min);
            return array;
        }

        private static void Print(Double[] array, String message = null) =>
            Console.WriteLine($"\t{message}{Environment.NewLine}{String.Join(Environment.NewLine, array)}{Environment.NewLine}");
        

        private static void Swap(Double[] array, Int32 i, Int32 j)
        {
            var temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

        private static void QuickSort(Double[] array, Int32 start, Int32 finish)
        {
            if (finish - start < 2)
                return;

            var pivot = random.Next(start, finish);
            Swap(array, start, pivot);
            var last = start;
            for (var index = start; index < finish; ++index)
                if (array[index] < array[start])
                    Swap(array, ++last, index);
            Swap(array, start, last);
            QuickSort(array, start, last);
            QuickSort(array, last + 1, finish);
        }

        private static void Main()
        {
            var array = Generate(5, -10, 10);
            Print(array, "before:");
            QuickSort(array, 0, array.Length);
            Print(array, "after:");
        }
    }
}
