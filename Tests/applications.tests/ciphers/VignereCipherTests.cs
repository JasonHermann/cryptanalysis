using applications.ciphers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace applications.tests.ciphers
{
    public class VignereCipherTests
    {
        /// <summary>
        /// Example taken from https://en.wikipedia.org/wiki/Vigen%C3%A8re_cipher
        /// </summary>
        [Fact]
        public void VignereCipher_WikipediaExample_1()
        {
            // Arrange
            var plainText = "attacking tonight";
            var key = "oculorhinolaryngology";
            var expectedCipher = "ovnlqbpvt eoegtnh";

            // Act
            var vigenère = VigenèreCipherGenerator.MakeVigenèreCipherFromKey(key);
            var cipherText = vigenère.Encrypt(plainText);
            var decryptedCipherText = vigenère.Decrypt(cipherText);

            // Assert
            Assert.Equal(expectedCipher, cipherText);
            Assert.Equal(plainText, decryptedCipherText);
        }

        /// <summary>
        /// Example taken from https://en.wikipedia.org/wiki/Vigen%C3%A8re_cipher
        /// </summary>
        [Fact]
        public void VignereCipher_WikipediaExample_2()
        {
            // Arrange
            var plainText = "attackatdawn";
            var key = "lemon";
            var expectedCipher = "lxfopvefrnhr";

            // Act
            var vigenère = VigenèreCipherGenerator.MakeVigenèreCipherFromKey(key);
            var cipherText = vigenère.Encrypt(plainText);
            var decryptedCipherText = vigenère.Decrypt(cipherText);

            // Assert
            Assert.Equal(expectedCipher, cipherText);
            Assert.Equal(plainText, decryptedCipherText);
        }

        /// <summary>
        /// Example taken from Modern Cryptanalysis 
        /// https://swenson.io/Modern%20Cryptanalysis%20v1.1%202022-01-23.pdf
        /// </summary>
        [Fact]
        public void VignereCipher_ModernCryptanalysisExample_1()
        {
            // Arrange
            var plainText = "the quick brown fox jumps over the lazy dog";
            var key = "caesar";
            var expectedCipher = "vhi iuzek fjonp fsp jlopw gvvt tlw lrby hgg";

            // Act
            var vigenère = VigenèreCipherGenerator.MakeVigenèreCipherFromKey(key, 
                foundation.ciphers.SubstitutionOptions.UnmappedCharacters_LeaveAsIs | foundation.ciphers.SubstitutionOptions.UnmappedCharacters_DoesNotAdvanceAlphabet);
            var cipherText = vigenère.Encrypt(plainText);
            var decryptedCipherText = vigenère.Decrypt(cipherText);

            // Assert
            Assert.Equal(expectedCipher, cipherText);
            Assert.Equal(plainText, decryptedCipherText);
        }
    }
}
