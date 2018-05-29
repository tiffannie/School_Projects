package edu.byuh.cis.cs203.tokens2.ui;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.graphics.PointF;
import android.os.AsyncTask;
import android.view.Gravity;
import android.view.MotionEvent;
import android.view.View;
import android.widget.Toast;

import edu.byuh.cis.cs203.tokens2.R;
import edu.byuh.cis.cs203.tokens2.logic.Difficulty;
import edu.byuh.cis.cs203.tokens2.logic.GameBoard;
import edu.byuh.cis.cs203.tokens2.logic.GameMode;
import edu.byuh.cis.cs203.tokens2.logic.Player;
import edu.byuh.cis.cs203.tokens2.logic.TickListener;

import static android.R.attr.id;
import static edu.byuh.cis.cs203.tokens2.logic.GameMode.ONE_PLAYER;
import static edu.byuh.cis.cs203.tokens2.logic.GameMode.TWO_PLAYER;

public class GameView extends View {

	private boolean initialized;
	private GameScreen current;
	private SlideScreen ds;
	private SplashScreen ss;
	private PointF screenSize;
	private GameMode m;

	public GameView(Context c) {
		super(c);
		initialized = false;

	}

	@Override
	public void onDraw(Canvas c) {
		if (initialized == false) {
			screenSize = new PointF(c.getWidth(), c.getHeight());
			ss = new SplashScreen(this, screenSize);
			current = ss;
			initialized = true;
		}
		current.draw(c);
	}

	@Override
	public boolean onTouchEvent(MotionEvent m) {
		current.handleTouch(m);
		return true;
	}

	public void switchToSlideScreen(GameMode m) {
		ds = new SlideScreen(this, screenSize, m);
		current = ds;
		invalidate();
	}

	public void handleOnPause() {
		if (current != null) {
			current.handleOnPause();
		}
	}

	public void handleOnResume() {
		if (current != null) {
			current.handleOnResume();
		}
	}

}
