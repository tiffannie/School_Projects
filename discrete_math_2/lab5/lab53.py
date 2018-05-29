#part three

garb = []
num = []
garb1 = []

for t in range(1):
	t = raw_input()
	garb.append(t)

#split by lines
for a in garb:
	garb1.append(a.rstrip("\n"))

#first line is message in encoded text
message = garb1[0]

#key not given so it must be betweet 0-25

print("CIPHERTEXT: " + message)
#decode the message **************
for key in range(0,26):
	#new empty array for each loop
	letters_of_decoded_messsage = []
	#f(p)= g^-1([g(p)-k]mod26)
	for a in message:
		if a != " ":
			#to get letter of DECODED message (g(p) - k) mod 26
			index_of_letter = ((ord(a)-65)-key) % 26 #to get 0-25 index
			#g^-1
			letters_of_decoded_messsage.append(chr(index_of_letter+65)) #to convert the letter back
		else: 
			letters_of_decoded_messsage.append(" ")
	mess = "".join(letters_of_decoded_messsage)
	print("key: " + str(key) + " decodes to: " + mess )
