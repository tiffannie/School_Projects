package edu.byuh.cis.cs203.tokens2.ui;

import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.graphics.PointF;
import android.media.MediaPlayer;
import android.os.AsyncTask;
import android.view.Gravity;
import android.view.MotionEvent;
import android.widget.Toast;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import edu.byuh.cis.cs203.tokens2.R;
import edu.byuh.cis.cs203.tokens2.logic.Difficulty;
import edu.byuh.cis.cs203.tokens2.logic.GameBoard;
import edu.byuh.cis.cs203.tokens2.logic.GameMode;
import edu.byuh.cis.cs203.tokens2.logic.Player;
import edu.byuh.cis.cs203.tokens2.logic.TickListener;

import static edu.byuh.cis.cs203.tokens2.R.xml.prefs;
import static edu.byuh.cis.cs203.tokens2.logic.GameMode.ONE_PLAYER;
import static edu.byuh.cis.cs203.tokens2.logic.GameMode.TWO_PLAYER;

/**
 * Created by tiffannie on 2/8/2017.
 */

public class SlideScreen extends GameScreen implements TickListener{

    private Grid grid;
    private boolean firstRun = false;
    private boolean windowRun;
    private boolean onePlayerMode;
    private GuiButton currentButton;
    private GuiButton copyCat;
    private GuiButton[] buttons;
    private List<GuiToken> tokens;
    private ArrayList<GuiToken> invisibleTokens = new ArrayList<>();
    private Map<Player, Bitmap> playerIcons;
    private GameBoard engine;
    private Timer timer;
    private int player0 = 0;
    private int playerX = 0;
    private Paint p;
    private GameMode gm;
    private Difficulty bo;
    private MediaPlayer music;

    //This is a constructor take a look at your constructor name vs your class name
    /**public SlideScreen(GameView v, PointF po) { //GameView

    }**/

    public SlideScreen(GameView v, PointF dim, GameMode id) {
        super(v, dim);
        p = new Paint();
        firstRun = true;
        windowRun = true;
        buttons = new GuiButton[10];
        tokens = new ArrayList<GuiToken>();
        engine = GameBoard.Singleton();
        playerIcons = new HashMap<Player, Bitmap>();

        timer = new Timer(Prefs.getSpeed(getContext()));

        timer.register(this);
        p = new Paint();
        gm = id; //initialize not instantiate, tiff
        bo = Difficulty.BUMBLING_OAF;
        music = MediaPlayer.create(getContext(), R.raw.pineapple);
        if (Prefs.musicOn(getContext())) {
            music.start();
            music.setLooping(true);
        }
    }
    @Override
    public void draw(Canvas c) {
        //super.draw(c);
        //draw(c);
        c.drawColor(Color.WHITE);
        p.setTextSize(90f);
        c.drawText("Pizza: " + String.valueOf(player0), width()/3,height()/9,p);
        c.drawText("Taco: " + String.valueOf(playerX), 2*width()/3, height()/9,p);

        if (firstRun) {
            init();
            firstRun = false;
            if(gm == GameMode.ONE_PLAYER) {
                AlertDialog.Builder b = new AlertDialog.Builder(getContext());
                AlertDialog dialog = b.setTitle(R.string.opponent_select)
                        .setMessage(R.string.choose_opponent)
                        .setPositiveButton(R.string.bumbling_oaf, new DialogInterface.OnClickListener() {
                            @Override
                            public void onClick(DialogInterface dialogInterface, int i) {
                                bo = Difficulty.BUMBLING_OAF;


                            }
                        })
                        .setNegativeButton(R.string.copy_cat, new DialogInterface.OnClickListener() {
                            @Override
                            public void onClick(DialogInterface dialogInterface, int i) {
                                bo = Difficulty.COPY_CAT;
                            }
                        })
                        .create();
                dialog.show();
                windowRun = false;
            }
        }

        grid.draw(c);

        for(GuiToken dead : invisibleTokens){
            tokens.remove(dead);
        }

        for (GuiToken t : tokens) {
            t.draw(c);
            if(t.isInvisible()) {
                timer.deregister(t); //unsubscribe
                invisibleTokens.add(t);//no concurrent modification error! booya!
            }
        }
        //remove tokens the have disappeared

        for (GuiButton b : buttons) {
            b.draw(c);
        }


    }
    private void init() {
        float w = width();
        float h = height();
        float unit = w/16f;
        float gridX = unit * 2.5f;
        float cellSize = unit * 2.3f;
        float gridY = unit * 9;
        float buttonTop = gridY - cellSize;
        float buttonLeft = gridX - cellSize;
        grid = new Grid(gridX, gridY, cellSize);
        buttons[0] = new GuiButton('1', getParent(), buttonLeft + cellSize*1, buttonTop, cellSize);
        buttons[1] = new GuiButton('2', getParent(), buttonLeft + cellSize*2, buttonTop, cellSize);
        buttons[2] = new GuiButton('3', getParent(), buttonLeft + cellSize*3, buttonTop, cellSize);
        buttons[3] = new GuiButton('4', getParent(), buttonLeft + cellSize*4, buttonTop, cellSize);
        buttons[4] = new GuiButton('5', getParent(), buttonLeft + cellSize*5, buttonTop, cellSize);
        buttons[5] = new GuiButton('A', getParent(), buttonLeft, buttonTop + cellSize*1, cellSize);
        buttons[6] = new GuiButton('B', getParent(), buttonLeft, buttonTop + cellSize*2, cellSize);
        buttons[7] = new GuiButton('C', getParent(), buttonLeft, buttonTop + cellSize*3, cellSize);
        buttons[8] = new GuiButton('D', getParent(), buttonLeft, buttonTop + cellSize*4, cellSize);
        buttons[9] = new GuiButton('E', getParent(), buttonLeft, buttonTop + cellSize*5, cellSize);
        Bitmap oImage = BitmapFactory.decodeResource(getResources(), R.drawable.taco);
        oImage = Bitmap.createScaledBitmap(oImage, (int)(cellSize),	(int)(cellSize), true);
        Bitmap xImage = BitmapFactory.decodeResource(getResources(), R.drawable.pizza);
        xImage = Bitmap.createScaledBitmap(xImage, (int)(cellSize),	(int)(cellSize), true);
        playerIcons.put(Player.X, xImage);
        playerIcons.put(Player.O, oImage);


    }

