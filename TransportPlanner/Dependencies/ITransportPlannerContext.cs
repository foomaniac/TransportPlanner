using System;
using System.Collections.Generic;
using System.Text;
using TransportPlanner.Models;

namespace TransportPlanner.Dependencies
{
    public interface ITransportPlannerContext
    {
        public IList<Port> Ports { get; set; }
        public IList<Route> Routes { get; set; }
        public IList<Journey> Journeys { get; set; }

    }
}
