package edu.byuh.cis.cs203.tokens2.ui;

import android.content.Context;
import android.content.res.Resources;
import android.graphics.Canvas;
import android.graphics.PointF;
import android.view.MotionEvent;

/**
 * Created by tiffannie on 2/8/2017.
 */

public abstract class GameScreen {

    private GameView parent;
    private float w, h;

    public GameScreen(GameView v, PointF dimension) {
        parent = v;
        w = dimension.x;
        h = dimension.y;
    }

    public float width() {
        return w;
    }

    public float height() {
        return h;
    }

    public abstract void draw(Canvas c);
    public abstract void handleTouch(MotionEvent m);

    public Resources getResources() {
        return parent.getResources();
    }

    public Context getContext() {
        return parent.getContext();
    }

    public GameView getParent() {
        return parent;
    }

    public void invalidate() {
        parent.invalidate();
    }

    public void handleOnPause() {
        //TODO override
    }

    public void handleOnResume() {

    }

}