using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * 
 * 
 * 
 * 
 * 
 * 
 * */
namespace MinimumSpanningTree
{
    class Program
    {
        static void Main(string[] args)
        {
            Kruskal();
            int m = int.MaxValue;
            int cost = 0;
            int[,] g = {
            {m,5,3,m,m,m,m },
            {5,m,4,6,2,m,m },
            {3,4,m,5,m,6,m },
            {m,6,5,m,6,6,m },
            {m,2,m,6,m,3,5 },
            {m,m,6,6,3,m,4 },
            {m,m,m,m,5,4,m }};



            //Prim(g, 7);
        }
        //***************************************************************************************************************************

        //***************************************************************************************************************************************
        //Kruskal's Algorithm
        static void Kruskal()
        {
            List<string> node1 = new List<string>() { "b", "e", "a", "f", "b", "a", "e", "c", "b", "e", "d", "c" };
            List<string> node2 = new List<string>() { "e", "f", "c", "g", "c", "b", "g", "d", "d", "d", "f", "f" };

            List<int> weight = new List<int>() { 2, 3, 3, 4, 4, 5, 5, 5, 6, 6, 6, 6 };

            List<List<String>> sets = new List<List<String>>();
            sets.Add(new List<string> { "a" });
            sets.Add(new List<string> { "b" });
            sets.Add(new List<string> { "c" });
            sets.Add(new List<string> { "d" });
            sets.Add(new List<string> { "e" });
            sets.Add(new List<string> { "f" });
            sets.Add(new List<string> { "g" });

            int cost = 0;

            for (int node = 0; node < node1.Count - 1; node++)
            {
                //for given edge, check and see if the two nodes are connected
                //check if they are inside the same list in my list of sets
                //if they are connected already, reject the edge and continue          //loop through all nodes in one list
                //if they are not connected, merge the two lists
                int index1 = 0;
                int index2 = 0;

                for (int listsIndex = 0; listsIndex < sets.Count; listsIndex++)        //loop through the lists of lists
                {
                    if (sets[listsIndex].Contains(node1[node]))                       //if list contains the node
                    {
                        index1 = listsIndex;                                          //get index of the node in sets list
                    }
                    if (sets[listsIndex].Contains(node2[node]))                       //if list contains node of connecting neighbor
                    {
                        index2 = listsIndex;                                          //get its index as well
                    }
                }
                if (index1 != index2)                                                  //they shouldn't equal eachother
                {                                                                      //they won't be in same index
                    Merge(sets, index1, index2);                                       //merge sets' lists at said indices
                    cost = cost + weight[node];                               //IT'S SKIPPING THE 5'S.. update: it's because I ordered the hard-coded lists incorrectly #dill
                    //Console.WriteLine(("List" + String.Join(",", sets)));
                    Console.WriteLine(cost);
                    Console.Write("{");
                    foreach (var sublist in sets)
                    {

                        foreach (var obj in sublist)
                        {

                            Console.Write(obj + " ");
                        }
                        Console.Write(",");
                    }
                    Console.Write("}");
                    Console.WriteLine();
                }
            }
            Console.WriteLine("Cost: " +cost);
            Console.ReadLine();
        }
        //*****************************************************
        //Merge method to combine items of one list with the items in another list within a list of lists
        static List<List<string>> Merge(List<List<string>> a, int indexA, int indexB)
        {
            a[indexA].AddRange(a[indexB]);                                              //the list at a certain inde adds ALL the contents of the list at the second index
            a.RemoveAt(indexB);                                                         //remove the list at the second index since the list exitsts within another list
            return a;                                                                   //return the merged list
        }
        //Prim's Algorithm
       
            public static void Prim(int[,] graph, int verticesCount)
            {
                int[] parent = new int[verticesCount];              //array of 7
                int[] key = new int[verticesCount];                 //array of 7
                bool[] mstSet = new bool[verticesCount];            //"if it's already in the MST set"
                List<int> visited = new List<int>();

                List<string> set = new List<string>() { "a", "b", "c", "d", "e", "f", "g" };
                int count1 = 0;
                int cost1 = 0;
                while (count1 < 7)
                {
                    int min = int.MaxValue;
                    int theIndex = 0;
                    for (int i = 0; i < 7; i++)
                    {
                        if (visited.Contains(i))
                        {
                            for (int j = 0; j < 7; j++)
                            {
                                if (graph[i, j] < min && !visited.Contains(j))
                                {
                                    min = graph[i, j];
                                    theIndex = j;
                                }
                            }
                        }
                    }

                    if (min != int.MaxValue)
                    {
                        Console.WriteLine(cost1);
                        cost1 += min;
                    }
                    visited.Add(theIndex);
                    visited.Sort();
                    count1++;
                    string s = "{";
                    int countt = 0;
                    foreach (int i in visited)
                    {
                        if (countt != visited.Count - 1)
                        {
                            s += set[i] + ", ";
                        }
                        else
                        {
                            s += set[i];
                        }
                        countt++;
                    }
                    s += "}";
                    Console.WriteLine(s);
                }
                Console.Write("}");
                Console.WriteLine();

                Console.WriteLine("cost is " + cost1);
                Console.WriteLine("CS 301 is the BEST");
            }
        }
    }

