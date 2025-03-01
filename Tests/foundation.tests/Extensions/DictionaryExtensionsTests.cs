using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace foundation.tests.Extensions
{
    public class DictionaryExtensionsTests
    {
        [Fact]
        public void IsBijection_ReturnsFalse_IfDuplicateValue()
        {
            // Arrange
            var d = new Dictionary<int, int>();
            d.Add(1, 1);
            d.Add(2, 2);
            d.Add(3, 1); // this make it not a bijection.

            // Act
            var result = d.IsBijection();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsBijection_ReturnsTrue()
        {
            // Arrange
            var d = new Dictionary<int, int>();
            d.Add(1, 1);
            d.Add(2, 2);
            d.Add(3, 3);

            // Act
            var result = d.IsBijection();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void MSE_IfEqual_Returns0()
        {
            // Arrange
            var a = new Dictionary<string, double>();
            a.Add("a", 1);
            a.Add("b", 2);
            a.Add("c", 3);
            a.Add("d", 4);
            a.Add("e", 5);

            // Act
            var mse = a.MSE(a);

            // Assert
            Assert.Equal(0, mse);
        }

        [Fact]
        public void MSE_ReturnsCorrectValue_SimpleCase()
        {
            // Arrange
            var a = new Dictionary<string, double>();
            a.Add("a", 1);
            a.Add("b", 2);
            a.Add("c", 3);
            a.Add("d", 4);
            a.Add("e", 5);

            var b = new Dictionary<string, double>();
            b.Add("a", 3); //+2
            b.Add("b", 3); //+1
            b.Add("c", 3); //+0
            b.Add("d", 3); //-1
            b.Add("e", 3); //-2

            // Act
            var mse = a.MSE(b);
            var expected = 10; // Sum of squared errors.

            // Assert
            Assert.Equal(expected, mse);
        }

        [Fact]
        public void MSE_ReturnsCorrectValue_MissingValues()
        {
            // Arrange
            var a = new Dictionary<string, double>();
            // a missing 
            a.Add("b", 2);
            a.Add("c", 3);
            a.Add("d", 4);
            a.Add("e", 5);

            var b = new Dictionary<string, double>();
            b.Add("a", 3); //+3 [missing]
            b.Add("b", 3); //+1
            b.Add("c", 3); //+0
            b.Add("d", 3); //-1
            // e missing     +5

            // Act
            var mse = a.MSE(b);
            var expected = 36; // Sum of squared errors.

            // Assert
            Assert.Equal(expected, mse);
        }
    }
}
