using foundation.ciphers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace foundation.tests.ciphers
{
    public class ColumnarTranspositionCipherTests
    {
        [Fact]
        public void ColumnarTransposition_HappyPath()
        {
            // Arrange
            var plainText = "hello world";
            var key = 100;
            var expectedCipher = "hello world";

            // Act
            var cipher = new ColumnarTranspositionCipher(key);
            var cipherText = cipher.Encrypt(plainText);
            var decryptedText = cipher.Decrypt(cipherText);

            // Assert
            Assert.Equal(expectedCipher, cipherText);
            Assert.Equal(plainText, decryptedText);
        }

        [Fact]
        public void ColumnarTransposition_ModernCryptanlysis_Example1()
        {
            // Arrange
            var plainText = "all work and no play makes johnny a dull boy";
            plainText = plainText.Replace(" ", ""); // Remove spaces
            var key = 6;
            var expectedCipher = "akpknllalenllnasybwdyjaoonmodyroahu";

            // Act
            var cipher = new ColumnarTranspositionCipher(key);
            var cipherText = cipher.Encrypt(plainText);
            var decryptedText = cipher.Decrypt(cipherText);

            // Assert
            Assert.Equal(expectedCipher, cipherText);
            Assert.Equal(plainText, decryptedText);
        }
    }
}
