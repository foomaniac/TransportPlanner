using System;
using System.Collections.Generic;
using System.Text;
using TransportPlanner.Models;

namespace TransportPlanner.Dependencies
{
    /// <summary>
    /// Provides abstraction away from data store
    /// Used lists to reduce dependency on EntityFramework DbSets
    /// </summary>
    public interface ITransportPlannerContext
    {       
        public IList<Port> Ports { get; set; }
        public IList<Route> Routes { get; set; }
        public IList<Journey> Journeys { get; set; }
        public IList<JourneyRoute> JourneyRoutes { get; set; }

    }
}
