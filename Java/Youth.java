import javax.swing.*;
import java.awt.*;


public class Youth extends JApplet {

	int age;
	String gender;
	
	@Override
	public void init() { //creating input commands and dialogue box
		String bob = JOptionPane.showInputDialog("What is your gender? (m/f)");
		
		String Input = JOptionPane.showInputDialog("What is your age?");
		age = Integer.parseInt(Input);
		gender = bob;
	}
	
	@Override
	public void paint(Graphics g) { //church class logic
		
		//start writing black text
		g.setColor(Color.BLACK);
		//for girls
		if (gender.equalsIgnoreCase("f")) {
			if (age < 12 && age >= 0 ) {
				g.drawString("You go to Primary!", 10, 30);
				ImageIcon prim = new ImageIcon("rockchapel.jpg");
				prim.paintIcon(null, g, 50, 50);
			} else if (age >= 12 && age < 14) {
				g.drawString("You belong to the Beehive class.", 10, 30);
				ImageIcon bee = new ImageIcon("beehive.jpg");
				bee.paintIcon(null, g, 50, 50);
			} else if (age >= 14 && age < 16) {
				g.drawString("You belong to the Miamaid class.", 10, 30);
				ImageIcon mia = new ImageIcon("miamaid.gif");
				mia.paintIcon(null, g, 50, 50);
			} else if (age >= 16 && age < 18) {
				g.drawString("You belong to the Laurel class.", 10, 30);
				ImageIcon yw = new ImageIcon("laurel.gif");
				yw.paintIcon(null, g, 50, 50);
			} else if (age >= 18 && age < 119) {
				g.drawString("You are a member of the Relief Society.", 10, 30);
				ImageIcon rs = new ImageIcon("RS.jpg");
				rs.paintIcon(null, g, 50, 50);
			} else {
				g.drawString("Are you sure you typed that correctly?", 10, 30);
			}
		} 
		//for the boys
		else if (gender.equalsIgnoreCase("m")) {
			if (age < 12) {
				g.drawString("You go to Primary!", 10, 30);
				ImageIcon prim = new ImageIcon("rockchapel.jpg");
				prim.paintIcon(null, g, 50, 50);
			} else if (age >= 12 && age < 14) {
				g.drawString("You belong to the Deacons quorum.", 10, 30);
				ImageIcon deac = new ImageIcon("deacon.jpg");
				deac.paintIcon(null, g, 50, 50);
			} else if (age >= 14 && age <= 15) {
				g.drawString("You belong to the Teachers quorum.", 10, 30);
				ImageIcon teach = new ImageIcon("teacher.jpg");
				teach.paintIcon(null, g, 50, 50);
			} else if (age >= 16 && age < 18) {
				g.drawString("You belong to the Priests quorum.", 10, 30);
				ImageIcon pr = new ImageIcon("priest.jpg");
				pr.paintIcon(null, g, 50, 50);
			} else if (age >= 18 && age < 119) {
				g.drawString("You should be an elder (or a high priest)", 10, 30);
				ImageIcon eld = new ImageIcon("elders.jpg");
				eld.paintIcon(null, g, 50, 50);
			} else {
				g.drawString("Are you sure you typed that correctly?", 10, 30);
			}
		}
		else{ 
			g.drawString("FAILED TO ENTER A GENDER", 10,30);
		}
	}
}