    public void submitMove(GuiButton b) {
        char label = b.getLabel();
        Player p = engine.getCurrentPlayer();
        engine.submitMove(label, p);

        GuiToken newToken = new GuiToken(playerIcons.get(p), b.getBounds(), label);
        timer.register(newToken);

        List<GuiToken> neighbors = new ArrayList<>();
        neighbors.add(newToken);
        if (b.isTopButton()) {
            //we're moving down
            for (char row = 'A'; row <= 'E'; row++) {
                GuiToken tokenInColumn = getTokenAt(row, label);
                if (tokenInColumn != null) {
                    neighbors.add(tokenInColumn);
                } else {
                    break;
                }
            }
        }
        else {
            //we're moving right
            for (char col = '1'; col <= '5'; col++) {
                GuiToken tokenInRow = getTokenAt(label, col);
                if (tokenInRow != null) {
                    neighbors.add(tokenInRow);
                } else {
                    break;
                }
            }
        }
        tokens.add(newToken);


        //move them!
        if (b.isTopButton()) {
            for (GuiToken t : neighbors) {
                t.moveDown();
            }
        } else {
            for (GuiToken t : neighbors) {
                t.moveRight();
            }
        }


    }

    private GuiToken getTokenAt(char row, char col) {
        for (GuiToken gt : tokens) {
            if (gt.matches(row, col)) {
                return gt;
            }
        }
        return null;
    }

    private void unselectAllButtons() {
        for (GuiButton b : buttons) {
            b.release();
        }
    }
    private boolean checkMoving(){
        int count = 0;
        for(GuiToken g : tokens){
            if (!g.isMoving()){
                count++;
            }
        }
        if(count == tokens.size()){
            return true;
        } else {
            return false;
        }
    }

