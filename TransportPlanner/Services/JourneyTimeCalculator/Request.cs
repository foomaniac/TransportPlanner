using System.Collections.Generic;

namespace TransportPlanner.Services.JourneyTimeCalculator
{
    public class Request
    {
        public IList<RequestRoute> Routes { get;  }
        public Request()
        {
            Routes = new List<RequestRoute>();
        }
    }
    public class RequestRoute
    {
        public RequestRoute(int startPortId, int destinationPortId)
        {
            StartPortId = startPortId;
            DestinationPortId = destinationPortId;
        }

        public int StartPortId { get; set; }

        public int DestinationPortId { get; set; }
    }

}
