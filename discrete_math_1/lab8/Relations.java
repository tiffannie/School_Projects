package rel;

import java.util.*; 
import javax.swing.*;
import java.awt.*;
import java.io.*;

public class Relations {
     /*
Create a program that does the following:
Accept exactly one command-line argument that gives the name of your input file.
If the file specified on the command line does not exist, exit the program with an error message: Could not find input file.
Read in the file. The first line of the file will contain all the elments of the set. The remaining lines of the file contain the ordered pairs that make up a binary relation on that set.
Print out the set and the relation as nicely-formatted sets.
Test whether the relation is reflexive, and print out yes or no.
Test whether the relation is symmetric, and print out yes or no.
Format your output as shown in the example test files below.
*/
	
	//read in file
	public static void main(String args[]) {
		
		int count = 1;
		int count1 = 0;
		int count2 = 0;
		boolean reflexive = false;
		boolean symmetric = false;
		
		ArrayList<String> element1 = new ArrayList<>();
		ArrayList<String> element = new ArrayList<>();
		ArrayList<String> wholefile = new ArrayList<>();
		
		//read in file
		//String filename = (args[0]);
		String filename = ("input3.txt");
		File f = new File(filename); 
		
		
		try {
			//scan file in
			Scanner s = new Scanner(f);			
			
			
			//PRINT SET
			String a = s.nextLine();
			String[] set = a.split(" ");
			System.out.print("Set: {");
			for(String g : set){
				if(count == set.length) { //if it's the last one, print element without comma
					System.out.println(g + "}");
				} 
				else {//if it isn't last element, print with comma
					System.out.print(g + ", ");
				}
				count++;
			}
			
			
			//place lines into array list not-split
			while(s.hasNextLine()){
				wholefile.add(s.nextLine());
				count++;
			}
			
			//split items in wholefile. x to their own and y to their own
			for (int i = 0; i < wholefile.size(); i ++){
				String[] z; //INSTANTIATE array here. it will dump the array at each loop
				z = wholefile.get(i).split(" ");
				element.add(z[0]); //because it resets, you can directly reference the first and 2nd element of each line. it's reset every loop
				element1.add(z[1]);

			}
			System.out.print("Relation: {");
			for(int m = 0; m < element.size(); m ++){
				System.out.print("(" + element.get(m) + ", " + element1.get(m) + "), ");
			}
			System.out.print("}\n");
			
			//REFLEXIVE
			for(int l = 0; l < element.size(); l++){//STRING COMPARISON is .equals not ==
				if(element.get(l).equals(element1.get(l))){
					count1 ++;	
				}
			}
			if (count1 == set.length) {
				reflexive = true;
			}	
			if(reflexive == true){//two equals is comparing. one equals is setting
				System.out.println("Reflexive: Yes");
			}
			else {
				System.out.println("Reflexive: No");
			}
			
			//SYMMETRIC
			for(int n = 0; n < wholefile.size(); n ++){
				String[] o;
				o = wholefile.get(n).split(" ");
				String p = o[0];
				String q = o[1];
				for(int r = 0; r < wholefile.size(); r++){
					String[] t;
					t = wholefile.get(r).split(" ");
					String b = t[0];
					String c = t[1];
					if(b.equals(q) && c.equals(p)){
						count2 ++;
					}
				}
			}
			if(count2 == wholefile.size()){
				symmetric = true;
			}
			if(symmetric == true){//two equals is comparing. one equals is setting
				System.out.println("Symmetric: Yes");
			}
			else {
				System.out.println("Symmetric: No");
			}
			
		} catch (FileNotFoundException e) {
			System.out.println("Could not find " + filename);
		}
	}
}