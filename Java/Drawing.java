import java.util.Scanner;
import javax.swing.*;
import java.awt.*;
import java.io.*;
/*
p.u.paint (...)
    read first 2 #'s from file.
    setSize(w,h)
    keep looping until end of file --while loop
        read next integer(n) //value of first number in row.... nextInt
                            <---- create 2 arrays (ints) of size[n]
        loop (n) times ---for loop
            ***read next int(x)** don't do these
            ***read next int(y)**
            
            xp[i] = s.nextInt()
            yp[i] = s.nextInt()
            
        }
        drawPolyLine(int[] xPoints, <---draw multiple lines with a single call rather than drawLine a billion times
                    int[] yPoints,
                    int[] nPoints)
         }
			g.drawPolyLine(xArray, yArray, n)
					
					}
					*/
public class Drawing extends JApplet {
 
	String dafile; 
	
	@Override
	public void init() {
		
		dafile = JOptionPane.showInputDialog("Input file name, please!");
		
		//Scanner s = new Scanner(f); 
	
	}
	@Override
	public void paint(Graphics g) {//scope rules--what are they?
		try {
			File f = new File(dafile); //reads in file
			Scanner s = new Scanner(f); //scans file/looks at all elements
			int w = s.nextInt();  //place 1st & 2nd numbers into variables
			int h = s.nextInt(); //these are the x,y coordinates of the shapes
			setSize(w,h);//sets the size of applet window
				
				while (s.hasNext() == true) { //as long as there is another token no white space
				
					int n = s.nextInt(); //places 3rd integer into a variable tells you how many
										// pairs
					int[] xp = new int[n]; //creates array for x values
					int[] yp = new int[n]; //creates array for y values
					
					for(int i=0; i < n; i++) {
						xp[i] = s.nextInt(); //places 4th int into array xp then stops
						yp[i] = s.nextInt();//places 5th int into array yp then stops
						//loops back to insert 6th into xp then 7th into yp 
					}
					g.setColor(Color.BLACK);//defines color of line
					g.drawPolyline(xp, yp, n);//draws a line from x coordinates
												// y then uses n as the starting point
				}
				
			} catch (FileNotFoundException e) {
				System.out.println("Cannot find file!");
			}
	}

}
//classes are the sets and methods are the elements in the set