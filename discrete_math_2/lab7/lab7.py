'''
Your program should read in a sequence of data describing a graph.
The format of the input is as follows. The first line lists the names of the nodes in the graph. You can assume that this line will always be a list of strings in alphabetical order. The second line is the name of the node at which you will start the traversal. The remaining lines each define one edge in the graph. The last line will consist of three asterisks ***. The edges are NOT directed. For example, the input:
After printing out the names of the nodes' neighbors, your program should print out the nodes in the order they would be visited in a depth-first traversal. Start with the node that is listed on the 2nd line of the input (So in the example above, the traversal would start with node F.) During the course of the traversal, if a node has multiple neighbors, we will follow the textbook's convention of visiting the nodes in alphabetical order.
Once you have depth first traversal working, implement breadth-first traversal as well
'''

from collections import deque

class Node:

    def __init__(self, data=None):
		self.data = data
		self.neighbors = []
		self.visited = False		
		 #puts in alphabetical order
		#print self.neighbors		

#recursive(without stack)
def DepthFirst(node, order):
	#writes nodes in graph g in depth-first order from node a
	
	node.visited = True
	order.append(node)
	for n in node.neighbors:		
		if not n.visited:
			DepthFirst(n, order)	
	return order
		
def BreadthFirst(node):
	order = []
	#initialize queue to be empty
	queue = deque() #python module when it says dequeue it's popleft()
	node.visited = True
	order.append(node.data)	
	queue.append(node)
	while len(queue) != 0:
		for neighbor in queue.popleft().neighbors: #pop node off and get value of neighbors
			if not neighbor.visited:
				neighbor.visited = True
				order.append(neighbor.data)
				queue.append(neighbor)
	return order
			


def printOutput(node):
	tmp = []
	for n in node.neighbors:
		tmp.append(str(n.data))
	tmp = sorted(tmp)
	outStr = "{}: {}".format(str(node.data), ", ".join(tmp))
	#for i in node.neighbors:
		#outStr = outStr + i #adds all remaining 'children/neighbors'
	print outStr

	
#reading in input
letters = raw_input().split(" ")
beginningNode = raw_input()
edges = []
nodes = []

for letter in letters:
	nodes.append(Node(letter))

while True:
	pairs = raw_input()
	edge = pairs.split(" ")
	if pairs == "***":
		break
		
	for n in nodes:
		if edge[0] == n.data:
			for i in nodes:
				if i.data == edge[1]:	
					n.neighbors.append(i) #gets neighbors or one side
		
		if edge[1] == n.data:
			for i in nodes:
				if i.data == edge[0]:
					n.neighbors.append(i) #gets neighbors from other side
	
	#edges.append(pairs.split(" "))
for n in nodes:
	printOutput(n)
order = []

for n in nodes:
	if str(n.data) == beginningNode:
		beginningNode = n

tmp = []
for i in DepthFirst(beginningNode, order):
	tmp.append(str(i.data))
print "DFS: {}".format(", ".join(tmp))

for n in nodes:
	n.visited = False #setting the node as not visited again. to not mess
	#up breadth first search
	

print "BFS: {}".format(", ".join(BreadthFirst(beginningNode)))
	
# every time you see Searches.visited, create a list called visited 
#in your method and replace it with that
#at the end of your method, return that and print it
	

#queue (not recursive)
#def BreadthFirst()

#extra credit is stack(not recursive)