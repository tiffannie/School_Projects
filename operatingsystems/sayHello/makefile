all: myprogram

myprogram: main.o myclass.o
	g++ main.o myclass.o -o myprogram

main.o: main.cpp
	g++ -c -Wall main.cpp

myclass.o: myclass.cpp
	g++ -c -Wall myclass.cpp

clean:
	rm -f *.o
