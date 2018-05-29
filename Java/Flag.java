import javax.swing.*;
import java.awt.*;

public class Flag extends JApplet {
	

	@Override
	public void paint(Graphics g) {
		int x = 10;
		int y = 10;
		int w = getWidth(); //when you expand the window it paints the color 4ever
		int h = getHeight(); 
		
		//declaring variables
		int blueBoxWidth = w * 2 / 5;
		int numStarRows = 6;
		int numStarCols = 8;
		int starWidth= blueBoxWidth / numStarCols;
		int numStripes = 13;
		int stripeWidth = w;
		int stripeHeight = h / numStripes;
		int blueBoxHeight = 7 * stripeHeight;
		int starHeight = blueBoxHeight / numStarRows;
		 
		x = 0;
		y = 0;
		
		//fill background white
		g.setColor(Color.WHITE);
		g.fillRect(0,0,w,h);
		
		// fill red stripes
		for (int i=0; i < numStripes; i+=2) {
			g.setColor(Color.RED);
			g.fillRect (x, y, stripeWidth, stripeHeight);
			y += stripeHeight * 2;
		}
		
		//blue rectangle
		g.setColor(Color.BLUE);
		g.fillRect(0,0,blueBoxWidth, blueBoxHeight);
		
		
		//star nested loop
		g.setColor(Color.WHITE);
		for (int j=0; j < numStarCols; j++) {
			for (int i =0; i < numStarRows; i++) {
				g.fillOval(starWidth * j,starHeight*i,starWidth, starHeight);
				//x += numStarRows *2;
			}
			
		}
		
		
	}
	
}