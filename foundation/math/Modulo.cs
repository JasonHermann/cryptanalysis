using foundation.tests.math;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace foundation.math
{
    public struct ModuloInteger : IEquatable<ModuloInteger>
    {
        private readonly long mod;
        private readonly long value;

        public ModuloInteger(long value, long MOD)
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
        public long Value => value;
        public long MOD => mod;

        #region Operator Overloads

        public static ModuloInteger operator +(ModuloInteger a, ModuloInteger b)
        {
            if (a.MOD != b.MOD)
                throw new InvalidOperationException();
            return a + b.Value;
        }

        public static ModuloInteger operator +(ModuloInteger a, long b)
        {
            // (a + b) mod M
            return new ModuloInteger(a.value + b, a.MOD);
        }

        public static ModuloInteger operator -(ModuloInteger a, ModuloInteger b)
        {
            if (a.MOD != b.MOD)
                throw new InvalidOperationException();

            return a + b.Value;
        }

        public static ModuloInteger operator -(ModuloInteger a, long b)
        {
            // (a - b) mod M
            return new ModuloInteger(a.value - b, a.MOD);
        }

        public static ModuloInteger operator *(ModuloInteger a, ModuloInteger b)
        {
            if (a.MOD != b.MOD)
                throw new InvalidOperationException();
            return a * b.Value;
        }

        public static ModuloInteger operator *(ModuloInteger a, long b)
        {
            // (a * b) mod M
            // Use long multiplication carefully if working with large values
            // But since they're always in [0..MOD-1], just do (a.value * b.value) % MOD
            return new ModuloInteger(a.value * b, a.MOD);
        }

        /// <summary>
        /// Division under modulo is multiplication by the modular inverse (a * b^-1 mod M).
        /// For prime MOD, b^-1 = b^(MOD-2) mod M (Fermat's Little Theorem).
        /// </summary>
        public static ModuloInteger operator /(ModuloInteger a, ModuloInteger b)
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
        public bool Equals(ModuloInteger other)
        {
            return value == other.value && mod == other.mod;
        }

        public override bool Equals(object obj)
        {
            return obj is ModuloInteger other && Equals(other);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public static bool operator ==(ModuloInteger a, ModuloInteger b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(ModuloInteger a, ModuloInteger b)
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
        public ModuloInteger Inverse()
        {
            // For prime modulus:
            // return Pow(MOD - 2);

            // Or use the Extended Euclidean Algorithm to handle non-prime moduli:
            return new ModuloInteger(ModInverse(value, mod), mod);
        }

        /// <summary>
        /// Fast exponentiation (a^exp) under the modulus.
        /// </summary>
        public ModuloInteger Pow(long exp)
        {
            long result = 1;
            long baseVal = value;
            long e = exp;
            while (e > 0)
            {
                if ((e & 1) == 1)
                {
                    result = (result * baseVal) % MOD;
                }
                baseVal = (baseVal * baseVal) % MOD;
                e >>= 1;
            }
            return new ModuloInteger(result, mod);
        }
        public long LegendreSymbol()
        {
            if (Primes.IsPrime(mod) == false)
                return -1;

            return Pow(mod - 1 >> 1).Value;
        }

        public (long, long) FindRoots()
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
        private static long ModInverse(long a, long m)
        {
            long m0 = m;
            (long x0, long x1) = (0, 1);

            if (m == 1) return 0; // Inverse doesn't exist if mod=1

            while (a > 1)
            {
                long q = a / m;
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
