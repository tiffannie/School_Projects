package edu.byuh.cis.cs203.tokens2.ui;

import android.content.res.Resources;
import android.graphics.Bitmap;
import android.graphics.Canvas;
import android.graphics.Paint;
import android.graphics.PointF;
import android.graphics.RectF;
import android.util.Log;

import edu.byuh.cis.cs203.tokens2.logic.TickListener;

public class GuiToken implements TickListener {

	private Bitmap image;
	private RectF bounds;
	private PointF velocity;
	private PointF destination;
	private float tolerance;
	private GridPosition pos;
	private static Paint paint = new Paint();
	private static int movers = 0;
	private boolean falling = false;
	private static class GridPosition {
		char row, column;
	}


	public GuiToken(Bitmap img, RectF bounds, char buttonOfOrigin) {
		image = img;
		this.bounds = new RectF(bounds);
		velocity = new PointF();
		destination = new PointF();
		tolerance = bounds.height()/10f;
		pos = new GridPosition();
		if (buttonOfOrigin >= 'A' && buttonOfOrigin <= 'E') {
			pos.row = buttonOfOrigin;
			pos.column = '1'-1;
		} else {
			pos.row = 'A'-1;
			pos.column = buttonOfOrigin;
		}
	}

	public boolean matches(char label) {
		return (pos.row == label || pos.column == label);
	}

	public boolean matches(char row, char col) {
		return (pos.row == row && pos.column == col);
	}

	public boolean isNeighborTo(GuiToken other) {
		return (this != other &&
				Math.abs(this.pos.row-other.pos.row) <=1 &&
				Math.abs(this.pos.column-other.pos.column) <=1);
	}
		
	public void draw(Canvas c) {
		c.drawBitmap(image, bounds.left, bounds.top, paint);
	}

	private void move() {
		if(falling){
			Log.d("aaaaa", "he's dead");
			velocity.y *= 2;
			bounds.offset(velocity.x, velocity.y);
		} else {
			if (velocity.x != 0 || velocity.y != 0) {
				float dx = destination.x - bounds.left;
				float dy = destination.y - bounds.top;
				if (PointF.length(dx, dy) < tolerance) {
					bounds.offsetTo(destination.x, destination.y);
					velocity.set(0,0);
					movers--;
					if(pos.row > 'E' || pos.column > '5') {//is token off the grid?????
						velocity.x = 0;
						velocity.y = 1;
						falling = true;
					}
				} else {
					bounds.offset(velocity.x, velocity.y);
				}
			}
		}

	}

	public void moveDown() {
		moveTo(bounds.left, bounds.top+bounds.height());
		pos.row++;
	}

	public void moveRight() {
		moveTo(bounds.left+bounds.width(), bounds.top);
		pos.column++;
	}

	public boolean isMoving() {
		return (velocity.x > 0 || velocity.y > 0);
	}

	private void moveTo(float x, float y) {
		movers++;
		destination.set(x,y);
		float dx = destination.x - bounds.left;
		float dy = destination.y - bounds.top;
		velocity.x = dx/11f;
		velocity.y = dy/11f;
	}

	@Override
	public void onTick() {
		move();		
	}
	
	public static boolean moving() {
		return (movers > 0);
	}

	public static int getScreenHeight() {
		return Resources.getSystem().getDisplayMetrics().heightPixels;
	}
	/**
	 * checks if the top of its bounds are greater than height of the screen.
	 * @param
	 * @return boolean
     */
	public boolean isInvisible(){
		return bounds.top > getScreenHeight();
	}

}

