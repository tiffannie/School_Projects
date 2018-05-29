package edu.byuh.cis.cs203.tokens2.ui;

import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Canvas;
import android.graphics.Paint;
import android.graphics.RectF;
import android.view.View;

import edu.byuh.cis.cs203.tokens2.R;

public class GuiButton {

	private RectF bounds;
	private boolean pressed;
	private static Paint paint;
	private static Bitmap pressedButton, unpressedButton;
	private static boolean firstLoad = true;
	private char label;
	
	public GuiButton(char name, View parent, float x, float y, float width) {
		label = name;
		bounds = new RectF(x, y, x+width, y+width);
		pressed = false;
		if (firstLoad) {
			firstLoad = false;
			pressedButton = BitmapFactory.decodeResource(parent.getResources(), R.drawable.glossy_red_button);
			unpressedButton = BitmapFactory.decodeResource(parent.getResources(), R.drawable.button_glossy_blue);
			pressedButton = Bitmap.createScaledBitmap(pressedButton, (int)width, (int)width, true);
			unpressedButton = Bitmap.createScaledBitmap(unpressedButton, (int)width, (int)width, true);
			paint = new Paint();
		}
	}
	
	public boolean contains(float x, float y) {
		return bounds.contains(x, y);
	}
	
	public void press() {
		pressed = true;
	}
	
	public void release() {
		pressed = false;
	}
	
	public void draw(Canvas c) {
		if (pressed) {
			c.drawBitmap(pressedButton, bounds.left, bounds.top, paint);
		} else {
			c.drawBitmap(unpressedButton, bounds.left, bounds.top, paint);
		}
	}
	
//	public PointF getCorner() {
//		return corner;
//	}
	
	public char getLabel() {
		return label;
	}
	
	public boolean isTopButton() {
		return (label >= '1' && label <= '5');
	}
	
	public boolean isLeftButton() {
		return (label >= 'A' && label <= 'E');
	}

	public boolean matches(char nom) {
		return (label == nom);
	}

	public RectF getBounds() {
		return bounds;
	}

}
