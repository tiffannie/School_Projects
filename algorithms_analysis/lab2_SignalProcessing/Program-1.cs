using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Signal processing deals with the analysis and manipulation of signals. A signal is any time varying quantity, and can be thought of as a function over time. An example of a signal is a codified message being sent across a communication channel where at each time t, a certain symbol is received. A sound recording is an example of a signal which has a specific amplitude (or volume) at each time t. The pitch heard is determined by the frequency of the changing signal. 

In this project you will implement an efficient method for convolving two signals. The files you will be given are .raw files which are essentially .wav files that had the headers removed. They are all standardized to have:

a sample rate of 22050,
16 bit word sizes per sample,
linear 2’s compliment encoding, and
1 channel (mono as opposed to stereo).
Which means that each sample of the sound file is a 16 bit 2’s compliment number, so you can just read in 16 bits and interpret them as an integer (or short). Since it is a binary file there are no spaces or parsing, just a stream of 16 bit words. A helpful class you may want to use for reading in binary data is a BinaryReader.

To output the convolved signal the program should just output 16 bit integers (or shorts) to a stream one after another. Make sure that you don’t just output standard 32 bit integers or there will be extra data in the sound file and it will sound garbled. Again, a helpful class you may want to use is the BinaryWriter.

Once you output the raw sound file you can listen to it if you have a player that can play raw sound files, such as Audacity. When opening a raw file it should ask for the file format (sample rate, word size, encoding, and channel) and when you enter the format (as specified above: 22050 sample rate, 16 bit word size, linear 2’s comp. encoding, 1 channel) it will then play the raw file.

Your assignment for this project is to implement the Fast Fourier Transform algorithm in C#. Your program will read in two different signals from two different files. It will then produce a new signal that is the convolution of the two input signals and output this new signal to a new file. The filenames for both input signals and output signal will need to be entered on the command line as parameters. Running your program from the command line must look like this:

C:\>convolution.cs inputfile1.raw inputfile2.raw outputfile.raw

Here are some guidelines/helps for implementing Fast Fourier Transform.

Follow the algorithm as it shows in the slides, you may also refer to Section 2.6 of the textbook.
Use the complex roots of unity to decide at which points to evaluate A(xj) and B(xj), the Complex class might be helpful.
Your program should use divide and conquer to evaluate the functions A and B in O(nlogn) time.
One easy way to do the inverse FFT is as follows:
take the conjugate of each point of the signal.
take the forward FFT.
take the pointwise conjugate again.
divide each point by n.
When you compute the FFT of your signals, multiply the results together, and compute the inverse FFT you will end up with numbers that are too large to fit in a 16 bit number. If you divide every number in your signal by the same amount you will preserve frequency (and the sound), and you will decrease the magnitude (or volume). A 16 bit signed number can hold numbers up to +/- 32,768. You should divide each value in your signal by some number which will make your absolute maximum value (positive or negative) be within the range of a 16 bit number. For good results, make sure your values fit well into that number, say below 20,000. This should make your signal output work well. Example (this is not intended to be actual code, but to give you an idea how it should work):

double divideNumber = absoluteMax / 20000;

for(int i = 0;i < signal.Length; i++)

{

          double smallNumber = signalValue[i] / divideNumber;

          double roundedNumber = Math.Round(smallNumber);

          short outVal = System.Convert.ToInt16(roundedNumber);

          binaryWriter.Write(outVal);

}

Once you have your program completed, you need to convolve the five pairs of signals found in the following table which show five different applications of convolution.
 * 
 * 
 * Signals	Impulse Responses	Desired Result
BlakesPiano.raw	StNicolaesChurch.raw	Blake's piano in St. Nicolaes Church
Classical.raw	lowpass.raw	Filter out high pitched sounds
Classical.raw	highpass.raw	Filter out the low sounds
Voice.raw	bandpass.raw	Produce telephone effect on voice
NoisySong.raw	notch60hz.raw	Should remove the buzzing sound
 * */


