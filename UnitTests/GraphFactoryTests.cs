﻿using Trains.Interfaces;
using Trains.Services;
using Xunit;

namespace UnitTests
{
    public class GraphFactoryTests
    {
        private const string _category = "GraphFactory";
        private GraphFactory _target = new GraphFactory();

        [Fact]
        [Trait("Category", _category)]
        public void CreateGraphQueryTest()
        {
            // Arrange
            var graph = _target.CreateGraph();

            // Act
            var output = _target.RetrieveGraph(graph);

            // Assert
            Assert.IsAssignableFrom<IGraphQuery>(output);
        }

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