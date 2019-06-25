using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.DiffieHellman
{
    public class DiffieHellman
    {

        public List<int> GetKeys(int q, int alpha, int xa, int xb)
        {

            List<int> key = new List<int>();

            var Ya = ModularArithmetic(q, alpha, xa); //Ya=(alpha^xa) mod q

            var Yb = ModularArithmetic(q, alpha, xb); //Yb=(alpha^xb) mod q

            var Ka = ModularArithmetic(q, Ya, xb); //Ka=(Ya^xb) mod q

            var Kb = ModularArithmetic(q, Yb, xa); //Kb=(Yb^xa) mod q

            int ka = (int)Ka;
            key.Add(ka);

            int kb = (int)Kb;
            key.Add(kb);

            return key;

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
