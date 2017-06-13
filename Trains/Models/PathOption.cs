namespace Trains.Models
{
    /// <summary>
    /// Path calculation option
    /// </summary>
    public enum PathOption
    {
        /// <summary> Max stops (<) </summary>
        StopMax = 0,

        /// <summary> Til max stops (<=) </summary>
        StopMaxEqual = 1,

        /// <summary> Equal stops (==) </summary>
        StopEqual = 2,

        /// <summary> Max distance (<) </summary>
        DistanceMax = 3,

        /// <summary> Til max distance (<=) </summary>
        DistanceMaxEqual = 4,

        /// <summary> Equal distance (==) </summary>
        DistanceEqual = 5
    }
}