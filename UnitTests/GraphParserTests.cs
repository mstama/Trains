// Copyright (c) 2017 Marcos Tamashiro. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

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
            var factory = new GraphFactory();
            var target = new GraphParser(factory);

            // Act // Assert
            Assert.Throws<FormatException>(() => target.Parse("AB"));
        }

        [Fact]
        [Trait("Category", _category)]
        public void ParseEmptyTest()
        {
            // Arrange
            var factory = new GraphFactory();
            var target = new GraphParser(factory);

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
            var factory = new GraphFactory();
            var target = new GraphParser(factory);

            // Act // Assert
            Assert.Throws<IndexOutOfRangeException>(() => target.Parse("A"));
        }

        [Fact]
        [Trait("Category", _category)]
        public void ParseTest()
        {
            // Arrange
            var factory = new GraphFactory();
            var target = new GraphParser(factory);

            // Act
            var output = target.Parse("AB5");

            // Assert
            Assert.IsAssignableFrom<IGraph>(output);
            Assert.NotEmpty(output.Towns);
            Assert.NotEmpty(output.Towns["A"].Routes);
        }
    }
}