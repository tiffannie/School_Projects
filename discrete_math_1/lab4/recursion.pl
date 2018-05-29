#Write a program that reads in a text file. The name of the file is passed into your program as a command-line parameter. The file will contain many numbers, one number per line. Your program will read the numbers into an array (called A in the pseudocode below). To sort the numbers in the array, call sort205(0, N-1); (replace N with the length of array A). Then print out the contents of the sorted array to the console.

For full credit, your program must implement the following two subroutines, the pseudocode for which are listed below. Each subroutine takes two parameters, p and r. p is the first parameter (i.e. $_[0]) while r is the second parameter
#user inputs file and if not, the file dies
if (@ARGV==0){
	die "failed to have input";
} 
#the file name is being stored to a variable
$lola = $ARGV[0];
#the file is opened and the contents are placed into an array
open (INPUT, $lola) or die "error 404 $lola not found";
@A = <INPUT> ;
close INPUT;
#each item is stored to a variable
foreach $x (@A) {
	print "$x"; #prints original order of array
}
print "\n";
sub sort205 {
	my $p = $_[0]; #declare r & p as the 1st & 2nd elements
	my $r = $_[1];
	if ($p < $r) { 
		$q = divide($p,$r); #calls divide method if 1st# < 3rd#
		sort205($p, $q-1);
		sort205($q+1, $r);
	}
}

sub divide {
	my $p = $_[0];
	my $r = $_[1];
	$x = @A[$r];
	$i = $p-1;

	for ($j = $p; $j <= $r-1; $j++) {
		if (@A[$j] <= $x) {
			$i++;
			$temp = @A[$i]; #exchanging i with j
			@A[$i] = @A[$j]; 
			@A[$j] = $temp;
		}
	}
	$temp = @A[$i+1];
	@A[$i + 1] = @A[$r];
	@A[$r] = $temp; 
	return ($i+1);
}
my $length = @A;
sort205(0, $length - 1);

foreach $x (@A) {
	print $x;
}	
