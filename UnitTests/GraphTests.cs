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
            var output = graph.AddRoute("X", "Y", 5);

            // Assert
            Assert.NotEmpty(graph.Towns);
            Assert.Contains<Town>(graph.Towns, t => t.Name == "X");
            Assert.Contains<Town>(graph.Towns, t => t.Name == "Y");
            Assert.Contains<Route>(graph.Towns[0].Routes, r => r.Destination.Name == "Y");
            Assert.Equal<int>(2, graph.Towns.Count);
        }

        [Fact]
        [Trait("Category", _category)]
        public void AddRouteRetrieveTest()
        {
            // Arrange
            var graph = new Graph();
            var t1 = graph.AddTown("X");
            var t2 = graph.AddTown("Y");
            // Act
            var output = graph.AddRoute("X", "Y", 5);

            // Assert
            Assert.NotEmpty(graph.Towns);
            Assert.Contains<Town>(graph.Towns, t => t.Name == "X");
            Assert.Contains<Town>(graph.Towns, t => t.Name == "Y");
            Assert.Contains<Route>(graph.Towns[0].Routes, r => r.Destination.Name == "Y");
            Assert.Equal<int>(2, graph.Towns.Count);
        }

        [Fact]
        [Trait("Category", _category)]
        public void AddTownCreateTest()
        {
            // Arrange
            var graph = new Graph();
            // Act
            var output = graph.AddTown("Z");

            // Assert
            Assert.NotEmpty(graph.Towns);
            Assert.Contains<Town>(graph.Towns, t => t.Name == "Z");
        }

        [Fact]
        [Trait("Category", _category)]
        public void AddTownRetrieveTest()
        {
            // Arrange
            var graph = new Graph();
            var t1 = graph.AddTown("Z");
            // Act
            var output = graph.AddTown("Z");

            // Assert
            Assert.NotEmpty(graph.Towns);
            Assert.Contains<Town>(graph.Towns, t => t.Name == "Z");
            Assert.Equal<int>(1, graph.Towns.Count);
        }
    }
}