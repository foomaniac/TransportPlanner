using System;
using System.Collections.Generic;
using System.Text;
using TransportPlanner.Dependencies;
using TransportPlanner.Models;

namespace TransportPlanner.Context
{
    /// <summary>
    /// Temporary Context to provide in memory data sets. Would swap out depending on choice of persistance layer
    /// </summary>
    public class TransportPlannerContext : ITransportPlannerContext
    {
        public IList<Port> Ports { get; }
        public IList<Route> Routes { get;  }
        public IList<Journey> Journeys { get;  }
        public IList<JourneyRoute> JourneyRoutes { get; }

        public TransportPlannerContext()
        {
            Ports = new List<Port>();
            Routes = new List<Route>();
            Journeys = new List<Journey>();
            JourneyRoutes = new List<JourneyRoute>();
        }
    }
}
