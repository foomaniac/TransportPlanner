using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace TransportPlanner.Models
{
    /// <summary>
    /// Represents a predefined set of routes from start to end
    /// </summary>
    public class Journey
    {
        /// <summary>
        /// Network Groups Journeys 
        /// </summary>
        public int NetworkId { get; private set; }
        public int Order { get; private set; }
        public int RouteId { get; private set; }
        public IList<Route> Routes { get; private set; }

        public Journey(int networkId, int order, int routeId)
        {
            NetworkId = networkId;
            Order = order;
            RouteId = routeId;
        }

        public int TotalJourneyTime()
        {
            return Routes.Sum(rt => rt.DaysDuration);
        }
    }
}
