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
		#self.head = None
		self.tail = None
		
	def enqueue(self, data):
		new_node = Node(data)
		if self.tail == None:
			self.tail = new_node
		else:
			current = self.tail
			while current.next_node != None: #while there is a next node
				#sets current to the child node until there are none left
				current= current.get_next() #last one in queue
			#sets next node as last node. last child
			current.set_next(new_node)
				
			
			
	#know how many nodes
	def size(self):
		current = self.head
		count = 0
		while current:
			count += 1
			current = current.get_next()
		return count
	
	#gets rid of node
	def dequeue(self):
		if self.tail != None:
			print(self.tail.data)
			temp = self.tail.get_next()
			self.tail = temp
		else:
			print("empty")			
	
	def printStack(self):
		node_stack = []
		current = self.tail
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
		if b[0] == "ENQUEUE":
			list.enqueue(b[1])
		elif b[0] == "DEQUEUE":
			list.dequeue()
		elif b[0] == "PRINT":
			list.printStack()
		elif b[0] == "CLEAR":
			list = List()
			
		