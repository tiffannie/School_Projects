using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> t = new List<int>();
            List<int> t2 = new List<int>();
            List<int> t3 = new List<int>();
            List<int> t4 = new List<int>();

            //My way
            //string[] x = File.ReadAllLines("C:/Users/tiffannie/Desktop/test.txt");
            /**foreach (string element in x)
            {
                t.Add(Int32.Parse(element));
                t2.Add(Int32.Parse(element));
                t3.Add(Int32.Parse(element));
                t4.Add(Int32.Parse(element));
            }

            **/
            //Stan's way
           string file = Console.ReadLine();
           
            using (var text = File.OpenRead(@file))
            using(var reader = new StreamReader(text))
            {                                                   //C:\Users\tiffannie\Desktop\test.txt
                while (!reader.EndOfStream)
               {
                   string line = reader.ReadLine();
                    
                    t.Add(Int32.Parse(line));
                    t2.Add(Int32.Parse(line));
                    t3.Add(Int32.Parse(line));
                    t4.Add(Int32.Parse(line));
                }
            }
            
            
            //call methods on different lists
            BubbleSort(t);
            Console.ReadLine();
            InsertionSort(t2);
            Console.ReadLine();
            SelectionSort(t3);
            Console.ReadLine();

            //call sort function on list
            t4.Sort();
            Console.ReadLine();

             
            bool worked = false;
            if (t3.SequenceEqual(t4))                  //compare selection sorted list to sort function sorted list
            {
                worked = true;
            }
            if (worked == true)
            {
                Console.WriteLine("it works");
            } else
            {
                Console.WriteLine("it doesn't...");
            }
            /**foreach (int a in t3)
            {
                foreach(int b in t4)
                {
                    if (t[a] == t[b])
                    {
                        Console.WriteLine("it works");
                    } else
                    {
                        Console.WriteLine("it doesn't...");
                    }
                }
            }**/

        }
        static void BubbleSort(List<int> t)
        {
            bool swapped = true;
            do
            {
                swapped = false;
                for (int i = 1; i < t.Count(); i++) //initialize variable i as 1. while i's value is < len of list, 
                {
                    if (t[i - 1] > t[i])            //if value at index before the current index is > current index's value
                    {
                        int tmp = t[i];             //save current index's value in temporary variable
                        t[i] = t[i - 1];
                        t[i - 1] = tmp;             //save value of previous index to the current index
                        swapped = true;             //the swap is complete
                        
                    }
                }
                Console.WriteLine(String.Join(",", t));
                //C:\Users\tiffannie\Desktop\test.txt

            } while (swapped); 

            //place print statement after you've swapped so you can see new values.
        }

        static void InsertionSort(List<int> t)
        {
            bool swapped = false;

            for (int i = 1; i < t.Count(); i++)
            {
                int x = t[i];
                int j = i - 1;
                while (j > -1 && x < t[j])
                {
                    t[j + 1] = t[j];
                    j = j - 1;
                    swapped = true;
                }
                t[j + 1] = x;
                Console.WriteLine(String.Join(",", t));
            }
        } 

        static void SelectionSort(List<int> t)
        {
            bool swapped = false;
            int i, j, n;
            n = t.Count();
            for (j = 0; j < n-1; j++)         //for i until length of list
            {
                int mini = j;                           //minimum j is equal to i
                for (i =j +1; i< n; i++)
                {
                    if (t[i] < t[mini])     //minx)
                    {
                        mini = i;
                        //minx = t[j];
                    }
                }
                //t[minj] = t[i];
                //t[i] = minx;
                if(mini != j)
                {
                    //swap t[i], t[minj]
                    int tmp = t[mini];             //save current index's value in temporary variable
                    t[mini] = t[j];
                    t[j] = tmp;             //save value of previous index to the current index
                    swapped = true;
                }
                Console.WriteLine(String.Join(",", t));
            }
        }
    }
}