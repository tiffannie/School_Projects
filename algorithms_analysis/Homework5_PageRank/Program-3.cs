using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * Use C# to simulate the PageRank algorithm (you can initialize r[i] = 1/7 for i = 1, ..., 7). Plot the PageRank of each page over time (until at least t = 20) for \alpha=0,\:0.25,\:0.5,\:0.75,\:1\:α=0,0.25,0.5,0.75,1. Create a separate matrix for each value of \alphaα. 
 *  
 * 
 * */
namespace PageRank
{
    class Program
    {
        static void Main(string[] args)
        {
            //e = {1,1,1,1,1,1,1}
            //e^t ={1,
            //      1,
            //      1,
            //      1,
            //      1,
            //      1,
            //      1}

            //e*e^t is matric 7x7 filled with 1's
            //(1/n)*ee^t is a matrix of 7x7 filled with 1/7's 

           double[,] r0 = new double[,]{ { (1.0 / 7) },
                                             { (1.0 / 7) },
                                             { (1.0 / 7) },
                                             { (1.0 / 7) },
                                             { (1.0 / 7) },
                                             { (1.0 / 7) },
                                             { (1.0 / 7)} };

            double[,] eeT = new double[7, 7] //eeT
             //   A      B       C     D      E      F      G           
            { { (1.0/7), (1.0/7), (1.0/7), (1.0/7), (1.0/7), (1.0/7), (1.0/7)}, //A  
              { (1.0/7), (1.0/7), (1.0/7), (1.0/7), (1.0/7), (1.0/7), (1.0/7)}, //B
              { (1.0/7), (1.0/7), (1.0/7), (1.0/7), (1.0/7), (1.0/7), (1.0/7)}, //C
              { (1.0/7), (1.0/7), (1.0/7), (1.0/7), (1.0/7), (1.0/7), (1.0/7)}, //D
              { (1.0/7), (1.0/7), (1.0/7), (1.0/7), (1.0/7), (1.0/7), (1.0/7)}, //E 
              { (1.0/7), (1.0/7), (1.0/7), (1.0/7), (1.0/7), (1.0/7), (1.0/7)}, //F
              { (1.0/7), (1.0/7), (1.0/7), (1.0/7), (1.0/7), (1.0/7), (1.0/7)}};//G 

            double[,] s = new double[7, 7]
             // A     B       C    D    E       F     G           
            { { 0,    0,      0,   0,   0,   (1.0/7), 0}, //A  
              { 1.0,  0,      0,   0,   0,   (1.0/7), 0}, //B
              { 0,   (1.0/3), 0,   0,   0,   (1.0/7), 0}, //C
              { 0,   (1.0/3), 0,   0,   0,   (1.0/7), 1.0}, //D
              { 0,   (1.0/3), 1.0, 1.0, 0,   (1.0/7), 0}, //E 
              { 0,    0,      0,   0,   1.0, (1.0/7), 0}, //F
              { 0,    0,      0,   0,   0,   (1.0/7), 0}};//G

            //**********************************************************************************************compute (alpha*s)+ (1-alpha)*(1/n)*eeT *********************
            double[] alpha = new double[5] { 0.0, 0.25, 0.5, 0.75, 1.0 };
            double[,] guu = new double[7,7];
            double[,] buu = new double[7, 7];
            double alph = 1.0;                                                                     //AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAALPHA
            guu = AlphaMultMatrix(s, alph);         //do first half of equation
            buu = AlphaMultMatrix(eeT, (1 - alph)); //do second half of equation
            double[,] g = AddMatrix(guu, buu);      //add them together

            //*************************************************************************************calculate and keep track of G times Rk
            List<double[,]> rs = new List<double[,]>();     //add r matrix values to a list to work with easier
            double[,] curry = r0;                           //current r value times matrix
            double[,] beef;                                 //previous r value times matrix
            int count = 0;
            for (int i = 0; i < 21; i++)
            {
                beef = MultiplyMatrix(g, curry);            
                Console.WriteLine(count);
                printMatrix(beef);
                rs.Add(beef);                               //add values to the list to output
                curry = beef;
                count++;
            }
            //******************************************************************************************************************writing file
            using(System.IO.StreamWriter sw = new System.IO.StreamWriter("C:\\Users\\tiffannie\\Desktop\\final4.csv"))
            {
                foreach(double[,] k in rs)
                {
                    printMatrix(k);
                    for (int i = 0; i < k.GetLength(0); i++)
                    {
                        for (int j = 0; j < k.GetLength(1); j++)
                        {
                            sw.Write(k[i, j]);
                            sw.Write(",");
                        }
                    }
                    sw.WriteLine("");
                }
            }
                
        }
        //LOGIC METHODS
        //********************************************************************************************************************print matrix
        public static void printMatrix(double[,] c)
        {
            for(int i = 0; i < c.GetLength(0); i++)
            {
                for (int j = 0; j < c.GetLength(1); j++)
                {
                    Console.Write(Math.Round(c[i, j], 3) + " ");
                }
                Console.WriteLine("");
            }
        }
        //********************************************************************************************************************add matrix
        public static double[,] AddMatrix(double[,] A, double[,] B)
        {
            int rA = A.GetLength(0);
            int cA = A.GetLength(1);
            double[,] newMatrix = new double[rA, cA];

            for (int row = 0; row < rA; row++) //for rows in first
            {
                for (int col = 0; col < cA; col++)    //for cols;
                {
                    newMatrix[row, col] = A[row, col] + B[row, col]; //add the values at the indices together
                }
            }
            
            return newMatrix;
        }
        //********************************************************************************************************************scalar matrix multiplication
        public static double[,] AlphaMultMatrix(double[,] multiDimArray, double alpha)
        {
            double alph = alpha;
            int row = multiDimArray.GetLength(0);
            int col = multiDimArray.GetLength(1);
            double[,] newMatrix = new double[row, col];

            for (int array = 0; array < multiDimArray.GetLength(0); array++)   //row
            {
                for (int elem = 0; elem < multiDimArray.GetLength(1); elem++)  //column
                {
                    newMatrix[array, elem] = multiDimArray[array, elem]* alph; //multiply value at index by alpha
                }
            }
            return newMatrix;

        }
        //******************************************************************************************************************** matrix multiplication
        public static double[,] MultiplyMatrix(double[,] A, double[,] B)
        {
            int rA = A.GetLength(0);        //get row and columns of first matrix
            int cA = A.GetLength(1);
            int rB = B.GetLength(0);        //get rows and columns of second matrix
            int cB = B.GetLength(1);
            double temp = 0;
            double[,] newMatrix = new double[rA, cB];
            double[,] wrong = new double[1, 1];
            if (cA != rB)
            {
                Console.WriteLine("matrices cannot be multiplied");
                return wrong;
            }
            else
            {
                for (int i = 0; i < rA; i++)            //row un A
                {
                    for (int j = 0; j < cB; j++)        //column in b
                    {
                        temp = 0;
                        for (int k = 0; k < cA; k++)    //for value in column of a
                        {
                            temp += A[i, k] * B[k, j];  //multiply the indices together
                        }
                        newMatrix[i, j] = temp;         //place them in the newMatrix
                    }
                }
                return newMatrix;
            }
        }
        //***********************************************************************************************************************show dimensions of matrix
        private static void ShowArrayInfo(Array arr)
        {
            Console.WriteLine("Length of Array:      {0,3}", arr.Length);
            Console.WriteLine("Number of Dimensions: {0,3}", arr.Rank);
            Console.WriteLine("Dimension 1: Rows, Dimension 2: Columns");

            // For multidimensional arrays, show number of elements in each dimension.
            if (arr.Rank > 1)
            {
                for (int dimension = 1; dimension <= arr.Rank; dimension++)
                    Console.WriteLine("   Dimension {0}: {1,3}", dimension,
                                      arr.GetUpperBound(dimension - 1) + 1);
            }
            Console.WriteLine();
        }
    }
}
