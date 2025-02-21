using foundation.Permutations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace foundation.tests.Permutations
{
    public class RotationPermutationTests
    {
        public static List<string> Animals =
            [
                "aardvark",
                "bat",
                "cat",
                "dog",
                "elephant",
                "fox",
                "giraffe",
                "hippo",
                "iguana",
                "jackal",
                "kangaroo",
                "llama"
            ];

        [Fact]
        public void Rotate_List_HappyPath()
        {
            // Arrange
            // Act
            var rotate = RotationPermutation.Rotate(Animals, 2);

            // Assert
            Assert.NotNull(rotate);
            Assert.Equal("kangaroo", rotate[0]);
            Assert.Equal("jackal", rotate.Last());
        }

        [Fact]
        public void Rotate_List_HappyPath_NegativeRotation()
        {
            // Arrange
            // Act
            var rotate = RotationPermutation.Rotate(Animals, -2);

            // Assert
            Assert.NotNull(rotate);
            Assert.Equal("cat", rotate[0]);
            Assert.Equal("bat", rotate.Last());
        }

        [Fact]
        public void Rotate_List_HappyPath_IEnumerable()
        {
            // Arrange
            // Act
            var rotate = RotationPermutation.Rotate(Animals.AsEnumerable(), -2);

            // Assert
            Assert.NotNull(rotate);
            Assert.Equal("cat", rotate.First());
            Assert.Equal("bat", rotate.Last());
        }
    }
}
