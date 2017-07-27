// Copyright (c) 2017 Marcos Tamashiro. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Trains.Models;
using Xunit;

namespace UnitTests
{
    public class GraphTests
    {
        private const string _category = "Graph";

        [Fact]
        [Trait("Category", _category)]
        public void AddRouteCreateTest()
        {
            // Arrange
            var graph = new Graph();
            // Act
            graph.AddRoute("X", "Y", 5);

            // Assert
            Assert.NotEmpty(graph.Towns);
            Assert.True(graph.Towns.ContainsKey("X"));
            Assert.True(graph.Towns.ContainsKey("Y"));
            Assert.True(graph.Towns["X"].Routes.ContainsKey("Y"));
            Assert.Equal<int>(2, graph.Towns.Count);
        }

        [Fact]
        [Trait("Category", _category)]
        public void AddRouteRetrieveTest()
        {
            // Arrange
            var graph = new Graph();
            graph.AddTown("X");
            graph.AddTown("Y");
            // Act
            graph.AddRoute("X", "Y", 5);

            // Assert
            Assert.NotEmpty(graph.Towns);
            Assert.True(graph.Towns.ContainsKey("X"));
            Assert.True(graph.Towns.ContainsKey("Y"));
            Assert.True(graph.Towns["X"].Routes.ContainsKey("Y"));
            Assert.Equal<int>(2, graph.Towns.Count);
        }

        [Fact]
        [Trait("Category", _category)]
        public void AddTownCreateTest()
        {
            // Arrange
            var graph = new Graph();
            // Act
            graph.AddTown("Z");

            // Assert
            Assert.True(graph.Towns.ContainsKey("Z"));
        }

        [Fact]
        [Trait("Category", _category)]
        public void AddTownRetrieveTest()
        {
            // Arrange
            var graph = new Graph();
            graph.AddTown("Z");
            // Act
            graph.AddTown("Z");

            // Assert
            Assert.True(graph.Towns.ContainsKey("Z"));
            Assert.Equal<int>(1, graph.Towns.Count);
        }
    }
}