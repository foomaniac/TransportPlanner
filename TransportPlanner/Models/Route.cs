namespace TransportPlanner.Models
{
    /// <summary>
    /// Models a predefined route between two ports. Client expressed that routes are directional.
    /// </summary>
    public class Route
    {
        public int Id { get; private set; }
      
        public int StartPortId { get; private set; }
        public int DestinationPortId { get; private set; }
        public int DaysDuration { get; private set; }

        public Route(int id, int startPortId, int destinationPortId, int daysDuration)
        {
            Id = id;
            StartPortId = startPortId;
            DestinationPortId = destinationPortId;
            DaysDuration = daysDuration;
        }
    }
}