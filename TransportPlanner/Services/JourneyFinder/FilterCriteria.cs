using System;
using System.Collections.Generic;
using System.Text;

namespace TransportPlanner.Services.JourneyFinder
{
    public class FilterCriteria
    {
        public bool ReturnQuickestRoute { get; }
        public int? ApplyMaximumJourneyTime { get; }
        public int? ApplyMaximumNumberOfStops { get; }
        public int? ApplyNumberOfStops { get; }

        public FilterCriteria(bool returnQuickestRoute, int? applyMaximumJourneyTime, int? applyMaximumNumberOfStops, int? applyNumberOfStops)
        {
            ReturnQuickestRoute = returnQuickestRoute;
            ApplyMaximumJourneyTime = applyMaximumJourneyTime;
            ApplyMaximumNumberOfStops = applyMaximumNumberOfStops;
            ApplyNumberOfStops = applyNumberOfStops;
        }
    }
}
