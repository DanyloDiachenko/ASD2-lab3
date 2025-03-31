using System;
using System.IO;
using System.Linq;

class Program
{
    static (int PivotIndex, int ComparisonCount) Partition(int[] array, int startIndex, int endIndex)
    {
        int comparisonCount = 0;

        int pivotElement = array[endIndex];
        int partitionIndex = startIndex - 1;

        for (int currentIndex = startIndex; currentIndex < endIndex; currentIndex++)
        {
            comparisonCount++;

            if (array[currentIndex] <= pivotElement)
            {
                partitionIndex++;
                (array[partitionIndex], array[currentIndex]) = (array[currentIndex], array[partitionIndex]);
            }
        }
        (array[partitionIndex + 1], array[endIndex]) = (array[endIndex], array[partitionIndex + 1]);

        return (partitionIndex + 1, comparisonCount);
    }

    static int QuickSort(int[] array, int startIndex, int endIndex)
    {
        int comparisonCount = 0;

        if (startIndex < endIndex)
        {
            var (pivotIndex, partComparison) = Partition(array, startIndex, endIndex);

            comparisonCount += partComparison;
            comparisonCount += QuickSort(array, startIndex, pivotIndex - 1);
            comparisonCount += QuickSort(array, pivotIndex + 1, endIndex);
        }

        return comparisonCount;
    }

    static (int PivotIndex, int ComparisonCount) Partition2(int[] array, int startIndex, int endIndex)
    {
        int comparisonCount = 0;

        int firstElement = array[startIndex];
        int middleElement = array[(startIndex + endIndex) / 2];
        int lastElement = array[endIndex];

        int[] trio = new int[] { firstElement, middleElement, lastElement };

        int pivotElement = trio.OrderBy(x => x).ElementAt(1);
        int pivotIndex;

        if (pivotElement == firstElement)
            pivotIndex = startIndex;
        else if (pivotElement == middleElement)
            pivotIndex = (startIndex + endIndex) / 2;
        else
            pivotIndex = endIndex;

        (array[pivotIndex], array[endIndex]) = (array[endIndex], array[pivotIndex]);

        int partitionIndex = startIndex - 1;

        for (int currentIndex = startIndex; currentIndex < endIndex; currentIndex++)
        {
            comparisonCount++;

            if (array[currentIndex] <= pivotElement)
            {
                partitionIndex++;
                (array[partitionIndex], array[currentIndex]) = (array[currentIndex], array[partitionIndex]);
            }
        }

        (array[partitionIndex + 1], array[endIndex]) = (array[endIndex], array[partitionIndex + 1]);

        return (partitionIndex + 1, comparisonCount);
    }

    static int QuickSort2(int[] array, int startIndex, int endIndex)
    {
        int comparisonCount = 0;

        if (startIndex < endIndex)
        {
            if (endIndex - startIndex + 1 > 3)
            {
                var (pivotIndex, partComparison) = Partition2(array, startIndex, endIndex);

                comparisonCount += partComparison;
                comparisonCount += QuickSort2(array, startIndex, pivotIndex - 1);
                comparisonCount += QuickSort2(array, pivotIndex + 1, endIndex);
            }
            else
            {
                for (int i = startIndex + 1; i <= endIndex; i++)
                {
                    int key = array[i];
                    int j = i - 1;
                    while (j >= startIndex && array[j] > key)
                    {
                        comparisonCount++;
                        array[j + 1] = array[j];
                        j--;
                    }

                    if (j >= startIndex)
                        comparisonCount++;

                    array[j + 1] = key;
                }
            }
        }

        return comparisonCount;
    }

