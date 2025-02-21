using applications.ciphers;
using foundation.ciphers;
using NuGet.Frameworks;

namespace applications.tests.ciphers
{
    /// <summary>
    /// Example taken from Modern Cryptanalysis 
    /// https://swenson.io/Modern%20Cryptanalysis%20v1.1%202022-01-23.pdf
    /// </summary>
    public class Rot13CipherTests
    {
        [Fact]
        public void Rot13Cipher_ReturnsExampleFromBook_2()
        {
            // Arrange
            var plainText = "the quick brown fox jumps over the lazy dog";
            var cipherTextExpected = "gur dhvpx oebja sbk whzcf bire gur ynml qbt";
            var rot13 = new Rot13Cipher();

            // Act
            var cipherText = rot13.Encrypt(plainText);

            // Assert
            Assert.Equal(cipherTextExpected, cipherText);
        }
    }
}