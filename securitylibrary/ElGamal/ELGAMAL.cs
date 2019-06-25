using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.ElGamal
{
    public class ElGamal
    {
        /// <summary>
        /// Encryption
        /// </summary>
        /// <param name="alpha"></param>
        /// <param name="q"></param>
        /// <param name="y"></param>
        /// <param name="k"></param>
        /// <returns>list[0] = C1, List[1] = C2</returns>



        public List<long> Encrypt(int q, int alpha, int y, int k, int m)
        {
            List<long> res = new List<long> { };


            long c1 = ModularArithmetic(q, alpha, k);
            int K = ModularArithmetic(q, y, k);
            long C2 = (K * m) % q;
            res.Add(c1);
            res.Add(C2);

            return res;

        }


        public int Decrypt(int c1, int c2, int x, int q)
        {
            int K = ModularArithmetic(q, c1, x);
            var extented_res = ExtendedEuclid1(q, K);
            c2 = c2 % q;
            int M = (c2 * extented_res) % q;
            return M;
        }


        public static int ExtendedEuclid1(int base_n, int number)
        {
            var a = base_n;
            var res = 0;
            var w = 1;
            var temp = 0;
            var temp1 = 0;
            var x = 0;
            if (GCD(number, base_n) == 1)
            {
                while (number > 0)
                {
                    temp1 = a / number;
                    x = number;
                    number = a % x;
                    a = x;
                    x = w;
                    temp = temp1 * x;
                    w = res - temp;
                    res = x;

                }
                res %= base_n;
                x = res + base_n;
                res = x % base_n;
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
                var temp = Math.Pow(alpha, 1) % q; //one more iteration for the odd number = ( (alpha^1) mod q )

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
