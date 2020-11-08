using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace TransportPlanner.Models
{
    /// <summary>
    /// Represents a route as step as part of a predefined Journey
    /// </summary>
    public class JourneyRoute
    {
        /// <summary>
        /// Network Groups Journeys 
        /// </summary>        
        public int JourneyId;
        public int Order { get; private set; }
        public int RouteId { get; private set; }
        public Route Route { get; set; }

        public JourneyRoute(int journeyId, int order, int routeId)
        {
            JourneyId = journeyId;
            Order = order;
            RouteId = routeId;
        }

    }
}
