package edu.byuh.cis.cs203.tokens2.ui;

import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.graphics.Paint.Style;
import android.graphics.RectF;

public class Grid {

	private final int dim = 5;
	private float lineWidth;
	private Paint paint;
	private RectF bounds;
	private float cellWidth;
	
	public Grid(float x, float y, float cellWidth) {
		this.cellWidth = cellWidth;
		lineWidth = cellWidth/20;
		bounds = new RectF(x, y, x+cellWidth*dim, y+cellWidth*dim);
		paint = new Paint();
		paint.setStrokeWidth(lineWidth);
		paint.setColor(Color.BLACK);
		paint.setStyle(Style.STROKE);
	}
	
	public boolean contains(float x, float y) {
		return bounds.contains(x, y);
	}
	
	public float getTop() {
		return bounds.top;
	}
	
	public void draw(Canvas c) {
		for (int i=0; i<=dim; ++i) {
			c.drawLine(bounds.left, bounds.top + cellWidth*i, bounds.right, bounds.top + cellWidth*i, paint);
		}
		for (int i=0; i<=dim; ++i) {
			c.drawLine(bounds.left + cellWidth*i, bounds.top, bounds.left + cellWidth*i, bounds.bottom, paint);
		}
	}

}
