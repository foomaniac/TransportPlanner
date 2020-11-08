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
        public int JourneyId { get;  }
        public int Order { get; }
        public int RouteId { get; }
        public Route Route { get; set; }

        public JourneyRoute(int journeyId, int order, int routeId)
        {
            JourneyId = journeyId;
            Order = order;
            RouteId = routeId;
        }

    }
}
