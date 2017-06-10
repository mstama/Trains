using System;
using System.Collections.Generic;
using System.Text;
using Trains.Models;
using Xunit;

namespace UnitTests
{
    public class GraphTests
    {
        private const string _category = "Graph";

        [Fact]
        [Trait("Category", _category)]
        public void CalculateRouteDistanceTest()
        {
            // Arrange
            var graph = BuildGraph();
            // Act
            var output = graph.CalculateRouteDistance("A", "B");

            // Assert
            Assert.Equal<int>(5, output);
        }

        [Fact]
        [Trait("Category", _category)]
        public void CalculateRouteDistanceTest2()
        {
            // Arrange
            var graph = BuildGraph();

            // Act
            var output = graph.CalculateRouteDistance("A", "B","C");

            // Assert
            Assert.Equal<int>(8, output);
        }


        public Graph BuildGraph()
        {
            var graph = new Graph();
            graph.AddRoute("A", "B", 5);
            graph.AddRoute("B", "C", 3);
            return graph;
        }
    }
}
