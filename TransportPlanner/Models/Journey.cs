using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TransportPlanner.Models
{
    /// <summary>
    /// Represents a set of predefined routes on a directional journey between ports.
    /// </summary>
   public class Journey
    {
        public int Id;

        public string Name { get; private set; }

        public IList<JourneyRoute> Routes { get; set; }

        public Journey(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int TotalJourneyTime()
        {
            return Routes.Sum(rt => rt.Route.DaysDuration);
        }
    }
}