namespace FFT1
{
    class Program
    { 
        static void Main(string[] args)
        {
            List<short> signal = new List<short>();
            List<short> bgsound = new List<short>(); //background sound

            String sound = args[0];         //get the file
            String response = args[1];
            String output = args[2];        //output file to write to

            BinaryReader b = new BinaryReader(File.Open(sound, FileMode.Open));     //use BinaryReaders to open the files
            BinaryReader bb = new BinaryReader(File.Open(response, FileMode.Open)); 
                                                                                    //C:\Users\tiffannie\Desktop\CS 301\BlakesPiano.raw
                                                                                    //C:\Users\tiffannie\Desktop\CS 301\\StNicolaesChurch.raw
           //***************************************************************************
            
                                        // Position and length variables.
            int pos = 0;
                                        // Use BaseStream.
            int length_sound = (int)b.BaseStream.Length;
           while (pos < length_sound)
           {
                                       // Read integer.
                short v = b.ReadInt16();
                //Console.WriteLine(v);
                signal.Add(v);    // Advance our position variable.
                pos += sizeof(short);
            }
            //**************************************************************************
            int pos1 = 0;
            int length_background = (int)bb.BaseStream.Length;
            while (pos1 < length_background)
            {
                                        // Read integer.
                short v = bb.ReadInt16();
                bgsound.Add(v);
                                        // Advance our position variable.
                pos1 += sizeof(short);
            }
            //****************************************************************************************
            //get next highest power of two from longest list
            double n = 0;
            double power = 0;
            while (true)
            {
                power = Math.Pow(2, n);
                //compare the lists to find the larger of the two
                                                                                //use array.count because the elements were converted 16 bit integers (2 bytes)
                if (signal.Count > bgsound.Count && signal.Count < power) //if the signal is bigger than response and the signal is less than the power number
                {
                    break;                                                      //break from the if statement because you're done and've found the next highest power of 2
                } else
                {
                    n++;                                                        //otherwise, increment the exponent
                }

               if (bgsound.Count > signal.Count && bgsound.Count < power) //do the same for the response signal just in case.
                {
                    break;                                                      //you've found highest power
                } else
                {
                    n++;                                                        //increment until otherwise 
                }
            }
            //**************************************************************************************
            //padding the arrays with zeros 

            int signalLen = signal.Count;         //get length of both signals
            int bgsoundLen = bgsound.Count;
            while (signalLen < power)               //while the signal length is less than the # 
            {                                       //nearest power of n
                signal.Add(0);
                signalLen++;                        //add a zero to the end of the list until it's
            }                                       //the length of the next highest power               
            while (bgsoundLen < power)
            {
                bgsound.Add(0);                     //do the same for the background list
                bgsoundLen++;
            }
            //*******************************************************************************************
            //convert elements of lists to complex numbers
            //yhrdr sttsyd have the complex numbers
            Complex[] newSignal = new Complex[signalLen];
            Complex[] newImpulse = new Complex[bgsoundLen];

            for (int i = 0; i < signalLen; i++)     ///add elements of double list to a complex list because I don't know how else to cast
            {
                newSignal[i] =  new Complex(signal[i],0);    
                newImpulse[i] = new Complex(bgsound[i],0);
            }
            
            //**************************************************************************
            //run fast fourier on the lists
            Complex[] finalNewSignal= FFT(newSignal);
            Complex[] finalNewImpulse = FFT(newImpulse);

            //*********************************************************************************combine the two lists PWM
            //pointwise multiplication
            Complex[] multiplied = new Complex[signalLen]; //array for the pointwise multiplied lists
            for (int i = 0; i < finalNewSignal.Length; i++)
            {
                multiplied[i] = finalNewSignal[i] * finalNewImpulse[i]; //multiply value of initial sound to response sound
            }
            //**************************************************************************************************
            for (int i = 0; i < multiplied.Length; i++)
            {
                multiplied[i] = Complex.Conj(multiplied[i]);            //use conjugate to get inverse of the FFT array
            }                                                           // to get back to complex form of the short

            Complex[] finalFFT = FFT(multiplied);
            
            //**********************************************************************************************************

            for (int i = 0; i < finalFFT.Length; i++)
            {
                finalFFT[i] = new Complex(finalFFT[i].Re / n, finalFFT[i].Im / n);
            }

            List<double> finalFFTSignal = new List<double>();

            foreach(Complex c in finalFFT)
            {
                finalFFTSignal.Add((double)c.Re);
            }
            //*****************************************************************write output file using binary writer
            double absoluteMax = finalFFTSignal.Max();
            double divideNum = absoluteMax / 20000;

            using (BinaryWriter bbb = new BinaryWriter(File.Open(output, FileMode.Create)))
            {
                for(int i = 0; i < multiplied.Length; i++)
                {
                    double small = Math.Round(finalFFTSignal[i] / divideNum);
                    short outfile = Convert.ToInt16(small);
                    bbb.Write(outfile);
                }
                bbb.Close();
            }
        }
//*******************************************************************************************************************************************************
        public static Complex[] FFT(Complex[] a)
        {
            
            int n = a.Length;                       //get length of array
            Complex[] aEven = new Complex[n / 2];
            Complex[] aOdd = new Complex[n / 2];
            Complex[] A = new Complex[n];
            if (n == 1)
            {
                return a;
            }
           
                                                         //cannot do (2 * Math.PI * i) / n because i is unknown but e**PI(i)/n is equivalent
            double real = Math.Cos(2 * Math.PI / n);      //equivalent to real number
            double imaginary = Math.Sin(2 * Math.PI / n); //equivalent to imaginary number

            //Complex omegaBaseN = new Complex(real, imaginary); //omega base n (Wn)

            Complex omega = new Complex(1,0);                   //omega (W)
            Complex omegaBaseN = new Complex(real, imaginary);
            int even = 0;
            for(int i= 0; i < n; i += 2)                    //even values and locations
            {
                aEven[even] = a[i];//get even values of array and place them in their own list
                even++;
            }
            int odd=0;
            for (int i = 1; i < n; i += 2)
            {
                aOdd[odd] = a[i]; //get odd values of array and place them in their own list
                odd++;
            }

            Complex[] AEven = FFT(aEven);                 //run the method on the broken up lists
            Complex[] AOdd = FFT(aOdd);
            List<Complex> ABaseK = new List<Complex>();

            for (int j = 0; j <= ((n/2)-1); j++)
                {
                    A[j] = AEven[j] + (omega * AOdd[j]);
                    A[j +(n/2)] = AEven[j] - (omega * AOdd[j]);
                    omega = omega * omegaBaseN;
                }
            return A;
        }
        /* an attempt to outsource pointwise multiplication to a method
         * 
        public static List<Complex> Pointwise(List<Complex> a, List<Complex> b)
        {
            List<Complex> pointwiseA = new List<Complex>();
            for(int i=0; i < a.Count(); i++)
            {
                Complex newPoint = a[i] * b[i];
                pointwiseA.Add(newPoint);
            }
            return pointwiseA;
        }*/
    }
}
