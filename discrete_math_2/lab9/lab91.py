'''
In this assignment you will write a program that reads in a transition sequence and a list of states, along with two transition states for each state and outputs the final state that will be reached by the sequence.

Learning Objectives
Learn how to implement a basic state transition graph in preparation for Computer Theory (CS 320)
Read in the data.
The first line lists the binary string for processing through the state transition graph.
The next lines, all the way the end, list the states in order along with their corresponding outputs.
Determine the final state that will be reached by the binary string by building the state transition graph using object-oriented programming


'''

class Node:
	def __init__(self, value, nodes):
		self.val = value
		self.connections = nodes
		
#instruction numbers in list of characters form #mind blown
instruction = raw_input()

#retrieve rest of lines and save them to an array
node=[]
while True:
	input = raw_input()
	if input == "***":
		break
	dict = {}
	input = input.split(", ")
	values = input.pop(0)
	
	for x in input:
		x = x.split(":")
		#add neighboring connections to dictionary. adding
		#second value and value to right of colon
		dict.update({x[0].strip(" "):x[1].strip(" ")})
	#make the current node a node object and svae
	node.append(Node(values, dict))


start = "s0"

for i in instruction:
	start = node[0].connections[i]
print "The final state is \"{}\"".format(start)
#for i in inputs:
	
#print str(node) 	
#print inputs
"""

for n in node:
	#print n.connections
	for key, value in n.connections.iteritems():
		for i in node:
			if i.val == value:
				value = i
				

start = node[0]
for i in instruction:
	for key, value in start.connections.iteritems():
		if key == i:
			start = start.connections[i]
"""