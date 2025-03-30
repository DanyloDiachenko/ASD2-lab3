using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

class QuickSortDemo
{
    static long comparisonsStandard = 0;
    static long comparisonsModified = 0;
    static long comparisonsThreePivots = 0;

    public static void Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Input file was not provided");
            return;
        }

        string inputFileName = args[0];
        if (!File.Exists(inputFileName))
        {
            Console.WriteLine("File was not found");
            return;
        }

        int[] arrOriginal;
        try
        {
            string[] lines = File.ReadAllLines(inputFileName);
            int n = int.Parse(lines[0]);
            arrOriginal = lines.Skip(1).Take(n).Select(int.Parse).ToArray();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error reading input file: " + ex.Message);
            return;
        }

        int[] arrStandard = (int[])arrOriginal.Clone();
        int[] arrModified = (int[])arrOriginal.Clone();
        int[] arrThreePivots = (int[])arrOriginal.Clone();

        QuickSortStandard(arrStandard, 0, arrStandard.Length - 1);
        QuickSortModified(arrModified, 0, arrModified.Length - 1);
        QuickSortThreePivots(arrThreePivots, 0, arrThreePivots.Length - 1);

        string result = $"{comparisonsStandard} {comparisonsModified} {comparisonsThreePivots}";
        string programName = "ip43_diachenko_03";
        string outputFileName = $"{programName}_output.txt";

        try
        {
            File.WriteAllText(outputFileName, result);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error writing output file: " + ex.Message);
        }
    }

    static void QuickSortStandard(int[] arr, int left, int right)
    {
        if (left < right)
        {
            int pivotIndex = PartitionStandard(arr, left, right);
            QuickSortStandard(arr, left, pivotIndex - 1);
            QuickSortStandard(arr, pivotIndex + 1, right);
        }
    }

    static int PartitionStandard(int[] arr, int left, int right)
    {
        int pivot = arr[right];
        int i = left - 1;
        for (int j = left; j < right; j++)
        {
            comparisonsStandard++;
            if (arr[j] < pivot)
            {
                i++;
                Swap(arr, i, j);
            }
        }
        Swap(arr, i + 1, right);
        return i + 1;
    }

    static void QuickSortModified(int[] arr, int left, int right)
    {
        if (right - left + 1 <= 3)
        {
            InsertionSort(arr, left, right, ref comparisonsModified);
            return;
        }
        if (left < right)
        {
            int pivotIndex = PartitionModified(arr, left, right);
            QuickSortModified(arr, left, pivotIndex - 1);
            QuickSortModified(arr, pivotIndex + 1, right);
        }
    }

    static int PartitionModified(int[] arr, int left, int right)
    {
        int middle = left + (right - left) / 2;
        int medianIndex = MedianOfThree(arr, left, middle, right);
        Swap(arr, medianIndex, right);

        int pivot = arr[right];
        int i = left - 1;
        for (int j = left; j < right; j++)
        {
            comparisonsModified++;
            if (arr[j] < pivot)
            {
                i++;
                Swap(arr, i, j);
            }
        }
        Swap(arr, i + 1, right);
        return i + 1;
    }

    static int MedianOfThree(int[] arr, int left, int middle, int right)
    {
        int a = arr[left], b = arr[middle], c = arr[right];
        if ((a <= b && b <= c) || (c <= b && b <= a)) return middle;
        else if ((b <= a && a <= c) || (c <= a && a <= b)) return left;
        else return right;
    }

    static void QuickSortThreePivots(int[] arr, int left, int right)
    {
        if (right - left + 1 <= 3)
        {
            InsertionSort(arr, left, right, ref comparisonsThreePivots);
            return;
        }

        if (arr[left] > arr[left + 1])
            Swap(arr, left, left + 1);
        if (arr[left] > arr[right])
            Swap(arr, left, right);
        if (arr[left + 1] > arr[right])
            Swap(arr, left + 1, right);

        int q1 = arr[left];
        int q2 = arr[left + 1];
        int q3 = arr[right];

        List<int> list1 = new List<int>();
        List<int> list2 = new List<int>();
        List<int> list3 = new List<int>();
        List<int> list4 = new List<int>();

        for (int i = left + 2; i <= right - 1; i++)
        {
            comparisonsThreePivots++;
            if (arr[i] < q1)
                list1.Add(arr[i]);
            else
            {
                comparisonsThreePivots++;
                if (arr[i] < q2)
                    list2.Add(arr[i]);
                else
                {
                    comparisonsThreePivots++;
                    if (arr[i] < q3)
                        list3.Add(arr[i]);
                    else
                        list4.Add(arr[i]);
                }
            }
        }

        int index = left;
        foreach (var num in list1)
            arr[index++] = num;
        int q1Index = index;
        arr[index++] = q1;

        foreach (var num in list2)
            arr[index++] = num;
        int q2Index = index;
        arr[index++] = q2;

        foreach (var num in list3)
            arr[index++] = num;
        int q3Index = index;
        arr[index++] = q3;

        foreach (var num in list4)
            arr[index++] = num;

        QuickSortThreePivots(arr, left, q1Index - 1);
        QuickSortThreePivots(arr, q1Index + 1, q2Index - 1);
        QuickSortThreePivots(arr, q2Index + 1, q3Index - 1);
        QuickSortThreePivots(arr, q3Index + 1, right);
    }

    static void InsertionSort(int[] arr, int left, int right, ref long comparisons)
    {
        for (int i = left + 1; i <= right; i++)
        {
            int key = arr[i];
            int j = i - 1;
            while (j >= left)
            {
                comparisons++;
                if (arr[j] > key)
                {
                    arr[j + 1] = arr[j];
                    j--;
                }
                else
                    break;
            }
            arr[j + 1] = key;
        }
    }

    static void Swap(int[] arr, int i, int j)
    {
        (arr[i], arr[j]) = (arr[j], arr[i]);
    }
}