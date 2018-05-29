using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

/*
 * Implement the following algorithms:

heapsort
mergesort
quicksort
as described in the slides on the following lists:

using the pseudocode from the slides on the following lists, print out each step of the sort:

1. [1, 6, 4, 7, 9, 8, 3, 2, 5]

2. [3, 7, 9, 2, 4, 1, 3, 5, 2]

Use your code to run each sorting algorithm on a file. Don't print each step of the sort, just check that the list is sorted correctly by comparing it to the result of the built in listName.sort() function and LINQ's list1.SequenceEqual(list2).
 * 
 * 
 * */
namespace SortingAlgorithmsP2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> t = new List<int>();    //create lists to store unordered lists
            List<int> t2 = new List<int>();
            List<int> t3 = new List<int>();
            List<int> t4 = new List<int>();

            //File read in
            string[] x = File.ReadAllLines("C:/Users/tiffannie/Desktop/test1.txt");
            foreach (string element in x)
            {
                t.Add(Int32.Parse(element)); //cast unordered string characters to ints and add them to lists
                t2.Add(Int32.Parse(element));
                t3.Add(Int32.Parse(element));
                t4.Add(Int32.Parse(element));
            }


            int[] tt = t.ToArray();             //convert lists to arrays
            int[] tt2 = t2.ToArray();
            int[] tt3 = t3.ToArray();
            int[] tt4 = t4.ToArray();

            //**********************************************//heapsort call
            HeapSort hs = new HeapSort();
            hs.PerformHeapSort(tt);
            Console.ReadLine();

            //***********************************************//merge sort
            Console.WriteLine("merge");
            Console.ReadLine();
            MergeSort(tt2);
            Console.ReadLine();

            //***************************************          //quicksort call

            quickSort q_Sort = new quickSort();
            
            int[] arr = {3,7,9,2,4,1,3,5,2};
            foreach (int element in tt3)
            q_Sort.arr = tt3;
            q_Sort.len = q_Sort.arr.Length;

            // Sort the array
            Console.WriteLine("quick");
            Console.ReadLine();
            q_Sort.QuickSort();
            
            Console.WriteLine();

            Console.ReadLine();
            //*******************************************
            //call sort function on list to do comparisons of big lists
            t4.Sort();
            Console.ReadLine();
            //convert arrays back to lists
            List<int> ttt = tt.OfType<int>().ToList();
            List<int> ttt2 = tt2.OfType<int>().ToList();
            List<int> ttt3 = tt3.OfType<int>().ToList();
            List<int> ttt4 = tt4.OfType<int>().ToList();

            bool worked = false; //check if the ordered lists are equal to the ordered list sorted by the .Sort() function
            if (ttt.SequenceEqual(t4) && ttt2.SequenceEqual(t4) && ttt3.SequenceEqual(t4) || ttt4.SequenceEqual(t4))  //compare selection sorted list to sort function sorted list
            {
                worked = true;
            }
            if (worked == true)
            {
                Console.WriteLine("it works");
            }
            else
            {
                Console.WriteLine("it doesn't...");
            }


        }

        //the sorting algorithms: Merge, Quick, Heap
        //*****************************************************************************************************************************************************************

        public static int[] MergeSort(int[] arr)
        {
            if (arr.Length <= 1)
            {
                return arr;
            }
            int mid = (arr.Length) / 2;
            int[] left = new int[mid];
            int[] right = new int[arr.Length - mid];

            Array.Copy(arr, left, mid);
            Array.Copy(arr, mid, right, 0, right.Length);


            left = MergeSort(left);
            right = MergeSort(right);
            String answer = "";
            foreach (int a in Merger(left, right))
            {
                answer += a + " ";
            }
            Console.WriteLine(answer);
            return Merger(left, right);


        }

        public static int[] Merger(int[] left, int[] right)
        {
            List<int> leftList = left.OfType<int>().ToList();
            List<int> rightList = right.OfType<int>().ToList();
            List<int> resultList = new List<int>();

            while (leftList.Count > 0 || rightList.Count > 0)
            {
                if (leftList.Count > 0 && rightList.Count > 0)
                {
                    if (leftList[0] <= rightList[0])
                    {
                        resultList.Add(leftList[0]);
                        leftList.RemoveAt(0);
                    }
                    else
                    {
                        resultList.Add(rightList[0]);
                        rightList.RemoveAt(0);
                    }
                }
                else if (leftList.Count > 0)
                {
                    resultList.Add(leftList[0]);
                    leftList.RemoveAt(0);
                }
                else if (rightList.Count > 0)
                {
                    resultList.Add(rightList[0]);
                    rightList.RemoveAt(0);
                }
            }
            int[] result = resultList.ToArray();
            return result;
        }
        //*****************************************************************************************************************************************************************

    }//quicksort
    class quickSort
    {

        // this is our array to sort
        public int[] arr = new int[9];

        // this holds a number of elements in array
        public int len;

        // Quick Sort Algorithm
        public void QuickSort()
        {
            sort(0, len - 1);
        }

        public void sort(int left, int right)
        {
            int pivot, l_holder, r_holder;

            l_holder = left;
            r_holder = right;
            pivot = arr[left];

            while (left < right)
            {
                while ((arr[right] >= pivot) && (left < right))
                {
                    right--;
                }

                if (left != right)
                {
                    arr[left] = arr[right];
                    left++;
                }

                while ((arr[left] <= pivot) && (left < right))
                {
                    left++;
                }

                if (left != right)
                {
                    arr[right] = arr[left];
                    right--;
                }
                Console.WriteLine(string.Join(",", arr));
            }

            arr[left] = pivot;
            pivot = left;
            left = l_holder;
            right = r_holder;

            if (left < pivot)
            {
                sort(left, pivot - 1);
            }

            if (right > pivot)
            {
                sort(pivot + 1, right);
            }
        }

    }
}
    //**************************************************************************************************************
    //heap sort is special because it needs its own class
    class HeapSort
    {
        private int heapSize;

        private void BuildHeap(int[] arr)
        {
            heapSize = arr.Length - 1;
            for (int i = heapSize / 2; i >= 0; i--)
            {
                Heapify(arr, i);
            Console.WriteLine(string.Join(",", arr));
        }
        }

        private void Swap(int[] arr, int x, int y)//function to swap elements
        {
            int temp = arr[x];
            arr[x] = arr[y];
            arr[y] = temp;
        }
        private void Heapify(int[] arr, int index)
        {
            int left = 2 * index + 1;
            int right = 2 * index + 2;
            int largest = index;
            if (left <= heapSize && arr[left] > arr[index])
            {
                largest = left;
            }

            if (right <= heapSize && arr[right] > arr[largest])
            {
                largest = right;
            }

            if (largest != index)
            {
                Swap(arr, index, largest);
                Heapify(arr, largest);
            }
        }
        public void PerformHeapSort(int[] arr)
        {
        Console.WriteLine("heap");
            BuildHeap(arr);
            for (int i = arr.Length - 1; i >= 0; i--)
            {
                Swap(arr, 0, i);
                heapSize--;
                Heapify(arr, 0);
            Console.WriteLine(string.Join(",", arr));
        }
        }
        
    }