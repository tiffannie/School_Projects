using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace GeneticsLab
{
    class PairWiseAlign
    {
        public int Align(GeneSequence sequenceA, GeneSequence sequenceB)
        {
            // this dummy code simply produces a interesting number.
            // Replace this code with your real implementation.
            string dashA = "-" + sequenceA.Sequence;
            string dashB = "-" + sequenceB.Sequence;
            char[] seqA = dashA.ToCharArray();
            char[] seqB = dashB.ToCharArray();
            int sub = 1;
            int indel = 5;
            int match = -3;
            int size = 1000;
            int sAlength = 0;
            int sBlength = 0;
            

            if (seqA.Length < 1001){
                sAlength = seqA.Length;
            }
            else
            {
                sAlength = 1001;
            }
            if (seqB.Length < 1001)
            {
                sBlength = seqB.Length;
            }
            else
            {
                sBlength = 1001;
            }
            int[,] grid = new int[sAlength, sBlength];
            //**************************************************** BUILD MATRIX *****************************************************************
            // Core alignment algorithm
            for (int i = 0; i < sAlength; i++)//seqA.Length ROWS
            {
                for (int j = 0; j < sBlength; j++)//seqB.Length COLUMNS
                {

                    // Starting position
                    if (i == 0 && j == 0)                                           //initialize matrix with 0
                    {
                        grid[i, j] = 0;
                    }
                    // Edge cases row 0, 5, 10, 15 ...
                    else if (i == 0 && j > 0)
                    // Get the value from the left cell, and add as indel
                    {
                        grid[i, j] = grid[i, j - 1] + indel;                        //add 5 to the top ones (1st column)
                    }
                    // Edge case columns
                    else if (i > 0 && j == 0)
                    {
                        // Get the node from the top cell, and add as indel
                        grid[i, j] = grid[i - 1, j] + indel;                        //add 5 to the right (1st row)
                    }
                    //**************************************************************
                    else                                                            //if it's not the edges and the letters are the same
                    {
                        // Match or indel
                        int left = grid[i - 1, j] + indel;                          //get the values from all sides
                        int bottom = grid[i, j - 1] + indel;
                        int diag = grid[i - 1, j - 1];
                        List<int> mins = new List<int>();
                        mins.Add(left);
                        mins.Add(bottom); 
                        if ((i > 0 && j > 0) && (seqA[i] == seqB[j]))
                        {
                            diag = diag + match;
                            mins.Add(diag);                                         //add the number to a list
                        }
                        else if((i > 0 && j > 0) && (seqA[i] != seqB[j]))
                        {
                            diag = diag + sub;
                            mins.Add(diag);
                        }
                        int min = mins.Min();                                       //get the minimum value of the list
                        grid[i, j] = min;                                           //make the current cell the min value
                    }                
                }
            }
            return grid[sAlength-1,sBlength-1];                                     //return opposite corner value (aka the cost)
        }
    }
}
