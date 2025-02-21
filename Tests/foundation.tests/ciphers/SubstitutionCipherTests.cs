using foundation.ciphers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace foundation.tests.ciphers
{
    public class SubstitutionCipherTests
    {
        [Fact]
        public void HappyPath()
        {
            // Arrange
            var mapping = new Dictionary<char, char>();
            mapping.Add('a', 'b');
            mapping.Add('b', 'c');
            mapping.Add('c', 'd');
            mapping.Add('d', 'a');
            var cipher = new SubstitutionCipher(mapping);
            var plainText = "feedback";
            var expectedText = "feeacbdk";


            // Act
            var cipherText = cipher.Encrypt(plainText);

            // Assert
            Assert.Equal(expectedText, cipherText);
        }

        [Fact]
        public void HappyPath_RemoveUnmapped()
        {
            // Arrange
            var mapping = new Dictionary<char, char>();
            mapping.Add('a', 'b');
            mapping.Add('b', 'c');
            mapping.Add('c', 'd');
            mapping.Add('d', 'a');
            var cipher = new SubstitutionCipher(mapping, SubstitutionOptions.UnmappedCharacters_Remove);
            var plainText = "feedback";
            var expectedText = "acbd";

            // Act
            var cipherText = cipher.Encrypt(plainText);

            // Assert
            Assert.Equal(expectedText, cipherText);
        }


        [Fact]
        public void HappyPath_ThrowsError()
        {
            // Arrange
            var mapping = new Dictionary<char, char>();
            mapping.Add('a', 'b');
            mapping.Add('b', 'c');
            mapping.Add('c', 'd');
            mapping.Add('d', 'a');
            var cipher = new SubstitutionCipher(mapping, SubstitutionOptions.UnmappedCharacters_ThrowError);
            var plainText = "feedback";

            // Act
            void action()
            {
                var cipherText = cipher.Encrypt(plainText);
            }

            // Assert
            Assert.Throws<NotSupportedException>(action);
        }

    }
}
