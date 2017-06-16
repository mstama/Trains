namespace Trains.Models
{
    public partial class GraphWalker
    {
        /// <summary>
        /// Processing class to find all paths
        /// </summary>
        private class MetaTown
        {
            public string Breadcrumb { get; }

            public Town Data { get; }

            public int Stops { get; }

            public int TotalDistance { get; }

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="town"></param>
            /// <param name="stops"></param>
            /// <param name="breadcrumb"></param>
            /// <param name="distance"></param>
            public MetaTown(Town town, int stops, string breadcrumb, int distance)
            {
                Data = town;
                Stops = stops;
                Breadcrumb = string.Format("{0}{1}", breadcrumb, town.Name);
                TotalDistance = distance;
            }

            /// <summary>
            /// Constructor First Town
            /// </summary>
            /// <param name="town"></param>
            /// <param name="stops"></param>
            public MetaTown(Town town, int stops)
            {
                Data = town;
                Stops = stops;
                Breadcrumb = town.Name;
                TotalDistance = 0;
            }
        }
    }
}