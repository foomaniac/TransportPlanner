using System;
using System.Collections.Generic;
using System.Text;

namespace TransportPlanner.Services.JourneyFinder
{
    public interface IJourneyFinderHandler
    {
        public Response FindJourneys(Request request);
    }
}
