using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using TransportPlanner.Dependencies;
using TransportPlanner.Models;

namespace TransportPlanner.Queries
{
    /// <summary>
    /// Retrives a Journey and inflates the object with all route information
    /// </summary>
   public class JourneyQuery : IJourneyQuery
    {
        private ITransportPlannerContext _context;
        public JourneyQuery(ITransportPlannerContext context)
        {
            _context = context;
        }
        public Journey GetJourney(int journeyId)
        {            
            var journey = _context.Journeys.Where(jt => jt.Id == journeyId).FirstOrDefault();

            if(journey == null)
            {
                throw new ApplicationException($"Journey not found for journey id {journeyId}");
            }

            //Inflate journey with routes
            journey.Routes = GetRoutes(journeyId);

            return journey;
        }
        private IList<JourneyRoute> GetRoutes(int journeyId)
        {
            var routesInJourney = new List<JourneyRoute>();

            var availableRoutesInJourney = _context.JourneyRoutes.Where(route => route.JourneyId == journeyId);

            foreach(var journeyRoute in availableRoutesInJourney)
            {
                var newJourneyRoute = new JourneyRoute(journeyId, journeyRoute.Order, journeyRoute.RouteId);
                newJourneyRoute.Route = _context.Routes.FirstOrDefault(rt => rt.Id == journeyRoute.RouteId);

                routesInJourney.Add(newJourneyRoute);
            }

            return routesInJourney;
        }

    }
}
