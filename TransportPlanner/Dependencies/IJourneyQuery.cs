using System;
using System.Collections.Generic;
using System.Text;
using TransportPlanner.Models;

namespace TransportPlanner.Dependencies
{
   public interface IJourneyQuery
    {
        public Journey GetJourney(int journeyId);
    }
}
