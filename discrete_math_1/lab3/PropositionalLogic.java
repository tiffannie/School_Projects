import java.util.Scanner;
import javax.swing.*;
import java.awt.*;
import java.io.*;
import java.util.*;

public class PropositionalLogic {
	
    /*Write a program that reads in a text file. The name of the file will be supplied as a command-line parameter. If the command-line parameter is missing, or if the file is not found, report an appropriate error message and terminate.

Your program will read each line of the file and determine whether that line is a statement or not a statement. Print each line to the console, along with the words STATEMENT or NOT A STATEMENT, as appropriate.

But how do you teach a computer to discern a statement from a non-statement? I do not know of a 100% accurate technique. But here are some heuristics you may wish to try. Can you think of some others?

If the line ends with a question mark, it's not a stat ement.
If the line is just a single word, it's not a statement.
If the line begins with a question word such as what, where, how, why, who, or when, it's not a statement
If the line contains any pronouns, such as he, she, it, or they, then it's not a statement.
 */
	
	public static void main(String[] args) {
	
		int count = 0;
		
		File f = new File(args[0]);
		
		try { 
			Scanner scn = new Scanner(f);
			
			while (scn.hasNextLine()) { //while file does not end
			//if hasnextline returns true, there is still info left in file
				
				String line = scn.nextLine();
				String[] words = line.split(" "); //place items that aren'that
				String[] individualchar = line.split("");
				int wordarray = 0;
				int chararray = 0;
				for(String x : words) {
					if (x.contains("She") || 
						x.contains("He") ||
						x.contains("They") ||
						x.contains("It")) {
						wordarray = 1;
						break;
					} else {
						wordarray = 0;
					}
				}
				for(String w: individualchar) {
					if (w.contains("?") ||
						w.contains("(")) {
						chararray = 1;
						break;
					} else {
						chararray = 0;
					}
				}
				//spaces in an array. each line is analyzed individually
				//String[] otherchar = line.split(); 
			//find single words
			if (words.length == 1) {
					System.out.println(line + " NOT A STATEMENT");	
			} else if (words[0].contains("What") ||
					words[0].contains("Why") ||
					words[0].contains("How") ||
					words[0].contains("Who") ||
					words[0].contains("Where") ||
					words[0].contains("When") ||
					words[0].contains("what") ||
					words[0].contains("why") ||
					words[0].contains("how") ||
					words[0].contains("who") ||
					words[0].contains("where") ||
					words[0].contains("when")) {
						System.out.println(line + " NOT A STATEMENT");
							
			} else if (wordarray == 1) {
				System.out.println(line + " NOT A STATEMENT");
			} else if (chararray == 1) {
				System.out.println(line + " NOT A STATEMENT");
			} else {
					System.out.println(line + "  THIS IS A STATEMENT");
				}
			}
			//place each pronoun thingy in it's own if statement
			//use .contains
			//use foreach loops. for each of items inside of array and split again with no spaces
			//use .contains to look for ? =
		}	catch (FileNotFoundException e) {
				System.out.println("please enter a file name");
		}
	}
}