using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace nQueenz


/*
 * Find all solutions to the n-Queens problem in C# for n = 4 (2 solutions) and n = 5 (10 solutions) using a branch and bound approach.
 * 
 * 
 * */
{
    class Program
    {
        public static int n = 15;
        static void Main(string[] args)
        {
            int[,] mat = new int[2, 3] { { 1, 2, 3 }, { 1, 1, 3 } };                      //test print
            //printGrid(mat);                                                         
            nQueensSolution();
        }
        public static void printGrid(int[,] board)//********************************PRINT MATRIX*************************************************************************************************
        {
            for (int i = 0; i < board.GetLength(0); i++)                            //loops through rows
            {
                for (int j = 0; j < board.GetLength(1); j++)                        //loop through columns
                {
                    Console.Write(board[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();                                                    //separate between matrices
        }
        public static void nQueensSolution()//**************************************SETTING BOUNDARIES OF THE QUEENS*******************************************************************************
        {
            int numberOfQueens = 0;                         //track number of queens on the board
            List<int[,]> solutions = new List<int[,]>();    //store the matrices with the correct queen placement
            int[,] board = new int[n, n];                   //standard matrix
            int[,] forwardDiagonal = new int[n, n];         //matrix to store diagonals going from bottom left to top right
            int[,] backwardDiagonal = new int[n, n];        //matrix to store diagonals going from top right to bottom left
            bool[] rowOccupied = new bool[n];               //true false value that tells me when there is a queen on the row
            bool[] forwardDiagCheck = new bool[2 * n - 1];  //makes sure we have any sized grid. gets all possible diagonals. treats each diag as separate entity
            bool[] backwardDiagCheck = new bool[2 * n - 1]; //with a grid 4x4 there are 7 diagonals possible
            //column check happens automatically

            for(int i=0; i < board.GetLength(0); i++)//rows
            {
                rowOccupied[i] = false;                                             //initialize to false because the queen hasn't been set
                forwardDiagCheck[i] = false;
                backwardDiagCheck[i] = false;
                for(int j = 0; j < board.GetLength(1); j++)// when you go row by row, cols are checked automatically                           //n = 4      fwd           bwd
                {                                                                                                                                       //0 1 2 3       3 2 1 0       
                    forwardDiagonal[i, j] =  i + j;                                  //initialize for and back diag in unique way                       //1 2 3 4       4 3 2 1
                    backwardDiagonal[i, j] = i - j + n - 1;                          //sets the row values corresponding with fwd and bwd diag lines    //2 3 4 5       5 4 3 2
                }                                                                                                                                       //3 4 5 6       6 5 4 3
            }
            //helper funk for recurjsh*************************************************************************************************************************************************************
            breakfastAndBrunch(board, 0, forwardDiagonal, backwardDiagonal, rowOccupied, forwardDiagCheck, backwardDiagCheck, numberOfQueens, solutions);//branch and bound function
            //foreach(int[,] grid in solutions)                                       //comment out this foreach loop to print 15 queens solutions
            //{
            //    printGrid(grid);                                                     //PRINT ALL SOLUTIONS
            //}
            Console.Write("For " + n + " queens, there are ");
            Console.WriteLine(solutions.Count + " solutions");
            Console.ReadLine();
        }//****************************************************************************************************************************************************************************************
        public static bool notSlayable(int row, int col, int[,] forwardDiagonal, int[,] backwardDiagonal, bool[] rowOccupied, bool[] forwardDiagCheck, bool[] backwardDiagCheck)
        {
            return !(forwardDiagCheck[forwardDiagonal[row, col]] || backwardDiagCheck[backwardDiagonal[row, col]] || rowOccupied[row]);
                    //check if the value at this coordinate has a queen from all diagonals and on row (returns true or false)
                    //if there is a queen there then she will get slayed or if there's a queen on the forward slash then she'll get slayed or if she's on the row then obvi she'll get slayed
        }
        public static void breakfastAndBrunch(int[,] grid, int col, int[,] forwardDiagonal, int[,] backwardDiagonal, bool[] rowOccupied, bool[] forwardDiagCheck, bool[] backwardDiagCheck, int numQueens, List<int[,]> solutions)
        {
            for(int i = 0; i < n; i++)
            {   //if the queen that this particular location is safe and not in the range of other Queens
                if(notSlayable(i, col, forwardDiagonal, backwardDiagonal, rowOccupied, forwardDiagCheck, backwardDiagCheck))
                {
                    rowOccupied[i] = true;                                          //now this row cannot have another queen on it
                    grid[i, col] = 1;                                               //now the queen will have 1 instead of 0
                    forwardDiagCheck[forwardDiagonal[i, col]] = true;               //the queen is present
                    backwardDiagCheck[backwardDiagonal[i, col]] = true;             //the queen is present
                    numQueens += 1;                                                 //incement count of queens on the board

                    if(numQueens == n)
                    {
                        int[,] grid1 = new int[n, n];                               //create a copy grid
                        for(int j = 0; j < n; j++)
                        {
                            for(int k = 0; k < n; k++)
                            {
                                grid1[j, k] = grid[j, k];
                            }
                        }
                        solutions.Add(grid1);                                       //add soultion to a list of matrices
                    }
                    else
                    {                                                           //recursive call to place the rest of the queens on the board
                        breakfastAndBrunch(grid, col + 1, forwardDiagonal, backwardDiagonal, rowOccupied, forwardDiagCheck, backwardDiagCheck, numQueens, solutions);
                    }
                    grid[i, col] = 0;                                               //make the rest of the values that aren't queens zeroes
                    numQueens -= 1;                                                 //decrement the number of queens
                    rowOccupied[i] = false;                                         //redo the booleans
                    forwardDiagCheck[forwardDiagonal[i, col]] = false;
                    backwardDiagCheck[backwardDiagonal[i, col]] = false;
                }
            }
        }
    }
}