using System;
using System.Collections.Generic;
using System.Text;
using TransportPlanner.Dependencies;
using TransportPlanner.Models;

namespace TransportPlanner.Context
{
    public class TransportPlannerContext : ITransportPlannerContext
    {
        public IList<Port> Ports { get; set; }
        public IList<Route> Routes { get; set; }
        public IList<Journey> Journeys { get; set; }
        public IList<JourneyRoute> JourneyRoutes { get; set; }

    }
}
