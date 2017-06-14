using System;
using Trains.Interfaces;
using Trains.Services;
using Xunit;

namespace UnitTests
{
    public class GraphParserTests
    {
        private const string _category = "GraphParser";

        [Fact]
        [Trait("Category", _category)]
        public void ParseDoubleTest()
        {
            // Arrange
            GraphFactory factory = new GraphFactory();
            GraphParser target = new GraphParser(factory);

            // Act // Assert
            Assert.Throws<FormatException>(() => target.Parse("AB"));
        }

        [Fact]
        [Trait("Category", _category)]
        public void ParseEmptyTest()
        {
            // Arrange
            GraphFactory factory = new GraphFactory();
            GraphParser target = new GraphParser(factory);

            // Act
            var output = target.Parse("");

            // Assert
            Assert.IsAssignableFrom<IGraph>(output);
            Assert.Empty(output.Towns);
        }

        [Fact]
        [Trait("Category", _category)]
        public void ParseSingleTest()
        {
            // Arrange
            GraphFactory factory = new GraphFactory();
            GraphParser target = new GraphParser(factory);

            // Act // Assert
            Assert.Throws<IndexOutOfRangeException>(() => target.Parse("A"));
        }

        [Fact]
        [Trait("Category", _category)]
        public void ParseTest()
        {
            // Arrange
            GraphFactory factory = new GraphFactory();
            GraphParser target = new GraphParser(factory);

            // Act
            var output = target.Parse("AB5");

            // Assert
            Assert.IsAssignableFrom<IGraph>(output);
            Assert.NotEmpty(output.Towns);
            Assert.NotEmpty(output.Towns[0].Routes);
        }
    }
}