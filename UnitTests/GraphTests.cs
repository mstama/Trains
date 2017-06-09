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
            var townA = new Town("A");
            var townB = new Town("B");
            // Act
            var output = graph.CalculateRouteDistance(townA, townB);

            // Assert
            Assert.Equal<int>(5, output);
        }

        [Fact]
        [Trait("Category", _category)]
        public void CalculateRouteDistanceTest2()
        {
            // Arrange
            var graph = BuildGraph();
            var townA = new Town("A");
            var townB = new Town("B");
            var townC = new Town("C");
            // Act
            var output = graph.CalculateRouteDistance(townA, townB,townC);

            // Assert
            Assert.Equal<int>(8, output);
        }


        public Graph BuildGraph()
        {
            var graph = new Graph();
            var townA = graph.AddTown("A");
            var townB = graph.AddTown("B");
            var townC = graph.AddTown("C");
            graph.AddRoute(townA, townB, 5);
            graph.AddRoute(townB, townC, 3);
            return graph;
        }
    }
}
