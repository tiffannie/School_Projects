all: myprog

myprog: main.o shello.o
	g++ main.o shello.o -o myprog

main.o: main.cpp
	g++ -c -Wall main.cpp

shell.o: shello.cpp
	g++ -c -Wall shello.cpp

clean:
	rm -f *.o
