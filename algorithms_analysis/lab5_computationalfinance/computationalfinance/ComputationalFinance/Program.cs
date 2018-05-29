using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace ComputationalFinance
{
    class Program
    {
        static void Main(string[] args)
        {
            int row = 0;
            string line;
            //double[,] maych = new double[4, 7];
            double[,] maych = new double[25, 55];
            //"C:/Users/tiffannie/Desktop/CS 301/lab 5 finance/test.csv" feasible_mu0.csv
            using (StreamReader file = new StreamReader("C:/Users/tiffannie/Desktop/CS 301/lab 5 finance/feasible_mu0_1.csv"))//check column and bottom. must be > for hw files & < 4 test
            {
                while ((line = file.ReadLine()) != null)
                {
                    double[] nums = line.Split(',').Select(double.Parse).ToArray();                       //split line by commas and cast to an int. put ints into array
                    for (int i = 0; i < nums.Length; i++)
                    {
                        maych[row, i] = nums[i];                                                    //add values to 2D array one row at a time
                    }
                    row++;
                }
                file.Close();
                // Suspend the screen.
                Console.ReadLine();
            }
            //************************************************************************************************************************************************************
            printMatrix(maych);
            //ShowArrayInfo(maych);
            int count = 1;
            List<int> rowVals = new List<int>();
            List<int> colVals = new List<int>();
            while (isBottom(maych))//while there is a negative number on the bottom row of matrix, do ALL THE OPERATIONS
            {
                int maxColIndexValue = getPivotColumn(maych);
                Console.WriteLine("Initial matrix" + count);
                Console.WriteLine("Pivot Column: " + maxColIndexValue);//biggest column value

                int minRowIndexValue = getPivotRow(maych, maxColIndexValue);
                Console.WriteLine("Pivot Row: " + minRowIndexValue);//smallest row value

                maych = rowOperations(maych, minRowIndexValue, maxColIndexValue);
                printMatrix(maych);
                Console.WriteLine();
                count++;
                if(maxColIndexValue >= 7)
                {
                    rowVals.Add(minRowIndexValue);                  //to get x vals or something
                    colVals.Add(maxColIndexValue);
                }
            }
            writeMatrixToFile(maych);

            List<double> xVal = xValues(maych, rowVals, colVals);
            Console.Write("x values: ");
            for (int i = 0; i < xVal.Count-1; i++)
            {
                Console.Write(xVal[i]);
            }
            Console.WriteLine("");
        }
        public static List<double> xValues(double[,] matrix, List<int> row, List<int> col)
        {
            List<double> vals = new List<double>();
            for (int i = 0; i < 7; i++)
            {
                if (col.Contains(i))
                {
                    int index = col.IndexOf(i);
                    int colIndex = col[index];
                    int rowIndex = row[index];
                    double value = matrix[rowIndex, matrix.GetLength(1) - 1];
                    vals.Add(value);
                }
                else
                {
                    vals.Add(0);
                }
            }
            return vals;
        }
        public static bool isBottom(double[,] matrix)
        {
            bool bottom = false;
            for(int i = 0; i < matrix.GetLength(1); i++)
            {
                if (matrix[matrix.GetLength(0)-1, i] > 0)                           //check if all bottom row is negative
                {
                    bottom = true;
                }
            }
            return bottom;
        }

        public static double[,] rowOperations(double[,] matrix, int pivotRow, int pivotCol)
        {
            matrix = divideRowByPivot(matrix, pivotRow, matrix[pivotRow, pivotCol]);
            for(int i = 0; i < matrix.GetLength(0); i++)
            {
                if(i != pivotRow)
                {
                    matrix = subtractProductOfPivotRowValueAndCurrentPivotColumnValueFromCurrentRowIndex(matrix, i, pivotRow, matrix[i, pivotCol]);
                }
            }
            return matrix;
        }
        public static double[,] subtractProductOfPivotRowValueAndCurrentPivotColumnValueFromCurrentRowIndex(double[,] matrix, int row, int pivotRow, double pivotValue)
        {
            for(int i = 0; i < matrix.GetLength(1); i++)
            {
                matrix[row, i] = matrix[row, i] - (matrix[pivotRow, i] * pivotValue);
            }
            return matrix;
        }
        public static  double[,] divideRowByPivot(double[,] matrix, int row, double pivotValue)
        {
            for(int i = 0; i < matrix.GetLength(1); i++)
            {
                matrix[row, i] = matrix[row,i] / pivotValue;
            }
            return matrix;
        }
        public static int getPivotRow(double[,] matrix, int col)
        {
            //Console.WriteLine(col);
            int pivotRow = 0;
            //double min = matrix[0, col] / matrix[0, matrix.GetLength(1) - 1]; //in terms of the ratio.  number in column / last number in row
            //first row, max column  / first row, last column
            double min = int.MaxValue;

            for (int i = 0; i < matrix.GetLength(0)-1; i++)                  
            {
                double rhs = matrix[i, matrix.GetLength(1) - 1];            //furthest "right hand side" value
                double next = rhs / matrix[i, col];                         //set first number as next initially
                if (i == 0 && next >=0)
                {
                    min = next;
                    //Console.WriteLine("min = " + min);
                }
                else if (next <= min && next >= 0)
                {                                                           //if the "next" number is less than the min value (a temp variable)
                    min = next;                                             //the next value is the new min value
                    pivotRow = i;                                           //the row index is set
                    //Console.WriteLine("actual min = " + min);
                }
            }
            return pivotRow;
        }

        public static int getPivotColumn(double[,] matrix)
        {
            double max = matrix[matrix.GetLength(0)-1,0];                   //set initial maximum pivot column index as the second to last. don't do -1 because you don't want to include the last column value(RHS)
            int pivotColumn = 0;
            for (int i = 0; i < matrix.GetLength(1)-1; i++)//don't include last value in row since it's the RHS so subtract 1. 
            {                                                               //for each column
                if(matrix[matrix.GetLength(0)-1, i] > max)                  //if the value of the last row of a certain column is greater than the "max" value (arbitrarily set to the first row value)
                {
                    max = matrix[matrix.GetLength(0)-1, i];                 //then set the max value to the last row, current column value
                    pivotColumn = i;                                                //set index
                }
            }
            return pivotColumn;

        }
        //****************************************************************************************************************************************************************************************
        public static void printMatrix(double[,] c)
        {
            for (int i = 0; i < c.GetLength(0); i++)
            {
                for (int j = 0; j < c.GetLength(1); j++)
                {
                    Console.Write(Math.Round(c[i, j],3) + " ");                 //round the numbers
                }
                Console.WriteLine("");
            }
        }
        public static void writeMatrixToFile(double[,] matrix)
        {
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"C:/Users/tiffannie/Desktop/CS 301/lab 5 finance/maych.csv"))
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        //file.Write(Math.Round(matrix[i, j], 15) + " ");                 //round the numbers
                        file.Write(matrix[i, j] + ",");
                    }
                    file.WriteLine();
                }
            }
        }
    }
}
