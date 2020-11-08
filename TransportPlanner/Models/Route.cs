namespace TransportPlanner.Models
{
    /// <summary>
    /// Models a predefined route between two ports. Client expressed that routes are directional.
    /// </summary>
    public class Route
    {
        public int Id { get; }

        public int StartPortId { get; }
        public int DestinationPortId { get; }
        public int DaysDuration { get; }

        public Route(int id, int startPortId, int destinationPortId, int daysDuration)
        {
            Id = id;
            StartPortId = startPortId;
            DestinationPortId = destinationPortId;
            DaysDuration = daysDuration;
        }
    }
}