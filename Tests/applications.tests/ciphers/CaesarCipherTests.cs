using applications.ciphers;
using foundation.ciphers;
using foundation.Permutations;
using NuGet.Frameworks;
using System.Collections;

namespace applications.tests.ciphers
{
    /// <summary>
    /// Example taken from Modern Cryptanalysis 
    /// https://swenson.io/Modern%20Cryptanalysis%20v1.1%202022-01-23.pdf
    /// </summary>
    public class CaesarCipherTests
    {
        [Fact]
        public void CaesarCipher_ReturnsExampleFromBook_1()
        {
            // Arrange
            var plainText = "retreat";
            var cipherTextExpected = "uhwuhdw";
            var caesar = new CaesarCipher();

            // Act
            var cipherText = caesar.Encrypt(plainText);

            // Assert
            Assert.Equal(cipherTextExpected, cipherText);
        }

        [Fact]
        public void CaesarCipher_ReturnsExampleFromBook_2()
        {
            // Arrange
            var plainText = "the quick brown fox jumps over the lazy dog";
            var cipherTextExpected = "wkh txlfn eurzq ira mxpsv ryhu wkh odcb grj";
            var caesar = new CaesarCipher();

            // Act
            var cipherText = caesar.Encrypt(plainText);

            // Assert
            Assert.Equal(cipherTextExpected, cipherText);
        }

        [Fact]
        public void CaesarCipher_ReturnsExampleFromBook_3_RemoveSpaces()
        {
            // Arrange
            var plainText = "the quick brown fox jumps over the lazy dog";
            var cipherTextExpected = "wkhtxlfneurzqiramxpsvryhuwkhodcbgrj";
            var plainTextExpected = "thequickbrownfoxjumpsoverthelazydog";
            var caesar = new CaesarCipher(SubstitutionOptions.UnmappedCharacters_Remove);

            // Act
            var cipherText = caesar.Encrypt(plainText);
            var plainTextDecrypted = caesar.Decrypt(cipherText);

            // Assert
            Assert.Equal(cipherTextExpected, cipherText);
            Assert.Equal(plainTextExpected, plainTextDecrypted);
        }
    }
}