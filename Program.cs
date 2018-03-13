using System;

namespace QuickSortDemo
{
    internal class Program
    {
        private static readonly Random random = new Random((Int32)DateTime.Now.Ticks);

        private static Double[] Generate(Int32 size, Double min = Double.MinValue, Double max = Double.MaxValue)
        {
            if (size <= 0)
                throw new ArgumentException("Size of an array must be greater then zero.", nameof(size));

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

        private static void QuickSort(Double[] array) =>
            QuickSortBody(array, 0, array.Length);

        private static void QuickSortBody(Double[] array, Int32 start, Int32 finish)
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
            QuickSortBody(array, start, last);
            QuickSortBody(array, last + 1, finish);
        }

#if UNSAFE
        private static unsafe Double* UnsafeGenerate(Int32 size, Double min = Double.MinValue, Double max = Double.MaxValue)
        {
            fixed (Double* ptr = Generate(size, min, max))
            {
                return ptr;
            }
        }

        private static unsafe void UnsafePrint(Double* array, Int32 size, String message)
        {
            if (size <= 0)
                return;

            Console.WriteLine($"\n{message}");
            for (var i = 0; i < size; ++i)
                Console.WriteLine($"\t{array[i]}");
            Console.WriteLine();
        }

        private static unsafe void UnsafeSwap(Double* array, Int32 i, Int32 j)
        {
            var temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }


        private static unsafe void UnsafeQuickSort(Double* array, Int32 size)
        {
            if (size < 2)
                return;

            var pivot = random.Next(size);
            UnsafeSwap(array, 0, pivot);
            var last = 0;
            for (var index = 0; index < size; ++index)
                if (array[index] < array[0])
                    UnsafeSwap(array, ++last, index);
            UnsafeSwap(array, 0, last);
            UnsafeQuickSort(array, last);
            UnsafeQuickSort(array + last + 1, size - last - 1);
        }
#endif

        private static void Main()
        {
            const Int32 size = 5;
            const Double min = -10;
            const Double max = 10;

#if UNSAFE
            unsafe
            {
                var ptr = UnsafeGenerate(size, min, max);
                UnsafePrint(ptr, size, "before:");
                UnsafeQuickSort(ptr, size);
                UnsafePrint(ptr, size, "after:");
            }
#else
            var array = Generate(size, min, max);
            Print(array, "before:");
            QuickSort(array);
            Print(array, "after:");
#endif
        }
    }
}
