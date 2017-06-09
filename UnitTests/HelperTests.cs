using System;
using System.Collections.Generic;
using Trains.Models;
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
            // Arrange
            string input = "A";

            // Act
            var output = Helper.ExtractTowns(input);

            // Assert
            Assert.Contains<Town>(output, t => t.Name == "A");
        }
    }
}