    @Override
    public void onTick() {
        invalidate();
        if(checkMoving()) {
            if (engine.checkForWin() != Player.BLANK) {
                if (engine.checkForWin() != Player.TIE) {
                    Player p = engine.getCurrentPlayer();
                    if (engine.getCurrentPlayer() == Player.O) {
                        player0++;
                    } else {
                        playerX++;
                    }
                    AlertDialog.Builder b = new AlertDialog.Builder(getContext());
                    AlertDialog dialog = b.setTitle(R.string.winner)
                            .setMessage(getResources().getString(R.string.winner) + getResources().getString(R.string.play_again))
                            .setPositiveButton(R.string.yes, new DialogInterface.OnClickListener() {
                                @Override
                                public void onClick(DialogInterface dialogInterface, int i) {
                                    //reset the board
                                    init(); //runs all the code
                                    engine.clear();//clear this first!
                                    timer.unPause();
                                    //unsubscribing the tokens and clearing board
                                    for (GuiToken t : tokens) {
                                        timer.deregister(t);
                                        //Log.d("help", "I've been removed!");
                                    }
                                    tokens = new ArrayList<GuiToken>();
                                    invisibleTokens = new ArrayList<GuiToken>();
                                    Difficulty bo = Difficulty.BUMBLING_OAF;
                                    invalidate();
                                }
                            })
                            .setNegativeButton(R.string.no, new DialogInterface.OnClickListener() {
                                @Override
                                public void onClick(DialogInterface dialogInterface, int i) {
                                    System.exit(i);
                                }
                            })
                            .create();
                    dialog.show();
                    timer.setPaused();
                } else {
                    player0++;
                    playerX++;
                    AlertDialog.Builder b = new AlertDialog.Builder(getContext());
                    AlertDialog dialog = b.setTitle(R.string.game_over)
                            .setMessage(R.string.tie)
                            .setPositiveButton(R.string.yes, new DialogInterface.OnClickListener() {
                                @Override
                                public void onClick(DialogInterface dialogInterface, int i) {
                                    init(); //runs all the code
                                    engine.clear();//clear this first!
                                    timer.unPause();
                                    for (GuiToken t : tokens) {
                                        timer.deregister(t);
                                        //Log.d("help", "I've been removed!");
                                    }
                                    tokens = new ArrayList<GuiToken>();
                                    invisibleTokens = new ArrayList<GuiToken>();
                                    invalidate();
                                }
                            })//put logic here!!!
                            .setNegativeButton(R.string.no, new DialogInterface.OnClickListener() {
                                @Override
                                public void onClick(DialogInterface dialogInterface, int i) {
                                    System.exit(i);
                                }
                            })
                            .create();
                    dialog.show();
                    timer.setPaused();
                }
            } else {

                if (gm == ONE_PLAYER && engine.getCurrentPlayer() == Player.O && !GuiToken.moving()) {
                    if(bo == Difficulty.BUMBLING_OAF){
                        class randButton extends AsyncTask<Void, Void,GuiButton> {
                            @Override
                            protected GuiButton doInBackground(Void... args){
                                int r = (int)(Math.random()*10);
                                return buttons[r];
                            }
                            @Override
                            protected void onPostExecute(GuiButton b) {
                                submitMove(b);
                            }
                        } new randButton().execute();
                    }
                    if (bo == Difficulty.COPY_CAT){
                        class randButton extends AsyncTask<Void, Void,GuiButton>{
                            @Override
                            protected GuiButton doInBackground(Void... args){
                                return copyCat;
                            }
                            @Override
                            protected void onPostExecute(GuiButton b) {
                                submitMove(b);
                            }
                        } new randButton().execute();
                    }


                }
            }
        }
        invalidate();
    }

    public void handleOnPause() {
        //if (music != null) {
        music.pause();
        //}
    }

    public void handleOnResume() {
        music.start();
    }

    @Override
    public void handleTouch(MotionEvent m){
        //ignore touch events if the View is not fully initialized
        if (grid == null || firstRun) return;

        //ignore touch events if there are any currently-moving tokens
        if (GuiToken.moving()) return;

        float x = m.getX();
        float y = m.getY();
        if (m.getAction() == MotionEvent.ACTION_DOWN) {
            //Main.say("finger down!");
            currentButton = null;
            for (GuiButton b : buttons) {
                if (b.contains(x, y)) {
                    currentButton = b;
                    b.press();
                    copyCat = currentButton;

                    break;
                }
            }

            //show a helpful hint if the user taps inside the grid
            if (currentButton == null) {
                if (grid.contains(x, y)) {
                    Toast t = Toast.makeText(getContext(),
                            R.string.to_play,
                            Toast.LENGTH_LONG);
                    t.setGravity(Gravity.TOP, 0, 0);
                    t.show();
                }
            }

        } else if (m.getAction() == MotionEvent.ACTION_MOVE) {
            boolean touchingAButton = false;
            for (GuiButton b : buttons) {
                if (b.contains(x, y)) {
                    touchingAButton = true;
                    if (currentButton != null && b != currentButton) {
                        currentButton.release();
                        currentButton = null;
                        break;
                    }
                }
            }
            if (!touchingAButton) {
                unselectAllButtons();
            }
        } else if (m.getAction() == MotionEvent.ACTION_UP) {
            for (GuiButton b : buttons) {
                if (b.contains(x, y)) {
                    if (b == currentButton) {
                        currentButton.release();
                        submitMove(currentButton);
                    }
                }
            }
            currentButton = null;
        }
        invalidate();
        return;
    }

}
