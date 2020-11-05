namespace TransportPlanner.Models
{
    public class WayPoint
    {
        public int Id { get; private set; }
        //int Step { get; set; }
        //int RouteId { get; set; }
        public int FromPortId { get; private set; }
        public int ToPortId { get; private set; }
        public int DaysTravelTime { get; private set; }

        public WayPoint(int id, int fromPortId, int toPortId, int daysTravelTime)
        {
            Id = id;
            FromPortId = fromPortId;
            ToPortId = toPortId;
            DaysTravelTime = daysTravelTime;
        }
    }
}