using foundation.math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace foundation.tests.math
{
    public static class Primes
    {
        public static long GCD(long a, long b)
        {
            if (a < b)
                return GCD(b, a);
            if (b == 0)
                return a;

            // Find smallest remainder (r) of a = k b + r
            var k = a / b;
            var r = a - k * b;

            return GCD(b, r);
        }

        public static bool AreCoPrime(long a, long b)
        {
            return GCD(a, b) == 1;
        }

        public static (long, long) ExtendedEuclid(long a, long b, long s_0 = 1, long s_1 = 0, long t_0 = 0, long t_1 = 1)
        {
            if (a < b)
            {
                var (u, v) = ExtendedEuclid(b, a, s_0, s_1, t_0, t_1);
                return (v, u);
            }
            if (b == 0)
                return (s_0, t_0);

            // Find smallest remainder (r) of a = k b + r
            var k = a / b;
            var r = a - k * b;
            var s_2 = s_0 - k * s_1;
            var t_2 = t_0 - k * t_1;

            return ExtendedEuclid(b, r, s_1, s_2, t_1, t_2);
        }

        /// <summary>
        /// Returns the solution to a congruent to b (modulo n).
        /// In other words, return the smallest value, b, s.t. 0 <= b <= n and
        /// a and b are congruent modulo n.
        /// </summary>
        public static long Modulo(long a, long n)
        {
            if (n <= 0)
                throw new InvalidOperationException();
            if (a >= 0 && a <= n)
                return a;
            if (a < 0)
                a = a * -1;
            return a - (a / n) * n;
        }

        public static HashSet<long> PrimeNumbers = new HashSet<long>();
        public static long MaxSearch = 1;

        public static void FindAllPrimesUpToN(long n)
        {
            if (n <= 0) return;
            if (n <= MaxSearch) return;
            for (long i = MaxSearch + 1; i <= n; i++)
            {
                bool isPrime = true;
                foreach (var p in PrimeNumbers)
                {
                    if (GCD(p, i) != 1)
                    {
                        isPrime = false;
                        continue;
                    }
                }
                if (isPrime)
                {
                    PrimeNumbers.Add(i);
                }
            }
            MaxSearch = n;
        }
        public static bool IsPrime(long a)
        {
            if (a == 0) return false;
            if (a < 0)
            {
                a *= -1;
            }
            if (a > 10000)
                throw new NotSupportedException("Try using the other implementation of this function with BigIntegers");

            if (a > MaxSearch)
            {
                FindAllPrimesUpToN(a);
            }

            return PrimeNumbers.Contains(a);
        }

        public static bool IsPrime(BigInteger possiblePrime)
        {
            if (possiblePrime < 1000)
            {
                return IsPrime((long)possiblePrime);
            }
            if (possiblePrime % 2 == 0)
            {
                return false;
            }

            (BigInteger s, BigInteger d) = FactorizePminus1(possiblePrime);
            var pminus1 = new BigModuloInteger(possiblePrime - 1, possiblePrime);
            var one = new BigModuloInteger(1, possiblePrime);

            // Pick several "random" bases
            // If none have proof p is composite, we assume p is prime.
            // Test with every prime up to 100
            Primes.IsPrime(100);
            var primesTo100 = PrimeNumbers.Take(25).ToList();
            foreach(var p in primesTo100)
            {
                var a = new BigModuloInteger(new BigInteger(p), possiblePrime);

                bool isDefinitelyComposite = true;
                var powerOfTwo = new BigInteger(1);
                for(BigInteger s0 = 0; s0 <= s; s0++)
                {
                    if (s0 != 0)
                        powerOfTwo *= 2;
                    var a0 = a.Pow(powerOfTwo * d);
                    if (a0 == pminus1 || a0 == one)
                    {
                        isDefinitelyComposite = false;
                        break;
                    }
                }
                if (isDefinitelyComposite == true)
                    return false;
            }
            return true;
        }

        public static (BigInteger, BigInteger) FactorizePminus1(BigInteger p)
        {
            // Find s and q such that
            // p - 1 = 2^s * q (q is odd)
            BigInteger s = 0;
            BigInteger q = p - 1;

            while (q % 2 == 0)
            {
                q >>= 1;
                s += 1;
            }
            return (s, q);
        }
    }
}
