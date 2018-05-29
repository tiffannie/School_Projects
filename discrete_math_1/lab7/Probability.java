package prob;

import java.util.*; 
import javax.swing.*;
import java.awt.*;
import java.io.*;

public class Probability {
/*
     Write a program that reads in a text file. The name will be supplied as a command-line parameter. Each line gives you a list of 4 cards in your current hand.
After reading in the file, your program will print out the probability of each type of winning hand, where a winning hand is given as one of the following:
Note that the Ace card can be at the start of a straight or an end of a straight, but not in the middle somewhere.
Finally, you must plot the probability distribution for each hand onto a single plot.
*/

    public static String[] lola; 
	public static String[] pamela;
	static boolean straight;
	static int doublescount =0;
	//read in file
	public static void main(String args[]) {
		//read in file
		
		String filename = (args[0]);
		//String filename = ("input.txt");
		File f = new File(filename);   
		
		//Generate all possible cards
		String[] num = {"A", "2", "3", "4", "5", "6","7","8","9","10","J","Q","K"};
		String[] suit = {"S","D","C","H"};
		ArrayList<String> deck = new ArrayList<String>();
		ArrayList<String> hand = new ArrayList<String>();
		ArrayList<String> suits_of_hand = new ArrayList<String>();
		ArrayList<String> value = new ArrayList<String>();//array of 2,3,4...J,Q,K
		int count =0;
		
		double probability;
		boolean isstraight;
		boolean isflush;
		boolean royalC = true;
		
		//make deck. 
		for (String i:num) {
			for(String j:suit) {
				deck.add(i+j);
			}
		}
		
		
		try {
			Scanner s = new Scanner(f);
			while(s.hasNextLine()) {
			suits_of_hand.clear();//empties array list at the start of this loop. always fresh when it loops through again
			value.clear();//ditto
			
			ArrayList<String> deck1 = new ArrayList<String>();
			
			//you can alter deck1 without affecting original deck. deck is template and deck1 is a copy
			deck1 =deck;
			
			String p = s.nextLine(); 
			//System.out.println(p);
			
			//split by comma and space
			lola = p.split(", ");   
								
			//MAKE HAND
			//further split each
			for (String letter : lola) {
				String[] paola = letter.split("");
				
				if (paola.length > 2) {//exception for 10"s
					hand.add(paola[0]+paola[1]+paola[2]);//add suit to suits array list. add the num 1 + num 0 + suit
					suits_of_hand.add(paola[2]);
					value.add(paola[0]+paola[1]);//contains numbers
				} else {//adds card to hand
					hand.add(paola[0]+paola[1]);
					suits_of_hand.add(paola[1]); //contains suits now
					value.add(paola[0]);
				}
				count++;
			}
			
			ArrayList<String> hand1 = new ArrayList<String>();
			
			//take away hand from deck
			hand1 = hand; //hand1 is copy of hand now
				for(int i = 0; i<hand1.size(); i++){//goes through and checks the size and checks up to size. "dog" that obeys no matter what. for each is complaining "guy"
					for(int j = 0; j<deck1.size(); j++){
						if(hand1.get(i).equals(deck1.get(j))){
								deck1.remove(j); //removes cards from hand from deck. now deck1 is the deck with the missing cards(our hand)
						}
					}
				}
					
				ArrayList<String> current_suits = new ArrayList<String>();
				
				//to check if you have repeating suits in your hand
				for(String b : suits_of_hand){
					if(!current_suits.contains(b)){//if not already in hand, add it
						current_suits.add(b);
					}
				}								
			
				
				//replace royalty with a numeric value to sort more easily
				for(int x = 0; x < value.size(); x++){
					
					if(value.get(x).equals("J")){
						value.set(x, "11");
					}
					if(value.get(x).equals("Q")){
						value.set(x, "12");
					}
					if(value.get(x).equals("K")){
						value.set(x, "13");
					}
					if(value.get(x).equals("A")){
						value.set(x, "14");
					}
				}
				
				//array of numerical values of current hand
				ArrayList<Integer> value_AND_value_of_suit = new ArrayList<Integer>();
				
				//add elements 
				for(int h = 0; h<value.size(); h++){
					int numb = Integer.parseInt(value.get(h));
					value_AND_value_of_suit.add(numb);
				}
				
				//orders it numerically
				Collections.sort(value_AND_value_of_suit);
				
				System.out.println("HEY LOOK AT ME NEW HAND!");
				//CHECK FOR ROYAL FLUSH
				if(current_suits.size() == 1){
					ArrayList<Integer> royalty = new ArrayList<Integer>();
					ArrayList<Integer> duplicate = new ArrayList<Integer>();
					
					royalty.addAll(Arrays.asList(10,11,12,13,14));
					
					if(royalty.contains(value_AND_value_of_suit.get(0))) {//if first card in hand is a royal card
						duplicate.add(value_AND_value_of_suit.get(0)); //add it to duplicate array list
						if(royalty.contains(value_AND_value_of_suit.get(1)) && !duplicate.contains(value_AND_value_of_suit.get(1))) {//AND ALSO it doesn't contain it already
							duplicate.add(value_AND_value_of_suit.get(1));
							if(royalty.contains(value_AND_value_of_suit.get(2)) && !duplicate.contains(value_AND_value_of_suit.get(2))) {
								duplicate.add(value_AND_value_of_suit.get(2));
								if(royalty.contains(value_AND_value_of_suit.get(3)) && !duplicate.contains(value_AND_value_of_suit.get(3))){
									royalC= true;
									probability = 1.0000 / 48.0000;
									System.out.printf("Chance of Royal Flush is %.6f", probability);
									System.out.println("\n");
									System.out.printf("Chance of Straight Flush is %.6f", probability);
									System.out.println("\n");
									System.out.println("Chance of Four of a Kind is 0.0\n");
									System.out.println("Chance of Full House is 0.0\n");
									probability = 7.0000 / 48.0000;
									System.out.printf("Chance of Flush is %.6f", probability);
									System.out.println("\n");
									probability = 6.0000 / 48.0000;
									System.out.printf("Chance of Straight is %.6f", probability);
									System.out.println("\n");
									System.out.println("Chance of Three of a Kind is 0.0\n");
									System.out.println("Chance of Two Pair is 0.0\n");
									probability = 12.0000 / 48.0000;
									System.out.printf("Chance of Pair is %.6f", probability);
									System.out.println("\n");
									probability = 21.0000 / 48.0000; //adding all the other probabilities then subtracting from deck size
									System.out.printf("Chance of High Card is %.6f", probability);
									System.out.println("\n\n");
								} else {
									royalC = false;
								}
							} else {
								royalC = false;
							}
						} else {
							royalC = false;
						}
					} else {
						royalC = false;
					}
				} else {
					royalC = false;
				}
				//check for straight
				ArrayList<Integer> smallest = new ArrayList<Integer>();
				ArrayList<Integer> duplicate1 = new ArrayList<Integer>();
				
					if(royalC == false) {
						System.out.println("Chance of Royal Flush is: 0.0)\n");
						if(current_suits.size() == 1) { //checks for straight flush or just a straight
							isflush = true;
						} else {
							isflush = false;
						}
						if(value_AND_value_of_suit.get(1) > value_AND_value_of_suit.get(0)) {
							int small = value_AND_value_of_suit.get(0);
							smallest.addAll(Arrays.asList(small,small + 1,small +2,small +3,small +4));
							if(smallest.contains(value_AND_value_of_suit.get(0)) && !duplicate1.contains(value_AND_value_of_suit.get(1))) {
								duplicate1.add(value_AND_value_of_suit.get(0));
								if(smallest.contains(value_AND_value_of_suit.get(1)) && !duplicate1.contains(value_AND_value_of_suit.get(1))) {
									duplicate1.add(value_AND_value_of_suit.get(1));
									if(smallest.contains(value_AND_value_of_suit.get(2)) && !duplicate1.contains(value_AND_value_of_suit.get(1))) {
										duplicate1.add(value_AND_value_of_suit.get(2));
										if(smallest.contains(value_AND_value_of_suit.get(3)) && !duplicate1.contains(value_AND_value_of_suit.get(1))) {
											duplicate1.add(value_AND_value_of_suit.get(3));
											straight = true;
											if(isflush = true){
												probability = 1.0000 / 48.0000;
												System.out.printf("Chance of Straight Flush %.6f", probability);
												System.out.println("\n");
												System.out.println("Chance of Four of a kind: 0.0\n");
												System.out.println("Chance of Full House: 0.0\n");
												System.out.println("Chance of Flush: 0.0\n");
												probability = 4.0000 / 48.0000;
												System.out.printf("Chance of Straight: %.6f", probability);
												System.out.println("\n");
												System.out.println("Chance of Three of a kind: 0.0\n");
												System.out.println("Chance of Two Pair: 0.0\n");
												probability = 39.0000 / 48.0000;
												System.out.printf("Chance of High Card: %.6f", probability);
												System.out.println("\n");
											} else {
												System.out.printf("Chance of Straight Flush is: 0.0\n");
												System.out.println("Chance of Four of a kind: 0.0\n");
												System.out.println("Chance of Full House: 0.0\n");
												System.out.println("Chance of Flush: 0.0\n");
												probability = 4.0000 / 48.0000;
												System.out.printf("Chance of Straight is %.6f", probability);
												System.out.println("Chance of Three of a kind: 0.0\n");
												System.out.println("Chance of Two Pair: 0.0\n");
												probability = 12.0000 / 48.0000;
												System.out.printf("Chance of Pair is %.6f", probability);
												System.out.println("\n");
												probability = 21.0000 / 48.0000; //adding all the other probabilities then subtracting from deck size
												System.out.printf("Chance of High Card is %.6f", probability);
												System.out.println("\n\n");
											}
										} else {
											straight = false;
										}
									} else {
										straight = false;
									}
								} else {
									straight = false;
								}
							} else {
								straight = false;
							}
						} else {
							straight = false;
						}
					}
				//to check for pairs and triples
					if (straight == true){
					System.out.println("straight");
					}
				if (straight == false && royalC == false){
					doublescount = 0; 
					for(int q : value_AND_value_of_suit) {
						for(int r : value_AND_value_of_suit) {
							if(q == r){
								doublescount++;
							}
						}
					}
					System.out.println("doublescount is " + doublescount);
					//CHECK FOR PAIR
					if(doublescount == 6 ){
						
						System.out.println("Chance of Straight Flush: 0.0\n");
						System.out.println("Chance of Four of a Kind: 0.0\n");
						System.out.println("Chance of Full House: 0.0\n");
						System.out.println("Chance of Flush: 0.0\n");
						System.out.println("Chance of Straight: 0.0\n");
						probability = 2.0000 / 48.0000;
						System.out.printf("Chance of Three of a Kind is %.6f", probability);
						System.out.println("\n");
						System.out.printf("Chance of Two Pair is %.6f", probability);
						System.out.println("\n");
						probability = 6.0000 / 48.0000;;
						System.out.printf("Chance of Pair is %.6f", probability);
						System.out.println("\n");
						probability = 40.0000 / 48.0000;
						System.out.printf("Chance of High Card is %.6f", probability);
						System.out.println("\n");
					} 
					else if (doublescount == 8){
						
						System.out.println("Chance of Straight Flush: 0.0\n");
						System.out.println("Chance of Four of a Kind: 0.0\n");
						probability = 4.0000 / 48.0000;
						System.out.printf("Chance of Full House is %.6f", probability);
						System.out.println("\n");
						System.out.println("Chance of Flush: 0.0\n");
						System.out.println("Chance of Straight: 0.0\n");						
						probability = 44.0000 / 48.0000;
						System.out.printf("Chance of Two Pair probability is %.6f", probability);
						System.out.println("\n");
						probability = 6.0000 / 48.0000;;
						System.out.printf("Chance of Pair is %.6f", probability);
						System.out.println("\n");
						probability = 40.0000 / 48.0000;
						System.out.printf("Chance of High Card is %.6f", probability);
						System.out.println("\n");
						
					}
					else if(doublescount == 10){
						
						System.out.println("Chance of Straight Flush: 0.0\n");
						probability = 1.0000 / 48.0000;
						System.out.printf("Chance of Four of a Kind is %.6f", probability);
						System.out.println("\n");
						probability = 3.0000 / 48.0000;
						System.out.printf("Chance of Full House %.6f", probability);
						System.out.println("\n");
						System.out.println("Chance of Flush: 0.0\n");
						System.out.println("Chance of Straight: 0.0\n");
						probability = 44.0000 / 48.0000;
						System.out.printf("Chance of Three of a Kind is %.6f", probability);
						System.out.println("\n");
						probability = 44.0000 / 48.0000;
						System.out.printf("Chance of Two Pair probability is %.6f", probability);
						System.out.println("\n");
						probability = 6.0000 / 48.0000;;
						System.out.printf("Chance of Pair is %.6f", probability);
						System.out.println("\n");
						probability = 40.0000 / 48.0000;
						System.out.printf("Chance of High Card is %.6f", probability);
						System.out.println("\n");
						
					}
					else if(doublescount == 16){
						System.out.println("Chance of Royal Flush: 0.0\n");
						System.out.println("Chance of Straight Flush: 0.0\n");
						probability = 48.0000 / 48.0000;
						System.out.printf("Chance of Four of a Kind is %.6f", probability);
						System.out.println("Chance of Full House: 0.0\n");
						System.out.println("Chance of Flush: 0.0\n");
						System.out.println("Chance of Straight: 0.0\n");
						probability = 44.0000 / 48.0000;
						System.out.printf("Chance of Three of a Kind is %.6f", probability);
						System.out.println("\n");
						probability = 44.0000 / 48.0000;
						System.out.printf("Chance of Two Pair probability is %.6f", probability);
						System.out.println("\n");
						probability = 6.0000 / 48.0000;;
						System.out.printf("Chance of Pair is %.6f", probability);
						System.out.println("\n");
						probability = 40.0000 / 48.0000;
						System.out.printf("Chance of High Card is %.6f", probability);
						System.out.println("\n");
						
					} else {
						
							System.out.printf("Chance of Flush is heeby sheeby");
							System.out.println("\n");
						
					}
					
				}
				
			}
		} catch (FileNotFoundException e) {
			System.out.println("Could not find " + filename);
		}
	}
}
   