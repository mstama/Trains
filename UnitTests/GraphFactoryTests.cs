// Copyright (c) 2017 Marcos Tamashiro. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Trains.Interfaces;
using Trains.Services;
using Xunit;

namespace UnitTests
{
    public class GraphFactoryTests
    {
        private const string _category = "GraphFactory";
        private readonly GraphFactory _target = new GraphFactory();

        [Fact]
        [Trait("Category", _category)]
        public void CreateGraphTest()
        {
            // Act
            var output = _target.CreateGraph();

            // Assert
            Assert.IsAssignableFrom<IGraph>(output);
        }
    }
}