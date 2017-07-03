using Trains.Interfaces;
using Trains.Models;
using Trains.Services;
using Xunit;

namespace UnitTests
{
    public class GraphWalkerTests
    {
        private const string _category = "GraphWalker";
        private IGraph _graph;
        private IGraphWalker _target = new GraphWalker();

        public GraphWalkerTests()
        {
            _graph = BuildGraph();
        }

        [Fact]
        [Trait("Category", _category)]
        public void FindPathsDistanceEqualTest()
        {
            // Act
            var output = _target.FindPaths(_graph, "A", "B", 5, EvalOptions.Distance | EvalOptions.Equal);

            // Assert
            Assert.NotEmpty(output);
            Assert.Equal<int>(1, output.Count);
        }

        [Fact]
        [Trait("Category", _category)]
        public void FindPathsDistanceMaxEqualTest()
        {
            // Act
            var output = _target.FindPaths(_graph, "A", "B", 5, EvalOptions.Distance | EvalOptions.MaxEqual);

            // Assert
            Assert.NotEmpty(output);
            Assert.Equal<int>(2, output.Count);
        }

        [Fact]
        [Trait("Category", _category)]
        public void FindPathsDistanceMaxTest()
        {
            // Act
            var output = _target.FindPaths(_graph, "A", "B", 5, EvalOptions.Distance | EvalOptions.Max);

            // Assert
            Assert.NotEmpty(output);
            Assert.Equal<int>(1, output.Count);
        }

        [Fact]
        [Trait("Category", _category)]
        public void FindPathsStopEqualTest()
        {
            // Act
            var output = _target.FindPaths(_graph, "A", "B", 3, EvalOptions.Stop | EvalOptions.Equal);

            // Assert
            Assert.NotEmpty(output);
            Assert.Equal<int>(1, output.Count);
        }

        [Fact]
        [Trait("Category", _category)]
        public void FindPathsStopMaxEqualTest()
        {
            // Act
            var output = _target.FindPaths(_graph, "A", "B", 3, EvalOptions.Stop | EvalOptions.MaxEqual);

            // Assert
            Assert.NotEmpty(output);
            Assert.Equal<int>(2, output.Count);
        }

        [Fact]
        [Trait("Category", _category)]
        public void FindPathsStopMaxTest()
        {
            // Act
            var output = _target.FindPaths(_graph, "A", "B", 3, EvalOptions.Stop | EvalOptions.Max);

            // Assert
            Assert.NotEmpty(output);
            Assert.Equal<int>(1, output.Count);
        }

        [Fact]
        [Trait("Category", _category)]
        public void ShortestPathDistanceTest()
        {
            // Act
            var output = _target.ShortestPathDistance(_graph, "A", "B");

            // Assert
            Assert.Equal<int>(3, output);
        }

        [Fact]
        [Trait("Category", _category)]
        public void TotalRouteDistance2StopsTest()
        {
            // Act
            var output = _target.TotalRouteDistance(_graph, "A", "B", "C");

            // Assert
            Assert.Equal<int>(8, output);
        }

        [Fact]
        [Trait("Category", _category)]
        public void TotalRouteDistanceErrorTest()
        {
            // Act
            var output = _target.TotalRouteDistance(_graph, "A", "X");

            // Assert
            Assert.Equal<int>(-1, output);
        }

        [Fact]
        [Trait("Category", _category)]
        public void TotalRouteDistanceTest()
        {
            // Act
            var output = _target.TotalRouteDistance(_graph, "A", "B");

            // Assert
            Assert.Equal<int>(5, output);
        }

        private IGraph BuildGraph()
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