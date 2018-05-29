#Write a program that reads in a text file. The name of the file is passed into your program as a command-line parameter. The file will consist of several lines of random characters. Each character is separated by a space. Your program must read through each line and count the number of times the character "8" (eight) occurs
#user inputs file and if not, the file dies
if (@ARGV==0){
	die "failed to have input";
} 
#the file name is being stored to a variable
$lola = $ARGV[0];
#the file is opened and the contents are placed into an array
open (INPUT, $lola) or die "error 404 $lola not found";
@input = <INPUT> ;
close INPUT;
#each item is stored to a variable
foreach $x (@input) {
	 chomp $x ;
#the spaces are filtered out and the remaining items are placed in an array
	@tom = split(/ /,$x);
#each item is reviewed and if it is the no. 8 then it incremented
	foreach $y (@tom) {
		if ($y == 8) {
			$count += 1;
		}
	}
}
print "$count\n";