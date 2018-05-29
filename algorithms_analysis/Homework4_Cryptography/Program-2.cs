using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.IO;


namespace Cryptography
{
    class Program
    {/**
      *This program can take a message, encrypt it, and decrypt it using public and private keys
      *Generates prime numbers
      *checks if the numbers are prime using Fermat's Little Theorem
      *uses euclid algorithm to check the greatest common denomenator of e (a prime number you have to choose) and phi
      *Use extended euclids to do the same as euclid but also to find the private key which is s
      *use expomod to encrypt and decrypt a message.
      *
      * */
        static void Main(string[] args)
        {
            String message = File.ReadAllText("C:/Users/student/Desktop/hp.txt");
            BigInteger p = GeneratePrimesNaive();
            BigInteger q = GeneratePrimesNaive();
            //BigInteger p = 101;
            //BigInteger q = 179;
            BigInteger z = p * q;
            BigInteger phi = (p - 1) * (q - 1);
            BigInteger n = generateE(phi); //1<n<z-1 and no common factors with z other than 1     //book's "e" go to page 44 to see 
            //BigInteger n = 6952;                                                                 //it's better that e be 3. more efficient 
            BigInteger msg = 123; //type any integer here and your decoded message should be this same number

            Console.WriteLine("p = " + p);
            Console.WriteLine("q = " + q);
            Console.WriteLine("z = " + z);
            Console.WriteLine("Phi = " + phi);
            Console.WriteLine("n = " + n);
            BigInteger[] a = ExtendedEuclid(n, phi); //ax +by = d  a = n, b = y
            
            while (a[0] < 1)//my x' since I go up to n, phi
            {
                a[0] += phi;
            }
            //for (BigInteger i = 0; i < a.Length; i++)
            //{
            //    Console.WriteLine(a[i]);
            //}
            Console.WriteLine("a = " + String.Join(", ", a[0], a[1], a[2])); //print off x', y', d

            BigInteger encMsg = encode(msg, n, z);
            BigInteger decMsg = decode(encMsg, a[0], z);

            Console.WriteLine("Encoded message = " + encMsg);
            Console.WriteLine("Decoded message = " + decMsg);

            List<BigInteger> mes = encode(message, n, z);
            String decmes = decode(mes, a[0], z);

            Console.WriteLine(decmes);
        }
//1    ********************************************************************************
        //fermat's little theorem
        //if a^n mod n = a then n is prime
        public static BigInteger expomod(BigInteger a, BigInteger n, BigInteger z)
        {               //whatever number, any exponent, mod z
            BigInteger i = n;
            BigInteger r = 1;
            BigInteger x = a % z;              //magic
            while (i > 0)
            {
                if (i % 2 != 0)
                {
                    r = (r * x) % z;        //magic
                }
                x = (x * x) % z;
                i = i / 2;
            }
            return r;                   //more magic
        }

        //***************************************************************************************
        //check if an BigInteger is prime

        public static Boolean isPrime(BigInteger a, BigInteger n)
        {
            if (expomod(a, n - 1, n) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //***************************************************************************************
        //generate prime numbers
        public static Random r = new Random();              //use random class to generate prime #
        public static BigInteger GeneratePrimesNaive()
        {
            BigInteger p = r.Next(100, 200);                   //generate random number
            for (BigInteger i = 0; i < p; i++)                 //from i to the random number
            {
                if (expomod(i, (BigInteger)p - 1, (BigInteger)p) != 1) //same as a^(p-1) modular p
                {        //#,  rand # -1,  rand #
                    p = r.Next(100, 200);               //chooses another random number if the remainder is not 1
                    i = 0;
                }
            }
            return p;
        }
        //3    ********************************************************************************* public key(n,z) 
        //choose e such that 1<e<n-1 and gcd(e,phi)=1
         public static BigInteger generateE(BigInteger phi)
         {
             BigInteger e = r.Next(1, (int)phi);
             while (true)
             {
                 if (euclid(phi, e) != 1)
                 {
                     e = r.Next(1, (int)phi);
                 }
                 else if(e ==1)
                 {
                     e = r.Next(1, (int)phi); 
                 }
                 else
                 {
                     return e;
                 }
             }
         }
        //calculates greatest common denominator of two numbers
        public static BigInteger euclid(BigInteger a, BigInteger b)              //checks that n and phi have GCD of 1
        {
            if (b == 0)
            {
                return a;
            }
            return euclid(b, a % b);
        }
        //calculate GCD of two numbers,
        public static BigInteger[] ExtendedEuclid(BigInteger a, BigInteger b)                //gets S or the inverse of d
        {                                 //   n,   phi
            if (b == 0)
            {
                BigInteger[] arr = { 1, 0, a };
                return arr;
            }
            BigInteger[] xyd = ExtendedEuclid(b, a % b);
            BigInteger[] yxd = { xyd[1], xyd[0] - (a / b) * xyd[1], xyd[2] };
            return yxd;
        }
        //*********************************************************************************************
        static BigInteger encode(BigInteger m, BigInteger n, BigInteger z)
        {                  //message, e,    p*q
            return expomod(m, n, z);
        }
        static BigInteger decode(BigInteger c, BigInteger s, BigInteger z)
        {
            return expomod(c, s, z);
        }
        static List<BigInteger> encode(String v, BigInteger n, BigInteger z)
        {                       //message to code raised to public key 
            List<BigInteger> d = new List<BigInteger>();
            foreach(char c in v)
            {
                BigInteger u = (BigInteger)c;
                d.Add(encode(u, n, z));
            }
            return d;
        }
        static string decode(List<BigInteger> v, BigInteger n, BigInteger z)
        {                       //coded message raised to secret key and mod'ed by z
            string output = "";
            foreach (BigInteger c in v)
            {
                int u = (int)c;
                output += (char)(encode(u, n, z));
            }
            return output;
        }
    }
}
