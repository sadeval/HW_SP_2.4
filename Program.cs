using System;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        int[] numbers = new int[1000000];
        Random random = new Random();
        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] = random.Next(0, 1000000);
        }

        Console.WriteLine("Начало сортировки...");

        MergeSort(numbers);

        Console.WriteLine("Сортировка завершена.");

        Console.WriteLine("Первые 50 элементов отсортированного списка:");
        for (int i = 0; i < 50; i++)
        {
            Console.Write(numbers[i] + " ");
        }
    }

    static void MergeSort(int[] array)
    {
        if (array.Length <= 1)
            return;

        int mid = array.Length / 2;

        int[] left = new int[mid];
        int[] right = new int[array.Length - mid];

        Array.Copy(array, 0, left, 0, mid);
        Array.Copy(array, mid, right, 0, array.Length - mid);

        Task leftTask = Task.Run(() => MergeSort(left));
        Task rightTask = Task.Run(() => MergeSort(right));

        Task.WaitAll(leftTask, rightTask);

        Merge(array, left, right);
    }

    static void Merge(int[] array, int[] left, int[] right)
    {
        int leftIndex = 0, rightIndex = 0, resultIndex = 0;

        while (leftIndex < left.Length && rightIndex < right.Length)
        {
            if (left[leftIndex] <= right[rightIndex])
            {
                array[resultIndex] = left[leftIndex];
                leftIndex++;
            }
            else
            {
                array[resultIndex] = right[rightIndex];
                rightIndex++;
            }
            resultIndex++;
        }

        while (leftIndex < left.Length)
        {
            array[resultIndex] = left[leftIndex];
            leftIndex++;
            resultIndex++;
        }

        while (rightIndex < right.Length)
        {
            array[resultIndex] = right[rightIndex];
            rightIndex++;
            resultIndex++;
        }
    }
}
