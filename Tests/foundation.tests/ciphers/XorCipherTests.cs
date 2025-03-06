using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace foundation.tests.ciphers
{
    public class XorCipherTests
    {
        /// <summary>
        /// Modified version of code from CryptoHack
        /// Actual keys and answers redacted.
        /// </summary>
        [Fact]
        public void Example_CharFromHexString()
        {
            // This is not asserting any behavior in code in  my project,
            // It is verifying I understand the behavior of the code in .Net Framework
            // This isn't a test as much as example.

            // Arrange
            var hex = "63727970746F7B72656461637465647D";

            // Act
            var b = Convert.FromHexString(hex);
            var s = new String([.. b.Select(b => (char)b)]);

            // Assert
            var expected = "crypto{redacted}";
            Assert.Equal(expected, s);
        }

        /// <summary>
        /// Modified version of code from CryptoHack
        /// Actual keys and answers redacted.
        /// </summary>
        [Fact]
        public void Example_Base64Encode()
        {
            // Arrange
            var hex = "72BCA9B68FEB79D69CB5E77F";

            // Act
            var b = Convert.FromHexString(hex);
            var s = Convert.ToBase64String(b);

            // Assert
            var expected = "crypto/redacted/";
            Assert.Equal(expected, s);
        }

        /// <summary>
        /// Modified version of code from CryptoHack
        /// Actual keys and answers redacted.
        /// </summary>
        [Fact]
        public void Example_LongInt_Encoding()
        {
            // Arrange
            var n = "132187956505730920083422024398614652029";
            BigInteger number = BigInteger.Parse(n);

            // Act
            var s = new String([.. number.ToByteArray(isBigEndian: true).Select(b => (char) b)]);

            // Assert
            var expected = "crypto{redacted}";
            Assert.Equal(expected, s);


        }

    }
}
