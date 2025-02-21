using foundation.Permutations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace foundation.tests.Permutations
{
    public class KeyedPermutationTests
    {
        const string alphabet = "abcdefghijklmnopqrstuvwxyz";

        [Fact]
        public void KeyedPermutation_HappyPath()
        {
            // Arrange
            var key = "zebra";
            var expectedPermutation = "zebracdfghijklmnopqstuvwxy";

            // Act
            string permutation = new([.. KeyedPermutation.PermuteByKey(alphabet, key)]);

            // Assert
            Assert.Equal(expectedPermutation, permutation);
        }

        /// <summary>
        /// From 1.2.1 Keyed Alphabets of Modern Cryptanalysis
        /// </summary>
        [Fact]
        public void KeyedPermutation_HappyPath_2()
        {
            // Arrange
            var key = "swordfish";
            var expectedPermutation = "swordfihabcegjklmnpqtuvxyz";

            // Act
            string permutation = new([.. KeyedPermutation.PermuteByKey(alphabet, key)]);

            // Assert
            Assert.Equal(expectedPermutation, permutation);
        }
    }
}
