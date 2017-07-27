// Copyright (c) 2017 Marcos Tamashiro. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

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