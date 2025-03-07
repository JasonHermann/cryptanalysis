using foundation.tests.math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace foundation.math
{
    /// <summary>
    /// https://en.wikipedia.org/wiki/Tonelli%E2%80%93Shanks_algorithm
    /// </summary>
    public static class TonelliShanks
    {
        public static (BigInteger, BigInteger) Algorithm(BigInteger n, BigInteger p)
        {
            // Assumptions:
            // P is prime 
            // n is a quadratic residue
            if (Primes.IsPrime(p) == false)
                throw new ArgumentException("p is not prime.");
            var l = LegendreSymbol(n, p);
            if(l != 1)
                return (0, 0);

            //Tonelli-Shanks
            (BigInteger s, BigInteger q) = Step0(p);
            BigInteger z = Step1(n, p);  // Find any quadratic non-residue.

            // Initialization
            var r = new BigModuloInteger(n, p).Pow((q + 1) >> 1); // r = n^(q+1)/2
            var t = new BigModuloInteger(n, p).Pow(q);            // t = n^q
            var c = new BigModuloInteger(z, p).Pow(q);            // c = z^q
            var m = s;


            var infiniteLoopDetector = 0;
            while (infiniteLoopDetector++ < 100000)
            {
                if (t.Value == 0) return (0, 0);
                if (t.Value == 1)
                {
                    var r1 = r.Value;
                    var r2 = p - r.Value;
                    return r1 < r2 ? (r1, r2) : (r2, r1);
                }

                for (int i = 1; i < m; i++)
                {
                    var temp = t.Pow(1 << i);
                    if (temp.Value == 1)
                    {
                        var b = c.Pow(1 << (int)(m - i - 1));
                        m = i;
                        c = b.Pow(2);
                        t = c * t;
                        r = r * b;
                        break;
                    }
                }
            }
            throw new NotSupportedException("No roots could be found using algorithm");
        }

        public static (long, long) Algorithm(long n, long p)
        {
            // Assumptions:
            // P is prime
            // n is a quadratic residue
            if (Primes.IsPrime(p) == false)
                throw new ArgumentException("p is not prime.");
            if (LegendreSymbol(n, p) != 1)
                return (0, 0);
                //throw new ArgumentException(string.Format("{0} is not a quadratic residue mod {1}", n, p));


            //Tonelli-Shanks
            (long s, long q) = Step0(p);
            long z = Step1(n, p);  // Find any quadratic non-residue.

            // Initialization
            var r = new ModuloInteger(n, p).Pow((q + 1) >> 1); // r = n^(q+1)/2
            var t = new ModuloInteger(n, p).Pow(q);            // t = n^q
            var c = new ModuloInteger(z, p).Pow(q);            // c = z^q
            var m = s;


            var infiniteLoopDetector = 0;
            while(infiniteLoopDetector++ < 100000)
            {
                if (t.Value == 0) return (0, 0);
                if (t.Value == 1)
                {
                    var r1 = r.Value;
                    var r2 = p - r.Value;
                    return r1 < r2 ? (r1, r2) : (r2, r1);
                }

                for(int i = 1; i < m; i++)
                {
                    var temp = t.Pow(1 << i);
                    if (temp.Value == 1)
                    {
                        var b = c.Pow(1 << (int)(m - i - 1));
                        m = i;
                        c = b.Pow(2);
                        t = c * t;
                        r = r * b;
                        break;
                    }
                }
            }
            throw new NotSupportedException("No roots could be found using algorithm");
        }

        public static long LegendreSymbol(long a, long p)
        {
            if (Primes.IsPrime(p) == false)
                return -1;

            var m = new ModuloInteger(a, p);
            return m.Pow((p - 1) >> 1).Value;
        }

        public static BigInteger LegendreSymbol(BigInteger a, BigInteger p)
        {
            if (Primes.IsPrime(p) == false)
                return -1;

            var m = new BigModuloInteger(a, p);
            return m.Pow((p - 1) / 2).Value;
        }

        public static (BigInteger, BigInteger) Step0(BigInteger p)
        {
            return Primes.FactorizePminus1(p);
        }

        public static (long, long) Step0(long p)
        {
            // Find s and q such that
            // p - 1 = 2^s * q (q is odd)
            long s = 0;
            long q = p - 1;

            while (q % 2 == 0)
            {
                q >>= 1;
                s += 1;
            }
            return (s, q);
        }

        public static long Step1(long n, long p)
        {
            // Return any value i ≠ n between [1, p-1] (inclusive)
            for (long i = 1; i <= p - 1; i++)
            {
                if (i == n) continue;
                var l = LegendreSymbol(i, p);
                if (l == -1 || l == p - 1)
                    return i;
            }
            throw new NotSupportedException();
        }

        public static BigInteger Step1(BigInteger n, BigInteger p)
        {
            // Return any value i ≠ n between [1, p-1] (inclusive)
            for (BigInteger i = 1; i <= p - 1; i++)
            {
                if (i == n) continue;
                var l = LegendreSymbol(i, p);
                if (l == -1 || l == p - 1)
                    return i;
            }
            throw new NotSupportedException();
        }

    }
}
