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
    }
}
