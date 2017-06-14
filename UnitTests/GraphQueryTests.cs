using Trains.Interfaces;
using Trains.Models;
using Xunit;

namespace UnitTests
{
    public class GraphQueryTests
    {
        private const string _category = "GraphQuery";
        private IGraphQuery _target;

        public GraphQueryTests()
        {
            _target = BuildGraph();
        }

        [Fact]
        [Trait("Category", _category)]
        public void FindPathsDistanceEqualTest()
        {
            // Act
            var output = _target.FindPaths("A", "B", 5, PathOption.DistanceEqual);

            // Assert
            Assert.NotEmpty(output);
            Assert.Equal<int>(1, output.Count);
        }

        [Fact]
        [Trait("Category", _category)]
        public void FindPathsDistanceMaxEqualTest()
        {
            // Act
            var output = _target.FindPaths("A", "B", 5, PathOption.DistanceMaxEqual);

            // Assert
            Assert.NotEmpty(output);
            Assert.Equal<int>(2, output.Count);
        }

        [Fact]
        [Trait("Category", _category)]
        public void FindPathsDistanceMaxTest()
        {
            // Act
            var output = _target.FindPaths("A", "B", 5, PathOption.DistanceMax);

            // Assert
            Assert.NotEmpty(output);
            Assert.Equal<int>(1, output.Count);
        }

        [Fact]
        [Trait("Category", _category)]
        public void FindPathsStopEqualTest()
        {
            // Act
            var output = _target.FindPaths("A", "B", 3, PathOption.StopEqual);

            // Assert
            Assert.NotEmpty(output);
            Assert.Equal<int>(1, output.Count);
        }

        [Fact]
        [Trait("Category", _category)]
        public void FindPathsStopMaxEqualTest()
        {
            // Act
            var output = _target.FindPaths("A", "B", 3, PathOption.StopMaxEqual);

            // Assert
            Assert.NotEmpty(output);
            Assert.Equal<int>(2, output.Count);
        }

        [Fact]
        [Trait("Category", _category)]
        public void FindPathsStopMaxTest()
        {
            // Act
            var output = _target.FindPaths("A", "B", 3, PathOption.StopMax);

            // Assert
            Assert.NotEmpty(output);
            Assert.Equal<int>(1, output.Count);
        }

        [Fact]
        [Trait("Category", _category)]
        public void ShortestPathDistanceTest()
        {
            // Act
            var output = _target.ShortestPathDistance("A", "B");

            // Assert
            Assert.Equal<int>(3, output);
        }

        [Fact]
        [Trait("Category", _category)]
        public void TotalRouteDistance2StopsTest()
        {
            // Act
            var output = _target.TotalRouteDistance("A", "B", "C");

            // Assert
            Assert.Equal<int>(8, output);
        }

        [Fact]
        [Trait("Category", _category)]
        public void TotalRouteDistanceTest()
        {
            // Act
            var output = _target.TotalRouteDistance("A", "B");

            // Assert
            Assert.Equal<int>(5, output);
        }

        [Fact]
        [Trait("Category", _category)]
        public void TotalRouteDistanceErrorTest()
        {
            // Act
            var output = _target.TotalRouteDistance("A", "X");

            // Assert
            Assert.Equal<int>(-1, output);
        }

        private IGraphQuery BuildGraph()
        {
            var graph = new Graph();
            graph.AddRoute("A", "B", 5);
            graph.AddRoute("B", "C", 3);
            graph.AddRoute("A", "D", 1);
            graph.AddRoute("D", "E", 1);
            graph.AddRoute("E", "B", 1);
            return graph;
        }
    }
}