package edu.byuh.cis.cs203.tokens2.logic;

public class GameBoard {

	private Player[][] grid;
	private final int DIM = 5;
	private Player whoseTurnIsIt;
	//private char[] rowLetters = {'A','B','C','D','E' };
	private static GameBoard singleton_object;
	
	private GameBoard() {
		grid = new Player[DIM][DIM];
		for (int i=0; i<DIM; ++i) {
			for (int j=0; j<DIM; ++j) {
				grid[i][j] = Player.BLANK;
			}
		}
		whoseTurnIsIt = Player.X;
	}

	/**
	 * public static factory method in your GameBoard class,
	 * that checks if your singleton object is null. If it is, instantiate it.
	 * Below the if-statement, simply return the singleton object.
	 * @return the singleton object
     */
	public static GameBoard Singleton(){
		if (singleton_object == null) {
			singleton_object = new GameBoard();
		}
		return singleton_object;
	}

	public void submitMove(char move, Player p) {
		if (move >= '1' && move <= '5') {
			//vertical move, move stuff down
			int col = Integer.parseInt(""+move)-1;
			Player newVal = p;
			for (int i=0; i<DIM; ++i) {
				if (grid[i][col] == Player.BLANK) {
					grid[i][col] = newVal;
					break;
				} else {
					Player tmp = grid[i][col];
					grid[i][col] = newVal;
					newVal = tmp;
				}
			}
			
		} else { //A-E
			//horizontal move, move stuff right
			int row = (int)(move - 'A');
			Player newVal = p;
			for (int i=0; i<DIM; ++i) {
				if (grid[row][i] == Player.BLANK) {
					grid[row][i] = newVal;
					break;
				} else {
					Player tmp = grid[row][i];
					grid[row][i] = newVal;
					newVal = tmp;
				}
			}	
		}
		if (whoseTurnIsIt == Player.X) {
			whoseTurnIsIt = Player.O;
		} else {
			whoseTurnIsIt = Player.X;
		}
	}
	
	public Player checkForWin() {
		Player winner = Player.BLANK;
		Player tmpWinner = Player.BLANK;
		
		//check all rows
		for (int i=0; i<DIM; ++i) {
			if (grid[i][0] != Player.BLANK) {
				tmpWinner = grid[i][0];
				for (int j=0; j<DIM; ++j) {
					if (grid[i][j] != tmpWinner) {
						tmpWinner = Player.BLANK;
						break;
					}
				}
				if (tmpWinner != Player.BLANK) {
					if (winner == Player.BLANK) {
						winner = tmpWinner;
					} else {
						return Player.TIE;
					}
				}
			}
		}
		
		//check all columns
		tmpWinner = Player.BLANK;
		for (int i=0; i<DIM; ++i) {
			if (grid[0][i] != Player.BLANK) {
				tmpWinner = grid[0][i];
				for (int j=0; j<DIM; ++j) {
					if (grid[j][i] != tmpWinner) {
						tmpWinner = Player.BLANK;
						break;
					}
				}
				if (tmpWinner != Player.BLANK) {
					if (winner == Player.BLANK) {
						winner = tmpWinner;
					} else {
						return Player.TIE;
					}
				}
			}
		}
		
		//at this point, either there's a tie, or there's not.
		//You can't have a tie with diagonals.
		if (winner != Player.BLANK) {
			return winner;
		}
		
		//check top-left -> bottom-right diagonal
		if (grid[0][0] != Player.BLANK) {
			winner = grid[0][0];
			for (int i=0; i<DIM; ++i) {
				if (grid[i][i] != winner) {
					winner = Player.BLANK;
					break;
				}
			}
			if (winner != Player.BLANK) {
				return winner; //5 in a diagonal!
			}
		}

		//check bottom-left -> top-right diagonal
		if (grid[DIM-1][0] != Player.BLANK) {
			winner = grid[DIM-1][0];
			for (int i=0; i<DIM; ++i) {
				if (grid[DIM-1-i][i] != winner) {
					winner = Player.BLANK;
					break;
				}
			}
			if (winner != Player.BLANK) {
				return winner; //5 in a diagonal!
			}
		}

		return winner;
	}
	
	public void consoleDraw() {
		System.out.print("  ");
		for (int i=0; i<DIM; ++i) {
			System.out.print(i+1);
		}
		System.out.println();
		System.out.print(" /");
		for (int i=0; i<DIM; ++i) {
			System.out.print("-");
		}
		System.out.println("\\");
		for (int i=0; i<DIM; ++i) {
			System.out.print(((char)('A'+i)) + "|");
			for (int j=0; j<DIM; ++j) {
				if (grid[i][j] == Player.BLANK) {
					System.out.print(" ");
				} else {
					System.out.print(grid[i][j]);
				}
			}
			System.out.println("|");
		}
		System.out.print(" \\");
		for (int i=0; i<DIM; ++i) {
			System.out.print("-");
		}
		System.out.println("/");
		
	}

	public Player getCurrentPlayer() {
		return whoseTurnIsIt;
	}

	public void clear() {
		for (int i=0; i<DIM; ++i) {
			for (int j=0; j<DIM; ++j) {
				grid[i][j] = Player.BLANK;
			}
		}
	}
}
