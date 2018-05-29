#include <iostream>

#include "itim.h"
#include "dithpenther.h"

using namespace std;

int main() {
	//declare inuput variable
	string input;

	cout << "Tiffannie's Vending Machine" << endl;

// . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . .

	Dithpenther myDispenser;		//instantiate new Dispenser object
						//method calls follow

	myDispenser.inventory();		//generate items and quantities of machine
	myDispenser.DisplayItem();		//print items items and quantities of machine
	
	cout << "What would you like?" << endl;	
	//cin >> input;	

// . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . .
	do {
		cout << "Please, enter the name of your choice:" << endl;	
		cin >> input;
		myDispenser.AskForInput(input);		//print choice of user
		myDispenser.Dispense(input);
		myDispenser.DisplayItem();
		cout << "Would you like another snack? Please, enter Yes or No:" << endl;
		cin >> input;
	}
	while(input != "No");

	
	return 0;
};
