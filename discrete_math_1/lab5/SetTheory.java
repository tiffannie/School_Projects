import java.util.*; 
import javax.swing.*;
import java.awt.*;
import java.io.*;

public class SetTheory {
     /*
Create a program that does the following:
Accept exactly one command-line argument that gives the name of your input file.
If the file specified on the command line does not exist, exit the program with an error message: Could not find input file.
Read in the file. It will contain two lines. The first line lists the elements in set A. The second line lists the elements in set B.
Print out eight results:
A in set notation
B in set notation
A n B
A ? B
A – B
B – A
A × B
B × A
*/
    public static String[] lola; 
	public static String[] pamela;
	//read in file
	public static void main(String args[]) {
		//read in file
		
		String filename = (args[0]);
		//String filename = ("input.txt");
		File f = new File(filename); 
		
		ArrayList<String> seta = new ArrayList<>();
   	 	ArrayList<String> setb = new ArrayList<>();  
		try {
			Scanner s = new Scanner(f);
			 
			    String p = s.nextLine(); 
				lola = p.split(" ");
    	 		String bob = s.nextLine();
				pamela = bob.split(" ");
				
				//A IN SET NOTATIO
				System.out.print("A = { ");	
				int count = 1;
				for(String b : lola) {
					seta.add(b);
					
					if(count == lola.length) {//if it's the last one, print without the comma
					//to see if it is last element of entire list, not just part (which would be
					// seta)
					    System.out.print(b);
					} 
					else {//else. if it isn't the last one, print with a comma
					    System.out.print(b + ", ");
					}
					count = count + 1;
				}
				System.out.print(" }" + "\n");
				
				//B IN SET NOTATION
				System.out.print("B = { ");
				
				for(String b : pamela) {
					setb.add(b);
					System.out.print(b + ", ");
				}
				System.out.print(" }" + "\n");
			
				//A INTERSECT B
				System.out.print("A intersect B = {");
				for(String b : lola) {
					seta.add(b);
					for(String c : pamela) {
						seta.add(c);
						if(b.equals(c)) {
						System.out.print( b + ", ");
						}
					}
				}
				System.out.print("}\n" );
				
				//A UNION B
				ArrayList<String> union = new ArrayList<>();
				
				System.out.print("A union B = {");
				for(String b : lola) {
					
					if(!union.contains(b)) {//if b is not already in
						//union, add b
						union.add(b);
					}
					for(String c : pamela) {//for each b you are going 
						//through each c and seeing if c is not in union 
						//array already						
						if(!union.contains(c)) {
							union.add(c); //if c isn't already
							//in union, add c
						} 
					}
				}
				for(String u : union) {
					System.out.print(u + ", ");
				}
				System.out.print("}\n");
				
				//A - B
				ArrayList<String> minus = new ArrayList<>();
				
				System.out.print("A - B = {");
				for(String b : lola) {
					if(!minus.contains(b)) {//if b is not already in
						//array list minus, add b
						minus.add(b);
					}
					for(String c : pamela) {//for each b you are going 
						//through each c and seeing if c is not in minus 
						//array already						
						if(minus.contains(c)) {
							minus.remove(c); //if c is already in minus
							//take away c
						} 
					}
				}
				for(String u : minus) {
					
					System.out.print(u + ", ");
				}
				System.out.print("}\n");
				
				//B - A
				ArrayList<String> minus1 = new ArrayList<>();
				
				System.out.print("B - A = {");
				for(String c : pamela) {
					if(!minus1.contains(c)) {//if c is not already in
						//array list minus, add c
						minus1.add(c);
					}
					for(String b : lola) {//for each b you are going 
						//through each b and seeing if b is not in minus1 
						//array already						
						if(minus1.contains(b)) {
							minus1.remove(b); //if b is already in minus1
							//take away b
						} 
					}
				}
				for(String u : minus1) {
					
					System.out.print(u + ", ");
				}
				System.out.print("}\n");
				
				//A X B		
				System.out.print("A X B = {");
				for(String b: lola) {//for first element in lola
					for(String c: pamela) {//for first element in pamela
						System.out.print("(" + b + ", " + c + "), ");
						//print b and c
					}
				}//then loop through for 2nd element of both array lists
				
				System.out.print("}\n");
				
				//B X A
				System.out.print("B X A = {");
				for(String c: pamela) {//for first element in pamela
					for(String b: lola) {//for first element in lola
						System.out.print("(" + c + ", " + b + "), ");
						//print c and b
					}
				}//then loop through for 2nd element of both array lists
				
				System.out.print("}\n");
				
				s.close();//closes the file
			
		} catch (FileNotFoundException e) {
			System.out.println("Could not find " + filename);
		}
	}
}