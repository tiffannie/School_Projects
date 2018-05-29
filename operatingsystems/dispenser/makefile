all: myprog

myprog: main.o dithpenther.o itim.o
	g++ main.o dithpenther.o itim.o -o myprog

main.o: main.cpp
	g++ -c -Wall main.cpp

dithpenther.o: dithpenther.cpp
	g++ -c -Wall dithpenther.cpp

itim.o: itim.cpp
	g++ -c -Wall itim.cpp

clean:
	rm -f *.o
