/*
#include <iostream>
#include <string>
#include "shello.h"

using namespace std;

					//methods

void Dithpenther::inventory(){				//stock the vending machine
	cout << "Stocking Vending Machine...." << endl;
	string snacks[] = {"TrailMix", "Granola","PopCorn","Peanuts", "Grapes"};
	for (int i = 0; i <5; i++) {
		Itim new_item;				//instantiate new Item object
		new_item.set_name(snacks[i]);
		new_item.set_quantity(rand() % 10);	//set random quantities to products
		snack_list[i] = new_item;
	}
	
}
void Dithpenther::DisplayItem(){			//Print the items in our snack list. 
	cout << "Items in Vending Machine" << endl;
	for(int i =0; i<5; i++){
		cout << snack_list[i].get_name() << ": " << snack_list[i].get_quantity() << endl;
	}
}

void Dithpenther::AskForInput(string snack_choice){	//prompt for user input and print user's choice
	cout << "Your snack choice is " << snack_choice << endl;
}

void Dithpenther::Dispense(string snack_purchased){	//'give' user their snack by subtracting quantity of item in the snack list
	cout << "Here you go!" << endl;
	for (int i = 0; i <5; i++) {
		if(snack_purchased == snack_list[i].get_name()) {
			snack_list[i].SubtractItem();
		}
	}
}*/
