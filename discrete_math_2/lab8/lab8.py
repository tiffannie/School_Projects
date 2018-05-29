'''
In this assignment, you will write a program that reads in a truth table and outputs an equivalent boolean expression in disjunctive normal form (DNF)
Read in the data.
The first line lists the variable names that will be used in the boolean expression. You can throw away the last token on this line, as it is not a variable name.
You can ignore the second line of the input; it is decorative.
The next lines, all the way to the end, list the input values, followed by the output value. (Think: How can you predict how many lines there will be?)
Create a boolean expression by converting each line where the output is 1, to a "product" of the variables. If an input is 1, just print the variable name. If the input is 0, then print the variable followed by an apostrophe (for negation). Put plus signs (+) between each product.
'''

names = raw_input()
names = names.split(" ")

#rid program of OUT
del(names[-1])

#keep "asthetic" dashed line by itself
dashes = raw_input()

#to know how many lines of input
numLines = 2**(len(names))

#retrieve rest of lines and save them to an array
input = []
while True:
	input.append(raw_input().split())
	#input = input.split(" ")
	if len(input) == numLines:
		break
		
#separate true statements from false statements
true_statement = []
for statement in input:
	#if it's one then it is true and is aggregated to an 
	#array 'true_statement'
	if statement[-1] == '1':
		true_statement.append(statement)

#we know it's true and no longer need the last number
for a in true_statement:
	del(a[-1])

#printing complement signs with corresponding letter index
final_statements = []	
#for each line in array of true statements
for line in true_statement:
	#instantiate new array
	copy = []
	#empty string
	res = ""
	#create copy of variable names (a,b,c) etc
	for b in names:
		copy.append(b)
	#for each index in length of a true statement
	for i in range(len(line)):
		#if the number is 0 then 
		if line[i] == '0':
			#add an appostraphe to the element at the current
			#certain index. didn't know we could do this #cool 
			copy[i] += "'"
	#iterate through variables in your copied array and add them to
	#an empty string with a space so they don't fight
	for i in copy:
		res += i + " "
	#add your new string to an array 
	final_statements.append(res)

#initialize an empty string
result=""
#for each group of variables 
for i in range(len(final_statements)):
	#add the group to the string
	result += final_statements[i]
	#make sure not to print a plus after the last group
	if i != len(final_statements)-1:
		result += "+ "
print result
