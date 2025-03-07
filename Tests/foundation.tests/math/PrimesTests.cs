using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace foundation.tests.math
{
    public class PrimesTests
    {
        [Fact]
        public void GCD_SimpleExample()
        {
            // Arrange
            var a = 1071;
            var b = 462;

            // Act
            var gcd_1 = Primes.GCD(a, b);
            var gcd_2 = Primes.GCD(b, a);

            // Assert
            var expected = 21;
            Assert.Equal(gcd_1, gcd_2);
            Assert.Equal(expected, gcd_1);
        }

        [Fact]
        public void GCD_SimpleExample_2()
        {
            // Arrange
            var a = 17;
            var b = 3;

            // Act
            var gcd_1 = Primes.GCD(a, b);
            var gcd_2 = Primes.GCD(b, a);

            // Assert
            var expected = 1;
            Assert.Equal(gcd_1, gcd_2);
            Assert.Equal(expected, gcd_1);
        }

        [Fact]
        public void GCD_SimpleExample_CoPrime()
        {
            // Arrange
            var a = 17;
            var b = 3;

            // Act
            var coPrime_1 = Primes.AreCoPrime(a, b);
            var coPrime_2 = Primes.AreCoPrime(b, a);

            // Assert
            var expected = true;
            Assert.Equal(coPrime_1, coPrime_2);
            Assert.Equal(expected, coPrime_1);
        }

        [Fact]
        public void GCD_AnotherExample()
        {
            // Arrange
            var a = 66528;
            var b = 52920;

            // Act
            var gcd = Primes.GCD(a, b);

            // Assert
            var expected = 1512;
            Assert.Equal(expected, gcd);
        }

        [Fact]
        public void GCD_ExtendedEuclid_Example1()
        {
            // Arrange
            var a = 240;
            var b = 46;

            // Act
            var (u_1, v_1) = Primes.ExtendedEuclid(a, b);
            var (u_2, v_2) = Primes.ExtendedEuclid(b, a);  

            // Assert
            var expected_u = -9;
            var expected_v = 47;
            Assert.Equal(u_1, v_2); // note the output changes based on the order of (a, b)
            Assert.Equal(u_2, v_1); // note the output changes based on the order of (a, b)

            Assert.Equal(expected_u, u_1);
            Assert.Equal(expected_v, v_1);
        }

        [Fact]
        public void GCD_ExtendedEuclid_Example2()
        {
            // Arrange
            var b = 26513;
            var a = 32321;

            // Act
            var gcd = Primes.GCD(a, b);
            var (u, v) = Primes.ExtendedEuclid(a, b);

            // Assert
            var expected_u = -8404;
            var expected_v = 10245;
            Assert.Equal(expected_u, u);
            Assert.Equal(expected_v, v);
        }

        [Fact]
        public void ModuloArithmetic_Example_1()
        {
            // Arrange
            var a = 11;
            var n = 6;

            // Act
            var b = Primes.Modulo(a, n);

            // Assert
            var expected_b = 5;
            Assert.Equal(expected_b, b);
        }

        [Fact]
        public void ModuloArithmetic_Example_2()
        {
            // Arrange
            var a = 8146798528947;
            var n = 17;

            // Act
            var b = Primes.Modulo(a, n);

            // Assert
            var expected_b = 4;
            Assert.Equal(expected_b, b);
        }

        [Fact]
        public void IsPrime_Example1()
        {
            // Arrange
            var p = 100;

            // Act
            var isPrime = Primes.IsPrime(p);
            var primes = Primes.PrimeNumbers;

            // Assert
            var expected = false;
            var expected_count = 25;
            
            Assert.Equal(expected, isPrime);
            Assert.Equal(expected_count, primes.Count);
        }

        [Fact]
        public void IsPrime_LargeValues()
        {
            // Arrange
            var p = 100000001923; // This is a known prime
            var bigP = new BigInteger(p);

            // Act
            var b = Primes.IsPrime(bigP);

            // Assert
            Assert.True(b);
        }

        [Fact]
        public void IsPrime_RidiculouslyLargeValues()
        {
            // Arrange
            var p_prime     = "101524035174539890485408575671085261788758965189060164484385690801466167356667036677932998889725476582421738788500738738503134356158197247473850273565349249573867251280253564698939768700489401960767007716413932851838937641880157263936985954881657889497583485535527613578457628399173971810541670838543309159139";
            var p_composite = "101524035174539890485408575671085261788758965189060164484385690801466167356667036677932998889725476582421738788500738738503134356158197247473850273565349249573867251280253564698939768700489401960767007716413932851838937641880157263936985954881657889497583485535527613578457628399173971810541670838543309159141";

            var bigP = BigInteger.Parse(p_prime);
            var bigComposite = BigInteger.Parse(p_composite);

            // Act
            var prime_test = Primes.IsPrime(bigP);
            var composite_test = Primes.IsPrime(bigComposite);

            // Assert
            Assert.True(prime_test);
            Assert.False(composite_test);
        }


    }
}
