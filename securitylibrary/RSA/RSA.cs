using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.RSA
{
    public class RSA
    {
        public int Encrypt(int p, int q, int M, int e)
        {
            var n = p * q;
            var x = ModularArithmetic(n, M, e); // cipher = M^e mod n
            return x;
        }

        public int Decrypt(int p, int q, int C, int e)
        {
            var n = p * q;
            var a = (p - 1) * (q - 1);
            var d = ExtendedEuclid1(a, e); //d = (e^-1) mod a
            var x = ModularArithmetic(n, C, d); //plain text = C^d mod n 
            return x;
        }
        public static int ExtendedEuclid1(int base_n, int number) //(number^-1) mod (base_n)..27^-1 mod 392
        {                                                          //392=27(14)+14
            var a = base_n;                             //base_n = number(base_n/number)+(base_n mod number)
            var res = 0;
            var w = 1;
            var temp = 0;
            var temp1 = 0;
            var x = 0;
            if (GCD(number, base_n) == 1)
            {
                while (number > 0) // 
                {
                    temp1 = a / number;  // base_n / number 392/27= 14 ............ 27/(14)=1
                    x = number;          // x = 27 ....(14)
                    number = a % x;      // number = 392 mod 27= 14 ............... 27 mod 14 = (13)
                    a = x;               // a(new base_n) = 27 @27 = 14(1) + 13.... 14 @14=13(1)+1
                    x = w;               // x = 1 ................................. 1
                    temp = temp1 * x;     // temp = 14 ............................. 1
                    w = res - temp;      // w = -14 ............................... 0
                    res = x;             // res = 1 ............................... 1

                } //res = -29
                res %= base_n;            // -29 mod 392 = -29
                x = res + base_n;         // -29 + 392 = 363
                res = x % base_n;         //  363 mod 392 = 363
            }
            else res = -1;
            return res;
        }
        public static int GCD(int a, int b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            return a == 0 ? b : a;
        }

        public static int ModularArithmetic(int q, int alpha, int x)  //(alpha^x) mod q 
        {
            var Y = 0.0;

            if (x % 2 == 0) //even power
            {


                int i = x / 2; //iteration is repeated half the power
                var a1 = Math.Pow(alpha, 2) % q; //(alpha^2) mod q
                var temp = 1.0;
                for (int j = 0; j < i; j++) //( (alpha^2) mod q )*( (alpha^2) mod q )*.......
                {
                    temp *= a1;
                    temp %= q;
                }

                Y = temp % q;

            }
            else //odd power
            {
                x = x - 1;
                int i = x / 2;
                var aa1 = Math.Pow(alpha, 2) % q;
                var temp = Math.Pow(alpha, 1) % q; //one more iteration for the odd number=((alpha^1) mod q)

                for (int j = 0; j < i; j++)
                {
                    temp *= aa1;
                    temp %= q;
                }

                Y = temp % q;
                x++;
            }
            return (int)Y;
        }


    }
}
