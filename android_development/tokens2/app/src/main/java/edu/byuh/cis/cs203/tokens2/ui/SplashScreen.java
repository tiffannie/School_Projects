package edu.byuh.cis.cs203.tokens2.ui;

import android.app.AlertDialog;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Canvas;
import android.graphics.PointF;
import android.graphics.RectF;
import android.view.MotionEvent;
import edu.byuh.cis.cs203.tokens2.R;
import edu.byuh.cis.cs203.tokens2.logic.GameMode;

/**
 * Created by tiffannie on 2/8/2017.
 */

public class SplashScreen extends GameScreen {

    private Bitmap image;

    public SplashScreen(GameView v, PointF dim) {
        super(v,dim);
        image = BitmapFactory.decodeResource(getResources(), R.drawable.splash);
        image = Bitmap.createScaledBitmap(image, (int)width(), (int)height(), true);
    }
    @Override
    public void draw(Canvas c) {
        c.drawBitmap(image, 0, 0, null);
    }

    @Override
    public void handleTouch(MotionEvent m) {
        if (m.getAction() == MotionEvent.ACTION_DOWN) {
            RectF helpButton = new RectF(67/600f*width(), 841/1024f*height(), 160/600f*width(), 951/1024f*height());
            //int duckID;
            float x = m.getX();
            float y = m.getY();
            if (x > width()*0.183 && x < width()*.815 && y < height()*.464 && y > 0.396*height()) {
                //the user pressed the bottom-left button!
                getParent().switchToSlideScreen(GameMode.ONE_PLAYER);
            } else if (x > width()*0.183 && x < width()*.815 && y<height()*.653 && y>0.558*height()) {
                //the user pressed the bottom-right button!
                getParent().switchToSlideScreen(GameMode.TWO_PLAYER);
            } else if (x > width()*0.112 && x < width()*.258 && y<height()*0.929 && y>.825*height()) {
                AlertDialog.Builder ab = new AlertDialog.Builder(getContext());
                ab.setTitle(R.string.about);
                ab.setNeutralButton(R.string.ok, null);
                ab.setMessage(R.string.about_content);
                ab.show();
            } else if(x > width()*.72 && x < width()*.867 && y<height()*0.931 && y>.814*height()){
                //launch the preferences screen
                Intent i = new Intent(getContext(), Prefs.class);
                getContext().startActivity(i);
            }


        }
    }
    public void handleOnPause() {
        //TODO override
    }

    public void handleOnResume() {

    }
}
