using System;
using System.Collections.Generic;
using System.Linq;
using TransportPlanner.Dependencies;
using TransportPlanner.Models;

namespace TransportPlanner.Services.JourneyFinder
{
    /// <summary>
    /// Service will search and return available predefined journeys that match a required start and destination port
    /// Also accepts filter criteria to further limit search
    /// </summary>
    public class JourneyFinderHandler : IJourneyFinderHandler
    {
        private readonly ITransportPlannerContext _context;
        private readonly IJourneyQuery _journeyQuery;

        public JourneyFinderHandler(ITransportPlannerContext context, IJourneyQuery journeyQuery)
        {
            _context = context;
            _journeyQuery = journeyQuery;
        }

        /// <summary>
        /// Journey find searches existing predefined journeys to locate journeys that begin at the specified home port
        /// and at some point arrive at the required destination.
        /// Aware the by simply defining a to b routes, it's possible to introduce algorithms to calculate possible routes
        /// however client specified they have defined networks/journeys
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Response FindJourneys(Request request)
        {
            if (request == null)
            {
                throw new ArgumentNullException($"Invalid request submitted");
            }

            var response = new Response();

            //Taken basic to approach to use linq queries and improve readability
            //Let's get all journeys that start with our home port 
            var routesMatchingStart = from journey in _context.JourneyRoutes
                                      join route in _context.Routes on journey.RouteId equals route.Id
                                      where route.StartPortId == request.StartPortId && journey.Order == 0
                                      select new { journey.JourneyId };

            //Get all routes that have a stop off at our destination
            var routesMatchingEnd = from journey in _context.JourneyRoutes
                                    join route in _context.Routes on journey.RouteId equals route.Id
                                    where route.DestinationPortId == request.DestinationPortId
                                    select new { journey.JourneyId };

            //Filter the available journeys based on ones matching both lists
            var availableJourneyIds = (from journey in _context.JourneyRoutes
                                       join matchingStartRoutes in routesMatchingStart on journey.JourneyId equals matchingStartRoutes.JourneyId
                                       join matchingEndRoutes in routesMatchingEnd on journey.JourneyId equals matchingEndRoutes.JourneyId
                                       select journey.JourneyId).Distinct().ToList();

            //Matches found?
            if (!availableJourneyIds.Any())
            {
                response.FoundMatchingJourneys = false;
            }
            else
            {
                //Hydrate response class with Journey
                foreach (var journeyId in availableJourneyIds)
                {
                    response.FoundMatchingJourneys = true;
                    var journey = _journeyQuery.GetJourney(journeyId);
                    response.Journeys.Add(journey);
                }

                ApplyFilterCriteria(request.FilterCriteria, response);
            }

            return response;
        }

        /// <summary>
        /// Applies set criteria logic to search
        /// </summary>
        /// <param name="filterCriteria"></param>
        /// <param name="response"></param>
        private static void ApplyFilterCriteria(FilterCriteria filterCriteria, Response response)
        {
            if (filterCriteria.ReturnQuickestRoute)
            {
                var quickestJourney = response.Journeys.OrderBy(jr => jr.TotalJourneyTime()).First();
                response.Journeys.Clear();
                response.Journeys.Add(quickestJourney);
            }

            if (filterCriteria.ApplyMaximumJourneyTime.HasValue)
            {
                var journeyUnderMaximumTime = response.Journeys.Where(jr => jr.TotalJourneyTime() <= filterCriteria.ApplyMaximumJourneyTime).ToList();
                response.Journeys.Clear();
                response.Journeys.AddRange(journeyUnderMaximumTime);
            }

            if (filterCriteria.ApplyMaximumNumberOfStops.HasValue)
            {
                var journeysUnderMaximumStops = response.Journeys.Where(jr => jr.Routes.Count <= filterCriteria.ApplyMaximumNumberOfStops).ToList();
                response.Journeys.Clear();
                response.Journeys.AddRange(journeysUnderMaximumStops);
            }

            if (filterCriteria.ApplyNumberOfStops.HasValue)
            {
                var journeysMatchingNumberOfStops = response.Journeys.Where(jr => jr.Routes.Count == filterCriteria.ApplyNumberOfStops).ToList();
                response.Journeys.Clear();
                response.Journeys.AddRange(journeysMatchingNumberOfStops);
            }

        }

    }
}
