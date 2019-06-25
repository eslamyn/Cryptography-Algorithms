using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.AES
{
    public class ExtendedEuclid 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <param name="baseN"></param>
        /// <returns>Mul inverse, -1 if no inv</returns>
        public int GetMultiplicativeInverse(int number, int baseN)
        {
            return ExtendedEuclid1(baseN, number);
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
    }
}
