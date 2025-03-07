using foundation.math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace foundation.tests.math
{
    public class ModuloTests
    {
        [Fact]
        public void ModuloArithmetic_Example1()
        {
            // Arrange
            var a = new ModuloInteger(3, 13);
            var b = new ModuloInteger(11, 13);

            // Act
            var s = a + b;
            var m = a * b;
            var d = a / b;

            // Assert
            Assert.Equal(1, s.Value);
            Assert.Equal(7, m.Value);
            Assert.Equal(5, d.Value);
        }

        [Fact]
        public void Legendre_Tests()
        {
            // Arrange
            var a = new ModuloInteger(5, 29);
            var b = new ModuloInteger(18, 29);

            // Act
            var l_a = a.LegendreSymbol();
            var l_b = b.LegendreSymbol();

            // Assert
            Assert.Equal(1, l_a);
            Assert.Equal(28, l_b);
        }
    }
}
