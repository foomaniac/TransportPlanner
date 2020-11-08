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
    /// Retrieves a Journey and inflates the object with all route information
    /// Depending on chosen persistance model would look to EF to handle relationships or
    /// look to repositories as an added abstraction
    /// </summary>
   public class JourneyQuery : IJourneyQuery
    {
        private readonly ITransportPlannerContext _context;
        public JourneyQuery(ITransportPlannerContext context)
        {
            _context = context;
        }
        public Journey GetJourney(int journeyId)
        {            
            var journey = _context.Journeys.FirstOrDefault(jt => jt.Id == journeyId);

            if(journey == null)
            {
                throw new ArgumentException($"Journey not found for journey id {journeyId}");
            }

            //Inflate journey with routes
            journey.Routes.AddRange(GetRoutes(journeyId));

            return journey;
        }
        private IEnumerable<JourneyRoute> GetRoutes(int journeyId)
        {
            var routesInJourney = new List<JourneyRoute>();

            var availableRoutesInJourney = _context.JourneyRoutes.Where(route => route.JourneyId == journeyId).ToList();

            foreach(var journeyRoute in availableRoutesInJourney)
            {
                var newJourneyRoute = new JourneyRoute(journeyId, journeyRoute.Order, journeyRoute.RouteId)
                {
                    Route = _context.Routes.FirstOrDefault(rt => rt.Id == journeyRoute.RouteId)
                };

                routesInJourney.Add(newJourneyRoute);
            }

            return routesInJourney;
        }

    }
}
