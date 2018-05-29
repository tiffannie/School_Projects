'''
For this program, you will implement a stack and a queue, based on a linked list. Although all the sample inputs are integers, you can safely treat them as strings if you prefer.
Read in the data and parse the input. Each line has one or two tokens. Here are the possible values:
PUSH n pushes "n" (an integer) onto the top of the stack.
POP removes the top item from the stack and prints it. If the stack is empty, it prints "empty" instead.
CLEAR tells you to throw away the current stack contents if any.
PRINT prints a dump of the current stack contents.
The input will end with a single line of three asterisks. ***

Write another program. Like the first one, it should accept data from standard input. The input will end with a single line of three asterisks.
Parse the file. Each line has one or two tokens. Here are the possible values:
ENQUEUE n adds "n" (an integer) to the tail of the queue.
DEQUEUE removes the item from the head of the queue and prints it. If the queue is empty, it prints "empty" instead.
CLEAR tells you to throw away the current queue contents if any.
PRINT prints a dump of the current queue contents.

'''

class Node:

    def __init__(self, data=None, next_node=None):
        self.data = data
        self.next_node = next_node
	
	#returns which node
    def get_data(self):
        return self.data

	#returns next node
    def get_next(self):
        return self.next_node
	
	#sets child/neighbor node
    def set_next(self, new_next):
        self.next_node = new_next

		#top node of list
class List:

	def __init__(self): #initialized to have no nodes
		self.head = None
		
	def push(self, data):
		new_node = Node(data)
		if self.head != None:
			new_node.set_next(self.head)
			self.head = new_node
		else:
			self.head = new_node
	#know how many nodes
	def size(self):
		current = self.head
		count = 0
		while current:
			count += 1
			current = current.get_next()
		return count
	
	#gets rid of node
	def pop(self):
		if self.head != None:
			print(self.head.data)
			temp = self.head.get_next()
			self.head = temp
		else:
			print("empty")			
	
	def printStack(self):
		node_stack = []
		current = self.head
		while current != None:
			node_stack.insert(0,current.get_data())
			current= current.get_next() 
		for node in node_stack:
			print node

inputs = [] 
if __name__ == '__main__':
	list = List()
	while(1):
		a = raw_input().strip()
		if a == "***":
			break
		inputs.append(a)
	for element in inputs:
		b = element.split(" ")
		if b[0] == "PUSH":
			list.push(b[1])
		elif b[0] == "POP":
			list.pop()
		elif b[0] == "PRINT":
			list.printStack()
		elif b[0] == "CLEAR":
			list = List()
			
		