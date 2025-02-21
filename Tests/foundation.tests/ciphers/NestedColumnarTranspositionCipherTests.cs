using foundation.ciphers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace foundation.tests.ciphers
{
    public class NestedColumnarTranspositionCipherTests
    {
        [Fact]
        public void ColumnarTransposition_ModernCryptanlysis_Example1()
        {
            // Arrange
            var plainText = "abcdefghijklmnopqrstuvwxyz";
            var keys = new List<int>() { 5, 9 };
            //var expectedCipher1 = "afkpu zbglq vchmr wdins xejot y".Replace(" ", "");
            var expectedCipher = "aqnfv skcxp heumj zrobw tgdyl i".Replace(" ", "");

            // Act
            var cipher = new NestedColumnarTransposition(keys);
            var cipherText = cipher.Encrypt(plainText);
            var decryptedText = cipher.Decrypt(cipherText);

            // Assert
            Assert.Equal(expectedCipher, cipherText);
            Assert.Equal(plainText, decryptedText);
        }
    }
}
