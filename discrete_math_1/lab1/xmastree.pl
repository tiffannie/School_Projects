#The purpose of this assignment is to refresh your programming skills from CIS 101. Specific skills that you will need:

If-statements
Loops
Subroutines
Command-line parameters
my $input = @ARGV[0]; #the input variable is what users put in command line
if (@ARGV[0] == "") {
	print "ERROR: Please supply command-line parameter."
}
if (@ARGV[0] < 0) {
	print " ERROR: Number must be non-negative.";
}
sub tree {

	for(my $stars=1; $stars<=$input; $stars++) {#the row will increase by one as long
										#as it is less than imputed number of stars
	printrow ($input-$stars,$stars);#this calls the subroutine to give parameters of tree
	}
	
}

sub printrow {
	
	my $stars = $_[1];
	my $space = $_[0];
	print " " x($space); #a space is printed for number of spaces
	print " *" x$stars, "\n";#a star is printed for every time a row is increased
	
}
tree();
