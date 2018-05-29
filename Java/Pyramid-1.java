import javax.swing.JApplet;
import java.awt.*;

public class Pyramid extends JApplet {

	@Override
	public void paint(Graphics g) {
//to make the tomb float, uncomment the int dx
		int dx = 0;
		
//g.fillShape(x,y,width,height)
		
		//draw sky
		g.setColor(new Color(117,197,224));
		g.fillRect(0, 0, 400, 400);
		
		//draw sun
		g.setColor(new Color(255,241,89));
		g.fillOval(50,50,75,75);
		
		//draw clouds overlapping, white grayish white
		g.setColor(Color.WHITE);
		g.fillOval(75,85,90,40);//first cloud
		g.fillOval(80,75,50,50);
		g.fillOval(90,70,50,50);
		g.fillOval(100,75,50,50);
		g.setColor(new Color(240,242,242));//middle gray cloud
		g.fillOval(115,98,90,40);
		g.fillOval(123,85,50,50);
		g.fillOval(130,80,50,50);
		g.fillOval(133,96,50,50);
		g.setColor(Color.WHITE);
		g.fillOval(135,85,90,40);//third cloud
		g.fillOval(142,75,50,50);
		g.fillOval(147,70,50,50);
		g.fillOval(160,75,50,50);

		
		//draw mountains
		g.setColor(new Color(31,105,37));
		g.fillRect(0, 250, 400, 150);//base of mountain
		g.fillOval(-20, 150, 150, 300);//peaks
		g.fillOval(75,175,150,250);
		g.fillOval(185,150,150,250);
		g.fillOval(280,125,140,250);
		
		//draw grass
		g.setColor(new Color(101,163,106));
		g.fillRect(0, 280, 400, 200);
		
		//draw Pyramid
		g.setColor(new Color(237, 160, 43));
		g.fillRect(90,300+dx,200,40);//base
		g.fillRect(115,275+dx,150,50);//middle
		g.fillRect(140,250+dx,100,50);//top
		g.fillRect(160,225+dx,60,50);//tippy top
		
		//stairs
		g.setColor(new Color(128, 101, 28));
		g.fillRect(130,335+dx,125,5);//bottom
		g.fillRect(140,300+dx,100,5);//base
		g.fillRect(155,275+dx,70,5);//middle
		g.fillRect(166,250+dx,50,5);//top
		g.fillRect(180,225+dx,25,5);//tippy top
		
		
		//draw name sign
		g.setColor(new Color(240,242,242));
		g.fillRect(250,325,90,30);
		g.setColor(new Color(0, 0, 0));
		g.fillRect(285,355,1,30);
		g.drawString("Tiffannie's Tomb", 250, 342);
		
		//draw airplane
		g.setColor(new Color(212, 201, 211));
		g.fillOval(260,50,15,40);//wings
		g.fillOval(277,55,10,30);//tail
		g.setColor(new Color(240, 218, 238));
		g.fillOval(240,62,50,15);//body
		g.setColor(new Color(0, 0, 0));
		g.fillOval(243,65,5,5);//windows
		g.fillOval(249,65,5,5);
		g.fillOval(256,65,5,5);
		
		//draw airplane sign
		g.setColor(new Color(222, 194, 151));
		g.fillRect(300,60,80,30);
		g.setColor(new Color(0, 0, 0));
		g.fillRect(285,70,15,1);
		g.drawString("Go Seasiders!", 300, 75);

	}

}