import java.util.*; 

import javax.swing.*;

import java.awt.*;
import java.io.*;

public class PermComb {
     /*
Write a program that reads in a text file. The name will be supplied as a command-line parameter.
The first section of the file lists the kinds of rice available, one per line.
The second section lists the kinds of meat available, one per line.
The third section lists the different ways of preparing the egg, one per line.
The fourth section lists the varieties of gravy offered, one per line.
NOTE: Each section is separated by a line with three asterisks ***.
After reading in the file, your program will print out all the possible kinds of loco mocos you can make from the ingredients mentioned.
Finally, your program must say how many loco mocos it listed.
*/
	public static String rice; 
	public static String meat;
	public static String eggs; 
	public static String gravy;
	//read in file
    
public static void main(String args[]) {
		
		String filename = (args[0]);
		
		File f = new File(filename); 

		try {
			//initialize string delimiter. separates sections by *** and extra break
			Scanner s = new Scanner(f).useDelimiter("\\*\\*\\*\\r?\\n");
			//places each item into string
			rice = s.next();
			meat = s.next();
			eggs = s.next();
			gravy = s.next();
			
			//elements of string are placed into array
			String[] rice1 = rice.split("\\r?\\n");
			String[] meat1 = meat.split("\\r?\\n");
			String[] eggs1 = eggs.split("\\r?\\n");
			String[] gravy1 = gravy.split("\\r?\\n"); 

			//begin counter
			int count = 0;
			
			//loop through all categories
			 	for(String b: rice1) {//for first element in rice1
					for(String c: meat1) {//for first element in meat1
					  for(String d: eggs1) {//for first element in eggs1
					        for(String h: gravy1) {//for first element in gravy1
						System.out.println(b + " rice, " + c + " meat, " + d + " eggs, " + h + " gravy.");
						//print b, c, d, e
						System.out.print("\n");
						count++; //counts 1 each time a combination is formed (increments)
							}
						}
					}
				} 
				//then loop through for 2nd element of all lists
			 	System.out.print(count + " loco mocos! Try them all!");
			 	
			} catch (FileNotFoundException e) {
			System.out.println("Could not find " + filename);
			}
			
	}
}