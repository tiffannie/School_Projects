using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*
 * The purpose of this project is to help you understand the importance of difference equations to model the time efficiency of recursive and divide and conquer algorithms.
 * 
 * 
 * 
 * */

namespace DivideAndConquer
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> inputValues = new List<int>();

            //prompt the user to enter 3 numbers
            Console.WriteLine("Please, enter 3 numbers:");

            //control input number with a for loop
            for (int i = 0; i < 3; i++)
            {
                //read raw input
                string userValue = Console.ReadLine();
                int input;
                if (int.TryParse(userValue, out input))
                {
                    //add numbers to arraylist
                    inputValues.Add(input);
                }
            }
            Console.WriteLine("your first number is: " + (inputValues[0]));

            //assign variable names to values in arraylist
            int a = inputValues[0];
            int b = inputValues[1];
            int c = inputValues[2];
            /**
            Console.WriteLine(a);
            Console.WriteLine(b);
            Console.WriteLine(c);**/

            //saving b**c to variable
            double n = Math.Pow(b,c);

            //the solution to d1
            double m = n / (n - a);
            string d1 = n.ToString() + "/" + m;

            //power of k
            double k = Math.Log(a, b);
            string kwrite = "log" + b + "(" + a + ")";

            string cheesy;
            if (c ==0)
            {
                if (a == n)
                {
                    cheesy = "T[n] = (c1) + log" + b + "(n)";
                } else
                {
                    cheesy = "T[n] = (c1) +(n^" + k + ") + (" + m + ")";
                }
            } else
            {
                if (a ==n)
                {
                    cheesy = "T[n] = (c1)(n ^" + c + ") + (n^ " + c + ")(log" + b + "(n))";
                } else
                {
                    cheesy = "T[n] = (c1)(n^ " + k + ") + (" + m + ")(n^ " + c + ")";
                }
            }
            Console.WriteLine(cheesy);

            Console.ReadLine();
        }
    }
}