    static (int LowIndex, int MidIndex, int HighIndex, int ComparisonCount) Partition3(int[] array, int startIndex, int endIndex)
    {
        int comparisonCount = 3;

        if (array[startIndex] > array[endIndex])
            (array[startIndex], array[endIndex]) = (array[endIndex], array[startIndex]);
        if (array[startIndex + 1] > array[endIndex])
            (array[startIndex + 1], array[endIndex]) = (array[endIndex], array[startIndex + 1]);
        if (array[startIndex] > array[startIndex + 1])
            (array[startIndex], array[startIndex + 1]) = (array[startIndex + 1], array[startIndex]);

        int leftBound = startIndex + 2, current = startIndex + 2, rightBound = endIndex - 1, highBound = endIndex - 1;
        int lowElement = array[startIndex], midElement = array[startIndex + 1], highElement = array[endIndex];

        while (current <= rightBound)
        {
            while (current <= rightBound && array[current] < midElement)
            {
                comparisonCount++;

                if (array[current] < lowElement)
                {
                    (array[leftBound], array[current]) = (array[current], array[leftBound]);
                    leftBound++;
                }

                current++;
                comparisonCount++;
            }
            while (current <= rightBound && array[rightBound] > midElement)
            {
                comparisonCount++;

                if (array[rightBound] > highElement)
                {
                    (array[rightBound], array[highBound]) = (array[highBound], array[rightBound]);
                    highBound--;
                }

                rightBound--;
                comparisonCount++;
            }

            if (current <= rightBound)
            {
                comparisonCount++;

                if (array[current] > highElement)
                {
                    comparisonCount++;

                    if (array[rightBound] < lowElement)
                    {
                        (array[current], array[leftBound]) = (array[leftBound], array[current]);
                        (array[leftBound], array[rightBound]) = (array[rightBound], array[leftBound]);

                        leftBound++;
                    }
                    else
                    {
                        (array[current], array[rightBound]) = (array[rightBound], array[current]);
                    }

                    (array[rightBound], array[highBound]) = (array[highBound], array[rightBound]);

                    current++;
                    rightBound--;
                    highBound--;
                }
                else
                {
                    comparisonCount++;

                    if (array[rightBound] < lowElement)
                    {
                        (array[current], array[leftBound]) = (array[leftBound], array[current]);
                        (array[leftBound], array[rightBound]) = (array[rightBound], array[leftBound]);

                        leftBound++;
                    }
                    else
                    {
                        (array[current], array[rightBound]) = (array[rightBound], array[current]);
                    }

                    current++;
                    rightBound--;
                }
            }
        }

        leftBound--;
        current--;
        highBound++;

        (array[startIndex + 1], array[leftBound]) = (array[leftBound], array[startIndex + 1]);
        (array[leftBound], array[current]) = (array[current], array[leftBound]);

        leftBound--;

        (array[startIndex], array[leftBound]) = (array[leftBound], array[startIndex]);
        (array[endIndex], array[highBound]) = (array[highBound], array[endIndex]);

        return (leftBound, current, highBound, comparisonCount);
    }

    static int QuickSort3(int[] array, int startIndex, int endIndex)
    {
        int comparisonCount = 0;

        if (startIndex < endIndex)
        {
            if (endIndex - startIndex + 1 > 3)
            {
                var (lowIndex, midIndex, highIndex, partComparison) = Partition3(array, startIndex, endIndex);

                comparisonCount += partComparison;
                comparisonCount += QuickSort3(array, startIndex, lowIndex - 1);
                comparisonCount += QuickSort3(array, lowIndex + 1, midIndex - 1);
                comparisonCount += QuickSort3(array, midIndex + 1, highIndex - 1);
                comparisonCount += QuickSort3(array, highIndex + 1, endIndex);
            }
            else
            {
                for (int i = startIndex + 1; i <= endIndex; i++)
                {
                    int key = array[i];
                    int j = i - 1;

                    while (j >= startIndex && array[j] > key)
                    {
                        comparisonCount++;
                        array[j + 1] = array[j];
                        j--;
                    }

                    if (j >= startIndex)
                        comparisonCount++;

                    array[j + 1] = key;
                }
            }
        }

        return comparisonCount;
    }

    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("File was not provided.");
            return;
        }

        string filename = args[0];
        if (!File.Exists(filename))
        {
            Console.WriteLine("File was not found.");
            return;
        }

        string[] lines = File.ReadAllLines(filename);
        int dataLength = int.Parse(lines[0].Trim());
        int[] data = new int[dataLength];
        for (int i = 0; i < dataLength; i++)
        {
            data[i] = int.Parse(lines[i + 1].Trim());
        }

        int[] testArray1 = (int[])data.Clone();
        int[] testArray2 = (int[])data.Clone();
        int[] testArray3 = (int[])data.Clone();

        int method1 = QuickSort(testArray1, 0, dataLength - 1);
        int method2 = QuickSort2(testArray2, 0, dataLength - 1);
        int method3 = QuickSort3(testArray3, 0, dataLength - 1);

        File.WriteAllText("ip43_diachenko_03_output.txt", $"{method1} {method2} {method3}\n");
    }
}
