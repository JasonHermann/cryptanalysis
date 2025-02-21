namespace foundation.tests
{
    public class SmokeTests
    {
        [Fact]
        public void Passing_Test()
        {
            // Arrange

            // Act

            // Asset
            Assert.Equal(1, 1);
        }

        [Fact]
        public void Failing_Test()
        {
            // Arrange

            // Act

            // Asset
            Assert.Equal(1, 0);
        }
    }
}