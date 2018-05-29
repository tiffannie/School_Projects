import numpy as np
import math

'''
Read in data.
The data will be in the following format:
	S(1) nnn
	S(2) nnn
	c1 nnn
	c2 nnn
These values represent, of course, the defining parts of a second-order linear recurrence relation.
Your program should compute, and print out, the values for p, q, r1, and r2.
Then your program should print out the formula for S(n), and then print out the results for n=1, 2, 3, 4, 5, 6, 7, 8, 9, 10.
Note: Your program does NOT need to recognize the special case where r1 == r2.
'''

garb = []
for t in range(4):
	t = raw_input()
	garb.append(t)

		#create array list to store number values
num = []
for a in garb:
	num.append(a.rstrip("\n"))
	
#print(num)
#print("S(1) = " + num[0]) #3
#print("S(2) = " + num[1]) #1
#print("c1 = " + num[2]) #2
#print("c2 = " + num[3]) #3

#ref from array the numbers
s1 = num[0] #3
s2 = num[1] #1
c1 = num[2] #2
c2 = num[3] #3

#preparation for quadratic formula
a = float(1)
b = 0 - float(c1)
c = 0 - float(c2)

#the 4ac under the root in the quadratic formula
ac4 = ((b**(2)) - 4*a*c)

#-b + or - 4xAxC and to the power of 1/2 gets the square root then divide by 2xA. sing the song
r1 = ((0 - b) + ac4**(1.0/2))/(2*a) 
r2 = ((0 - b) - ac4**(1.0/2))/(2*a)

q = (float(s2)-float(s1)*r1)/(r2-r1)
p = float(s1) - q

print("r1 = " + str(r1))
print("r2 = " + str(r2))
print("p = " + str(p))
print("q = " + str(q))
print("S(n) = (" + str(p) + ")(" + str(r1) + ")^(n-1) + (" + str(q) + ")(" + str(r2) + ")^(n-1)")

for x in range(1,11):
	fib = ((float(p)*(float(r1)**(float(x)-1))) + (float(q)*(float(r2)**(float(x)-1))))
	print("S(" + str(x) + ") = " + str(fib))