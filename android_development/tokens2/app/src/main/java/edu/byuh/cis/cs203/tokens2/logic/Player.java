package edu.byuh.cis.cs203.tokens2.logic;

public enum Player {
	X,
	O,
	BLANK,
	TIE;

	public String getText(){
		return this.toString();
	}
}

