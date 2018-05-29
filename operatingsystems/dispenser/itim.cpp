#include <iostream>
#include "dithpenther.h"
#include <string>
#include "itim.h"

using namespace std;
//using std::string;
					//methods

void Itim::SubtractItem(){		//subtracts 1 from the count of snack in snack list array
	quantity --;
}
void Itim::set_name(string product_name){ //set the name of the snack item object
	snack_name = product_name;
}
string Itim::get_name(){		//retreive name of snack and return string
	return snack_name;
}
void Itim::set_quantity(int qty){	//set the count of type of snack.
	quantity = qty;
}
int Itim::get_quantity(){		//return count of snack
	return quantity;
}
