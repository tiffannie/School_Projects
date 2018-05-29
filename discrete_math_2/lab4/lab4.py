'''
Donald Duck wants to build a playground for his nephews, but first he needs money to buy equipment. So Donald takes a part-time job at Function-O-Matic, Inc. The job consists of standing behind a pickup truck, and catching relations which are thrown at him. For each relation, he must correctly classify it as one of the following:

regular function
onto function
one-to-one function
bijection
not a function
Help Donald out by writing a program to do the classification for him. His nephews are counting on you!
Read in the input and parse it. The input consists of exactly three lines. The first line lists the members of the domain, separated by whitespace. These might be integers, floating-point numbers, or strings. The second line lists the members of the codomain, separated by whitespace. Again, these could be anything. The third line lists the ordered pairs of the relation. For convenience in parsing, these also are just separated by whitespace. Every two adjacent elements in this list forms an ordered pair.
Print out to the console the domain, codomain, and relation, formatted as sets. Also print out whether the given relation represents a function. If it is a function, print out whether it is onto, one-to-one, or a bijection.
'''
import math
import sys

garb = []
#domain
num = []
for t in range(3):
	t = raw_input()
	garb.append(t)

		#split by lines
garb1 = []
for a in garb:
	garb1.append(a.rstrip("\n"))

#print(garb1) #split by spaces
for b in garb1:
	s = b.split(" ")
	num.append(s)

#put different values into arrays
dom = num[0]
codom = num[1]
rel = num[2]
x = rel[0::2] #retrieve every other number
y = rel[1::2]

#domain values 
sys.stdout.write("DOMAIN: { ")

#print them next to each other
domain = ",".join(dom)
sys.stdout.write(domain)
print(" }")

#codomain values 
sys.stdout.write("CODOMAIN: { ")
codomain = ",".join(codom)
sys.stdout.write(codomain)
print(" }")

#relation
len_of_rel = (len(rel)-1)

sys.stdout.write("RELATION: { ")
count = 0
while (count < (len_of_rel)):
	sys.stdout.write("(" + str(rel[count]) + "," + str(rel[count+1]) + ")" )
	if count+1 != len_of_rel:
		sys.stdout.write(",")#no end comma
	count = count +2
print(" }")

#put elements in order for easier comparisons
dom = sorted(dom)
codom = sorted(codom)
y = sorted(y)
x = sorted(x)

isFunction = True
#check if it is a function
dup = []
for p in range(len(x)):
	if x[p] in dup: #if it is a duplicate, then false
		isFunction = False
	else:
		dup.append(x[p])

#if number is codom appears once in y values then onto
isOnto = True
for k in range(len(codom)):
	for l in range(len(y)):
		if codom[k] not in y:
			isOnto = False

#one-one
isOnetoOne = True
dupy = [] #iterate through y values and if duplicate appears, then not one-one
for m in range(len(y)):
	if y[m] in dupy:
		isOnetoOne = False
	else:
		dupy.append(y[m])

if isFunction == True:
	print ("This is a function")
else:
	print("This is NOT a function")

if isOnto == True:
	print ("This is onto")
else:
	print("This is NOT onto")
	
if isOnetoOne == True:
	print ("This is one-to-one")
else:
	print("This is NOT one-to-one")
	
if isOnetoOne == True and isOnto ==True:
	print ("This is a bijection")
else:
	print("This is NOT a bijection")

