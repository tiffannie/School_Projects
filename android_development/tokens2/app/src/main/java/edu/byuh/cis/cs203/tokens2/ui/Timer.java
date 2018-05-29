package edu.byuh.cis.cs203.tokens2.ui;

import java.util.ArrayList;
import java.util.List;

import android.os.Handler;
import android.os.Message;

import edu.byuh.cis.cs203.tokens2.logic.TickListener;

public class Timer extends Handler {
	
	private List<TickListener> observers;
	private boolean paused;
	private int speed;
	public Timer(int speed) {

		this.speed = speed;

		observers = new ArrayList<>();
		handleMessage(obtainMessage());
	}
	
	public void register(TickListener s) {
		observers.add(s);
	}
	
	public void deregister(TickListener s) {
		observers.remove(s);
	}

	public void setPaused() {
		paused = true;
	}

	public void unPause() {
		paused = false;
		handleMessage(obtainMessage());
	}

	@Override
	public void handleMessage(Message m) {
		for (TickListener s : observers) {
			s.onTick();
		}
		if(!paused){
			sendMessageDelayed(obtainMessage(), speed);

		}
	}
}

