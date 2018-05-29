#ifndef MYCLASS_H
#define MYCLASS_H
#include <string>

using namespace std;

class Itim{
	private:
	string snack_name;
	int quantity;
	
	public:
	void SubtractItem();
	void set_name(string);
	string get_name();
	void set_quantity(int);
	int get_quantity();
};

#endif




