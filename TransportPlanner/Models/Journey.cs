using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TransportPlanner.Models
{
    /// <summary>
    /// Represents a set of predefined routes on a directional journey between ports.
    /// Since the client describes a network of predefined routes, decision taken to
    /// provision ability to define set journeys that have a network of routes.
    /// Gives client option to add new journeys/networks in the future.
    /// </summary>
   public class Journey
    {
        public int Id { get; }

        public string Name { get; }

        public List<JourneyRoute> Routes { get; }

        public Journey(int id, string name)
        {
            Id = id;
            Name = name;
            Routes = new List<JourneyRoute>();
        }
        
        public int TotalJourneyTime()
        {
            return Routes.Sum(rt => rt.Route.DaysDuration);
        }
    }
}
