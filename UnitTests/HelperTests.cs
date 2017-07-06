using Trains.Services;
using Xunit;

namespace UnitTests
{
    public class HelperTests
    {
        private const string _category = "Helper";

        [Fact]
        [Trait("Category", _category)]
        public void ExtractTownsSingle()
        {
            // Arrange // Act
            var output = Helper.ExtractNames("A");

            // Assert
            Assert.IsType<string[]>(output);
            Assert.Equal<string>("A", output[0]);
        }
    }
}