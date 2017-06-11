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

        [Fact]
        [Trait("Category", _category)]
        public void FindRoutesTest()
        {
            // Arrange
            var graph = BuildGraph();
            Func<int,int,bool> depthFunc = (i,j) => i <= j;
            // Act
            var output = graph.FindRoutes("A", "C", 2, depthFunc);

            // Assert

        }

        public Graph BuildGraph()
        {
            var graph = new Graph();
            graph.AddRoute("A", "B", 5);
            graph.AddRoute("B", "C", 3);
            graph.AddRoute("A", "D", 6);
            graph.AddRoute("D", "C", 4);
            return graph;
        }
    }
}
