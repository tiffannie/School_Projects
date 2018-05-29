'''
Your program should accept input from standard input (not a file.)
The first line of the input will contain a message in plain text. The second line of the input will be a numeric value k that specifies the key to be used to encode the message.
Print out the encoded message to the console.
Suggestion: You may find the example on pages 367–368 of the textbook very helpful.

Little does Darth Sidious know, however, that you are in fact a Jedi padawan working for the Rebel Alliance. Thus, you have been secretly transmitting all of Darth Sidious's messages to Alliance officials, along with the key needed to decode them.
'''

#part one

garb = []
num = []
garb1 = []
letters_of_encoded_messsage = []
for t in range(2):
	t = raw_input()
	garb.append(t)

		#split by lines

for a in garb:
	garb1.append(a.rstrip("\n"))

#first line is message in plain text
message = garb1[0]

#numeric value of k. specifies key to encode message
key = int(garb1[1])

#encode the message **************
#gives the unicode code of the character. the values of the letters. A is 65

#f(p)= g^-1([g(p)+k]mod26)
for a in message:
	if a != " ":
		#to get letter of ENCODED message (g(p) + k) mod 26
		index_of_letter = ((ord(a)-65)+key) % 26 #to get 0-25 index
		#g^-1
		letters_of_encoded_messsage.append(chr(index_of_letter+65)) #to convert the letter back
	else: 
		letters_of_encoded_messsage.append(" ")
print("".join(letters_of_encoded_messsage))

#abc = ord (key) + cipher #cipher offset
#while abc < 65:
#	abc = abc +26
#while abc > 90:
#	abc = abc - 26
#outputhere
