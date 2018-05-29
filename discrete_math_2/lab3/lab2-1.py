'''
Read in 3 lines of input.
The input will be in the following format:
	S(1) nnn^nnnnn
	c nnn^nnnnnn
	g(n) nnn^nnnnnnnn

These values represent, of course, the defining parts of a first-order linear recurrence relation. NOTE: for this assignment, we will only consider the simplified case in which g(n) is a constant!
Then your program should print out the formula for S(n), and then print out the results for n=1, 2, 3, 4, 5, 6, 7, 8, 9, 10.
'''

import math

garb = []
print "is this the ta file... [Y/N]"
#read in file. three numbers
if raw_input() == "N":
	for t in range(3):
		t = raw_input()
		garb.append(t)
			#create array list to store number values
	num = []
	for a in garb:
		num.append(a.rstrip("\n"))

	s1 = float(num[0])
	c = float(num[1]) #lamda
	gn = float(num[2])
else:
	for t in range(3):
		t = raw_input().split("^")
		garb.append(t)
	print (garb)
	num = []
	for a in garb:
		num.append(a)
	print (num)
	
	e = math.e
	exp = float(num[0][1])
	an = float(num[0][0])
	
	#d = num[2]
	s1 = an**exp 
	c = float(num[1][0])
	gn = (float(num[2][0]))**float(e)

#finding "D" the one multiplied by the root
q = (float(gn))/(1-c)

#finding "C" from lamda
p = (s1 - q)/c

print("S(n) = (" + str(p) + ") * (" + str(c) + ")^n " + str(q))

for n in range(1,11):
	dif = p*(c**n) + q
	print("S(" + str(n) + ") = " + str(dif))
