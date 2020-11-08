using System;
using System.Collections.Generic;
using System.Text;

namespace TransportPlanner.Services.JourneyFinder
{
    public class Request
    {
        public int StartPortId { get; }

        public int DestinationPortId { get; }

        public FilterCriteria FilterCriteria { get; }

        public Request(int startPortId, int destinationPortId, FilterCriteria filterCriteria)
        {
            StartPortId = startPortId;
            DestinationPortId = destinationPortId;
            FilterCriteria = filterCriteria;
        }
    }
}
