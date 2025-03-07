using foundation.math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace foundation.tests.math
{
    public class TonelliShanksTests
    {
        [Fact]
        public void HappyPath()
        {
            // Arrange
            var p = 29;
            var n = 5;

            // Act
            var (r1, r2) = TonelliShanks.Algorithm(n, p);

            // Assert
            Assert.Equal(11, r1);
            Assert.Equal(18, r2);
        }

        [Fact]
        public void FindRoots_IfTheyExist()
        {
            // Arrange
            var p = 29;

            // Act
            var r6 = TonelliShanks.Algorithm(6, p);
            var r11 = TonelliShanks.Algorithm(11, p);
            var r14 = TonelliShanks.Algorithm(14, p);

            // Assert
            Assert.Equal((8,21), r6);
            Assert.Equal((0, 0), r11);
            Assert.Equal((0, 0), r14);
        }
    }
}
