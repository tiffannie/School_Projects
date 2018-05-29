#part two

garb = []
num = []
garb1 = []
letters_of_decoded_messsage = []
for t in range(2):
	t = raw_input()
	garb.append(t)

#split by lines
for a in garb:
	garb1.append(a.rstrip("\n"))

#first line is message in encoded text
message = garb1[0]

#numeric value of k. specifies key to decode message
key = int(garb1[1])

#decode the message **************

#f(p)= g^-1([g(p)-k]mod26)
for a in message:
	if a != " ":
		#to get letter of DECODED message (g(p) - k) mod 26
		index_of_letter = ((ord(a)-65)-key) % 26 #to get 0-25 index
		#g^-1
		letters_of_decoded_messsage.append(chr(index_of_letter+65)) #to convert the letter back
	else: 
		letters_of_decoded_messsage.append(" ")
print("".join(letters_of_decoded_messsage))
