using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

/*
 * calculate the probabilities of each character in a file and implement the Huffman Encoding algorithm in C# to determine the compressed binary representation of each character.
 * 
 * 
 * 
 * */

namespace Huffman
{
    class Program
    {
        static void Main(string[] args)
        {                                                               //C:\Users\tiffannie\Desktop\test.txt
            List<string> words = new List<string>();                  //read in file
            String line;
            String line1;
            using (StreamReader sr = new StreamReader("C:/Users/student/Desktop/test1.txt"))
            {
                //Read the stream to a string, and write the string to the console.
                line = sr.ReadToEnd();
                line1 = sr.ReadToEnd();
            }
            char[] hum = line.ToCharArray();                          //list of individual characters in text
      
            List<char> visited = new List<char>();                   //list of unique characters in text
            foreach(char a in hum){
                if (!visited.Contains(a))
                {
                    visited.Add(a);
                }
            }
            Dictionary<char, int> myDict = new Dictionary<char, int>();//dictionary with letter and number of times it appears in the text

            for (int a = 0; a < hum.Count(); a++)                     //for every character in the text
            {
                Console.WriteLine(hum.ElementAt<char>(a));
                if (!myDict.ContainsKey(hum.ElementAt<char>(a)))      //if the character isn't in the dictionary
                {
                    myDict.Add((hum.ElementAt<char>(a)), 1);          //add the character to the dictionary with 1 as
                }                                                     //its value since it has only appeared once so far
                else
                {
                    myDict[(hum.ElementAt<char>(a))] += 1;            //otherwise add 1 to the character's value as it
                }                                                     //appears in the text
            }
                                                                      //print keys and values in dictionary
            foreach (KeyValuePair<char, int> pair in myDict)
            {
                Console.WriteLine("Key: {0} Values: {1}", pair.Key, pair.Value);
            }
            //*************************************************************************************************************************
            List<Node> nodes = new List<Node>();                      //convert the key/value pairs into nodes
            foreach (KeyValuePair<char, int> pair in myDict)
            {
                Node a = new Node(pair.Key, pair.Value);              //uses node constructor that takes in a letter and frequency
                nodes.Add(a);
            }
            nodes = nodes.OrderBy(n => n.frequency).ToList();         //order this nodes list by its frequency then save to a list
                                                            //specifies parameter that's named n and returns the value of n.frequency
            //************************************************************************************************************************
            
            int num = nodes.Count;
            for (int a = 0; a < num-1; a++)
            {
                Node i = nodes[0];
                Node j = nodes[1];                                    //creating node with lowest node values
                Node k = new Node(i, j);                              //frequency is set inside constructor
                //f[k] = f[i] + f[j] 
                nodes.Add(k);
                //insert(H,k)
                                                                    //since using lists, the removing has to be done manually
                nodes.RemoveAt(0);                                  //same as pop in a stack
                nodes.RemoveAt(0);                                  //you already removed the top one and now you need to remove the second one which is the new top
                                                                    //we also have to order the list again (the priority queue would do this automatically but we're using lists
                nodes = nodes.OrderBy(n => n.frequency).ToList();
            }
            //when you print out nodes, it should print the total number of characters in the file
            //*************************************************************************************************************************
            //expanding the tree
            List<Node> tree = new List<Node>();
            tree.Add(nodes[0]);
            for (int a = 0; a < (num * 2)-1; a++)                 //each node has left & right child so its the total amt of doubles
            {
                if (tree[a].leftChild != null)                      //if it has a left child
                {
                    tree.Add(tree[a].leftChild);                    //add it to the tree. the next index is the left child
                }
                if (tree[a].rightChild != null)                     //the right child is then checked
                {
                    tree.Add(tree[a].rightChild);                   //it's added to the index right next to the left child
                }
                else
                {
                    Console.WriteLine("barren");
                }
            }//when this loops, we will be on the LEFT child of the FIRST node (tree[0])
                                                                       //as we go down the list we start with parent->left->right
            Dictionary<char, string> encode = new Dictionary<char, string>();//we have chars and want to change to ints 0's & 1's
            Dictionary<string, char> decode = new Dictionary<string, char>();//we have 0's & 1's & want replace with the chars
            foreach (char b in visited)                            //foreach unique character                               
            {
                foreach (Node node in tree)                         //foreach node in the tree
                {                                                   //goes through list of nodes in tree
                    if (b == node.letter)                           //finds the node with character b. find starting node to traverse upwards
                    {
                        string enc = getZeroesAndOnes(node, tree, nodes[0].frequency);//looks for left or right child
                        encode.Add(b, enc);                         //get character and it's encoding (its 0's and 1's
                        decode.Add(enc, b);                         //get 0's and 1's and characters
                    }
                }
            }
            foreach (KeyValuePair<char, string> pair in encode)     //print results
            {
                Console.WriteLine("Character: {0} Binary Encoding: {1}", pair.Key, pair.Value);
                Console.WriteLine("");
            }
            foreach (KeyValuePair<string, char> pair in decode)
            {
                Console.WriteLine("Binary Encoding: {0} Character: {1}", pair.Key, pair.Value);
                Console.WriteLine("");
            }
            List<string> file = new List<string>();
            file = encodeMess(encode, line);                         //dictionary and text file
            /*for (int a = 0; a < file.Count; a++)                   //print encoded message
            {
                Console.Write(file[a] + " ");
            }*/
            string file1;
            file1 = decodeMess(decode, file);
            
            //Console.WriteLine(file1);     //print decoded message
           
        }
        //************************************************************************************************************************************
        public class Node
        {
            public char letter;
            public int frequency;
            public double percentage;
            public Node leftChild;
            public Node rightChild;

            public Node(char c, int frequency)
            {
                letter = c;
                this.frequency = frequency;
            }
            public Node(Node leftChild, Node rightChild)
            {
                this.leftChild = leftChild;
                this.rightChild = rightChild;
                frequency = leftChild.frequency + rightChild.frequency;
            }

            public override bool Equals(object obj)
            {
                char c = (char)obj;
                return letter == c;
            }
            public void percent(double total)
            {
                percentage = ((double)frequency / total) * 100;
            }
        }
        public static string getZeroesAndOnes(Node n, List<Node> l, int length)
        {
            List<string> en = new List<string>();
            Node next = n;                                          //start at starting node
            while (next.frequency != length)                        //top node's frequency(value) is equal to the length of all the values of the nodes beneath
            {

                foreach (Node o in l)                               //loop through each node in list
                {
                    if (o.leftChild == next)                        //if the left child is equal to the starting node
                    {
                                                                    //that means this o is the parent of next
                                                                    //since it's left, left is always 0
                        en.Insert(0, "0");                          //add elements to the start of list. similar to a stack
                        next = o;                                   //o is the parent of next, therefore we set o to next so we can find it's(o) parent
                    }
                    else if (o.rightChild == next)
                    {               
                                                                   //since on right side, insert a 1
                        en.Insert(0, "1");
                        next = o;                                   //set it to o because it's its parent
                    }
                }
            }
            return String.Join("", en);
        }
        static List<string> encodeMess(Dictionary<char, string> d, string m)
        {
            List<string> enc = new List<string>();
            foreach(char c in m)
            {
                string encode;
                d.TryGetValue(c, out encode);
                enc.Add(encode);
            }
            return enc;
        }
        static string decodeMess(Dictionary<string, char> d, List<string> m)
        {
            List<char> dec = new List<char>();
            foreach (string s in m)
            {
                char c;
                d.TryGetValue(s, out c);
                dec.Add(c);
            }
            return string.Join("", dec);
        }
    }
}