using foundation.ciphers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace foundation.tests.ciphers
{
    public class PolyAlphabeticCipherTests
    {
        [Fact]
        public void PolyalphabetCipher_KeySize1_SpecialCaseSubstitutionCipher_HappyPath()
        {
            // Arrange
            var mapping = new Dictionary<char, char>();
            mapping.Add('a', 'b');
            mapping.Add('b', 'c');
            mapping.Add('c', 'd');
            mapping.Add('d', 'a');
            var list = new List<Dictionary<char, char>> { mapping };
            var cipher = new PolyAlphabeticCipher(list);
            var plainText = "feedback";
            var expectedText = "feeacbdk";


            // Act
            var cipherText = cipher.Encrypt(plainText);

            // Assert
            Assert.Equal(expectedText, cipherText);
        }
    }
}
