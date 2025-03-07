using foundation.tests.math;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace foundation.math
{
    public struct BigModuloInteger : IEquatable<BigModuloInteger>
    {
        private readonly BigInteger mod;
        private readonly BigInteger value;

        public BigModuloInteger(BigInteger value, BigInteger MOD)
        {
            if (MOD <= 0)
                throw new InvalidOperationException();

            // Normalize into [0..MOD-1] range
            this.mod = MOD;
            this.value = ((value % MOD) + MOD) % MOD;
        }

        /// <summary>
        /// Accessor to the raw numeric value. (Always in [0..MOD-1] range)
        /// </summary>
        public BigInteger Value => value;
        public BigInteger MOD => mod;

        #region Operator Overloads

        public static BigModuloInteger operator +(BigModuloInteger a, BigModuloInteger b)
        {
            if (a.MOD != b.MOD)
                throw new InvalidOperationException();
            return a + b.Value;
        }

        public static BigModuloInteger operator +(BigModuloInteger a, BigInteger b)
        {
            // (a + b) mod M
            return new BigModuloInteger(a.value + b, a.MOD);
        }

        public static BigModuloInteger operator -(BigModuloInteger a, BigModuloInteger b)
        {
            if (a.MOD != b.MOD)
                throw new InvalidOperationException();

            return a + b.Value;
        }

        public static BigModuloInteger operator -(BigModuloInteger a, BigInteger b)
        {
            // (a - b) mod M
            return new BigModuloInteger(a.value - b, a.MOD);
        }

        public static BigModuloInteger operator *(BigModuloInteger a, BigModuloInteger b)
        {
            if (a.MOD != b.MOD)
                throw new InvalidOperationException();
            return a * b.Value;
        }

        public static BigModuloInteger operator *(BigModuloInteger a, BigInteger b)
        {
            // (a * b) mod M
            // Use BigInteger multiplication carefully if working with large values
            // But since they're always in [0..MOD-1], just do (a.value * b.value) % MOD
            return new BigModuloInteger(a.value * b, a.MOD);
        }

        /// <summary>
        /// Division under modulo is multiplication by the modular inverse (a * b^-1 mod M).
        /// For prime MOD, b^-1 = b^(MOD-2) mod M (Fermat's Little Theorem).
        /// </summary>
        public static BigModuloInteger operator /(BigModuloInteger a, BigModuloInteger b)
        {
            if (a.MOD != b.MOD)
                throw new InvalidOperationException();
            var divisor = b.Inverse().value;
            if (divisor == 0)
                throw new DivideByZeroException();
            return a * divisor;
        }

        #endregion

        #region Equality and Overrides

        /// <summary>
        /// Checks for equality of two ModInt values.
        /// </summary>
        public bool Equals(BigModuloInteger other)
        {
            return value == other.value && mod == other.mod;
        }

        public override bool Equals(object obj)
        {
            return obj is BigModuloInteger other && Equals(other);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public static bool operator ==(BigModuloInteger a, BigModuloInteger b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(BigModuloInteger a, BigModuloInteger b)
        {
            return !a.Equals(b);
        }

        public override string ToString()
        {
            return string.Format("{0} (mod {1})", value.ToString(), mod.ToString());
        }

        #endregion


        #region Utility: Inverse

        /// <summary>
        /// Returns the modular multiplicative inverse of this value under MOD.
        /// Uses Fermat's Little Theorem if MOD is prime: a^(MOD-2) mod MOD.
        /// Otherwise, you can use the Extended Euclidean Algorithm approach.
        /// </summary>
        public BigModuloInteger Inverse()
        {
            // For prime modulus:
            // return Pow(MOD - 2);

            // Or use the Extended Euclidean Algorithm to handle non-prime moduli:
            return new BigModuloInteger(ModInverse(value, mod), mod);
        }

        /// <summary>
        /// Fast exponentiation (a^exp) under the modulus.
        /// </summary>
        public BigModuloInteger Pow(BigInteger exp)
        {
            BigInteger result = 1;
            BigInteger baseVal = value;
            BigInteger e = exp;
            while (e > 0)
            {
                if ((e & 1) == 1)
                {
                    result = (result * baseVal) % MOD;
                }
                baseVal = (baseVal * baseVal) % MOD;
                e >>= 1;
            }
            return new BigModuloInteger(result, mod);
        }
        public BigInteger LegendreSymbol()
        {
            if (Primes.IsPrime(mod) == false)
                return -1;

            return Pow(mod - 1 >> 1).Value;
        }

        public (BigInteger, BigInteger) FindRoots()
        {
            var l = LegendreSymbol();
            if (l == -1 || l == mod - 1)
                throw new NotSupportedException();

            return TonelliShanks.Algorithm(value, mod);
        }


        /// <summary>
        /// Extended Euclidean Algorithm to find x such that (a*x) ≡ 1 (mod m).
        /// Returns 0 if inverse doesn't exist.
        /// </summary>
        private static BigInteger ModInverse(BigInteger a, BigInteger m)
        {
            BigInteger m0 = m;
            (BigInteger x0, BigInteger x1) = (0, 1);

            if (m == 1) return 0; // Inverse doesn't exist if mod=1

            while (a > 1)
            {
                BigInteger q = a / m;
                (a, m) = (m, a % m);
                (x0, x1) = (x1 - q * x0, x0);
            }

            // Make x1 positive
            if (x1 < 0)
                x1 += m0;
            return x1;
        }

        #endregion
    }
}
