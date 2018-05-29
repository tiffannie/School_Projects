'''
Read in the data.
The data will have 2 or more lines. The first line states how many numbers (not necessarily lines) will follow. Each line thereafter may have 1 or more numbers on it. You can safely assume that all input files are in the correct format (i.e. no letters, only numbers).
Your program will read in all the numbers and store them in an array.
Print out the sum of all the numbers.
Print out the mean (arithmetic average) of all the numbers. This is the sum divided by how many numbers there are.
Print out the median of the numbers.
'''
x = int(input()) 	#gives you first input
count = 0
num = []		#create array list to store number values
while(count < x):
    
	vals = raw_input().split(" ") #strip to get rid of white space. raw reads it in as a string
	
	for i in vals:
		num.append(float(i))#adds/"appends" numbers into an array and type cast the elements from strings to float
	
	count = len(num)

#print the sum
print("Sum: " + str(format(sum(num),'.2f')))

#print the mean
print("Mean: " + str(format(sum(num)/len(num),'.2f')))

#print the median
def median(num):
    num = sorted(num)
    if len(num) < 1:
            return None
    if len(num) %2 == 1:
            return num[((len(num)+1)/2)-1]
    else:
            return float(sum(num[(len(num)/2)-1:(len(num)/2)+1]))/2.0

print("Median: " + str(format(median(num),'.2f')))