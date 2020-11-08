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
        public IList<Port> Ports { get; }
        public IList<Route> Routes { get; }
        public IList<Journey> Journeys { get; }
        public IList<JourneyRoute> JourneyRoutes { get; }

    }
}
