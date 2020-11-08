using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransportPlanner.Dependencies;
using TransportPlanner.Models;
using TransportPlanner.Queries;

namespace TransportPlanner.Services
{
    /// <summary>
    /// Service will search and return available predefined journeys that match a required start and destination port
    /// </summary>
    public class JourneyFinder
    {
        private ITransportPlannerContext _context;
        private IJourneyQuery _journeyQuery;

        public JourneyFinder(ITransportPlannerContext context, IJourneyQuery journeyQuery)
        {
            _context = context;
            _journeyQuery = journeyQuery;
        }

        public Response FindJourneys(Request request)
        {
            var response = new Response();

            //TODO: Clean up linq usage and streamline

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
                                       select journey.JourneyId).Distinct();

            if (!availableJourneyIds.Any())
            {
                response.FoundMatchingJourneys = false;
                return response;
            }

            foreach (var journeyId in availableJourneyIds)
            {
                response.FoundMatchingJourneys = true;
                var journey = _journeyQuery.GetJourney(journeyId);
                response.Journeys.Add(journey);
            }

            ApplyFilterCriteria(request.FilterCriteria, response);

            return response;
        }

        private void ApplyFilterCriteria(FilterCriteria filterCriteria, Response response)
        {
            if (filterCriteria.ReturnQuickestRoute)
            {
                var quickestJourney = response.Journeys.OrderBy(jr => jr.TotalJourneyTime()).First();
                response.Journeys.Clear();
                response.Journeys.Add(quickestJourney);
            }

            if (filterCriteria.ApplyMaximumJourneyTime.HasValue)
            {
                var quickestJourney = response.Journeys.OrderBy(jr => jr.TotalJourneyTime() <= filterCriteria.ApplyMaximumJourneyTime).First();
                response.Journeys.Clear();
                response.Journeys.Add(quickestJourney);
            }

            if (filterCriteria.ApplyMaximumNumberOfStops.HasValue)
            {
                var quickestJourney = response.Journeys.OrderBy(jr => jr.Routes.Count() <= filterCriteria.ApplyMaximumNumberOfStops).First();
                response.Journeys.Clear();
                response.Journeys.Add(quickestJourney);
            }

            if (filterCriteria.ApplyNumberOfStops.HasValue)
            {
                var quickestJourney = response.Journeys.OrderBy(jr => jr.Routes.Count() == filterCriteria.ApplyNumberOfStops).First();
                response.Journeys.Clear();
                response.Journeys.Add(quickestJourney);
            }
        }

        public class Request
        {
            public int StartPortId { get; set; }

            public int DestinationPortId { get; set; }

            public FilterCriteria FilterCriteria { get; set; }

            public Request()
            {
                FilterCriteria = new FilterCriteria(false, null, null, null);
            }
        }

        public class FilterCriteria
        {
            public bool ReturnQuickestRoute { get; set; }
            public int? ApplyMaximumJourneyTime { get; set; }
            public int? ApplyMaximumNumberOfStops { get; set; }
            public int? ApplyNumberOfStops { get; set; }

            public FilterCriteria(bool returnQuickestRoute, int? applyMaximumJourneyTime, int? applyMaximumNumberOfStops, int? applyNumberOfStops)
            {
                ReturnQuickestRoute = returnQuickestRoute;
                ApplyMaximumJourneyTime = applyMaximumJourneyTime;
                ApplyMaximumNumberOfStops = applyMaximumNumberOfStops;
                ApplyNumberOfStops = applyNumberOfStops;
            }
        }

        public class Response
        {
            public bool FoundMatchingJourneys { get; set; }
            public List<Journey> Journeys { get; set; }

            public Response()
            {
                Journeys = new List<Journey>();
            }
        }
    }
}
