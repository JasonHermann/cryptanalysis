using foundation.attacks.Xor;
using foundation.ciphers;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            // This is not asserting any behavior in code in  my project,
            // It is verifying I understand the behavior of the code in .Net Framework
            // This isn't a test as much as example.

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
            // This is not asserting any behavior in code in  my project,
            // It is verifying I understand the behavior of the code in .Net Framework
            // This isn't a test as much as example.

            // Arrange
            var n = "132187956505730920083422024398614652029";
            BigInteger number = BigInteger.Parse(n);

            // Act
            var s = new String([.. number.ToByteArray(isBigEndian: true).Select(b => (char) b)]);

            // Assert
            var expected = "crypto{redacted}";
            Assert.Equal(expected, s);
        }

        [Fact]
        public void Example_XorCiper()
        {
            // Arrange
            var k = 13;
            var cipher = new XorCipher(k);
            var s = "label";

            // Act
            var cipherText = cipher.Encrypt(s);

            // Assert
            var expected = "aloha";
            Assert.Equal(expected, cipherText);
        }

        [Fact]
        public void ReverseXor()
        {
            // Arrange
            var k1 = Convert.FromHexString("a6c8b6733c9b22de7bc0253266a3867df55acde8635e19c73313");
            var k2_k1 = Convert.FromHexString("37dcb292030faa90d07eec17e3b1c6d8daf94c35d4c9191a5e1e");
            var k2_k3 = Convert.FromHexString("c1545756687e7573db23aa1c3452a098b71a7fbf0fddddde5fc1");
            var f_k1_k3_k2 = Convert.FromHexString("04ee9855208a2cd59091d04767ae47963170d1660df7f56f5faf");

            // Act
            var flag = f_k1_k3_k2.Xor(k2_k3).Xor(k1);
            var s = new String([.. flag.Select(b => (char) b)]);

            // Assert
        }

        /// <summary>
        /// Modified version of code from CryptoHack
        /// Actual keys and answers redacted.
        /// </summary>
        [Fact]
        public void FindXorCipherKey()
        {
            // Arrange
            var cipherArray = Convert.FromHexString("63727970746F7B72656461637465647D");
            var cipherText = new String([.. cipherArray.Select(b => (char)b)]);

            // Act
            var attack = new XorCipherAttack("crypto", HintType.StartsWith);
            var key = attack.FindKey(cipherText, maxKeyValue: 1000);

            var cipher = new XorCipher(key);
            var plainText = cipher.Decrypt(cipherText);

            // Assert
            var expected = "crypto{redacted}";
            Assert.Equal(expected, plainText);
        }
    }
}
