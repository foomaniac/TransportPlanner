using System;
using System.Linq;
using TransportPlanner.Tests.Fixtures;
using Xunit;

namespace TransportPlanner.Tests
{
    /// <summary>
    /// Find the number of routes matching the scenarios below
    /// Route 1. Find the number of routes from Liverpool to Liverpool with a maximum number of 3 stops.
    /// Route 2. Find the number of routes from Buenos Aires to Liverpool where exactly 4 stops are made.
    /// Route 3. Find the number of routes from Liverpool to Liverpool where the journey time is less than or equal to 25 days.
    /// </summary>
    public partial class RouteFinderTests : IClassFixture<RoutePlannerFixture>
    {
        RoutePlannerFixture _fixture;
        public RouteFinderTests(RoutePlannerFixture fixture)
        {
            _fixture = fixture;
        }

        /// <summary>
        /// Route 1. Find the number of routes from Liverpool to Liverpool with a maximum number of 3 stops.
        /// </summary>
        [Fact]
        public void Calling_Find_Routes_Matching_Scenario_1_Returns_Correct_Number_Of_Matches()
        {                        
            var maxiumNumberOfRoutes = 2;
            var expectedNumberOfRoutesWith3Steps = 1;
            var calculatedNumberOfRoutesWith3Steps = 0;

            var homePortId = (int)RoutePlannerFixture.PortsIds.Liverpool;
            var destinationPortId = (int)RoutePlannerFixture.PortsIds.Liverpool;

            //Let's get all journeys that start with our home port 
            var routesMatchingStart = from journey in _fixture._context.Journeys
                                      join route in _fixture._context.Routes on journey.RouteId equals route.Id
                                      where route.StartPortId == homePortId && journey.Order == 0
                                      select new { journey.NetworkId };

            //Get all routes that have a stop off at our destination
            var routesMatchingEnd = from journey in _fixture._context.Journeys
                                    join route in _fixture._context.Routes on journey.RouteId equals route.Id
                                    where route.DestinationPortId == destinationPortId
                                    select new { journey.NetworkId };

            //FInd all matching journeys with number of steps
            var availableJourneysWith3Stops = (from journey in _fixture._context.Journeys
                                               join matchingStartRoutes in routesMatchingStart on journey.NetworkId equals matchingStartRoutes.NetworkId
                                               join matchingEndRoutes in routesMatchingEnd on journey.NetworkId equals matchingEndRoutes.NetworkId
                                               join route in _fixture._context.Routes on journey.RouteId equals route.Id
                                               group journey by journey.NetworkId into journeyRoutes
                                               select new { journeyRoutes.Key, stopsInRoute = journeyRoutes.Select(route => route.RouteId).Distinct().Count() }).ToList();
            
            calculatedNumberOfRoutesWith3Steps = availableJourneysWith3Stops.Where(journey => journey.stopsInRoute <= maxiumNumberOfRoutes).Count();

            Assert.Equal(expectedNumberOfRoutesWith3Steps, calculatedNumberOfRoutesWith3Steps);
        }

        /// <summary>
        /// Route 2. Find the number of routes from Buenos Aires to Liverpool where exactly 4 stops are made.
        /// </summary>
        [Fact]
        public void Calling_Find_Routes_Matching_Scenario_2_Returns_Correct_Number_Of_Matches()
        {
            var requiredNumberOfStops = 3;
            var expectedNumberOfRoutesWith4Steps = 1;
            var calculatedNumberOfRoutesWith4Steps = 0;

            var homePortId = (int)RoutePlannerFixture.PortsIds.BuenosAires;
            var destinationPortId = (int)RoutePlannerFixture.PortsIds.Liverpool;


            //Let's get all journeys that start with our home port 
            var routesMatchingStart = from journey in _fixture._context.Journeys
                                      join route in _fixture._context.Routes on journey.RouteId equals route.Id
                                      where route.StartPortId == homePortId && journey.Order == 0
                                      select new { journey.NetworkId };

            //Get all routes that have a stop off at our destination
            var routesMatchingEnd = from journey in _fixture._context.Journeys
                                    join route in _fixture._context.Routes on journey.RouteId equals route.Id
                                    where route.DestinationPortId == destinationPortId
                                    select new { journey.NetworkId };

            //FInd all matching journeys with number of steps
            var availableJourneysWith3Stops = (from journey in _fixture._context.Journeys
                                               join matchingStartRoutes in routesMatchingStart on journey.NetworkId equals matchingStartRoutes.NetworkId
                                               join matchingEndRoutes in routesMatchingEnd on journey.NetworkId equals matchingEndRoutes.NetworkId
                                               join route in _fixture._context.Routes on journey.RouteId equals route.Id
                                               group journey by journey.NetworkId into journeyRoutes
                                               select new { journeyRoutes.Key, stopsInRoute = journeyRoutes.Select(route => route.RouteId).Distinct().Count() }).ToList();

            calculatedNumberOfRoutesWith4Steps = availableJourneysWith3Stops.Where(journey => journey.stopsInRoute == requiredNumberOfStops).Count();

            Assert.Equal(expectedNumberOfRoutesWith4Steps, calculatedNumberOfRoutesWith4Steps);
        }

        /// <summary>
        /// Route 3. Find the number of routes from Liverpool to Liverpool where the journey time is less than or equal to 25 days.
        /// </summary>
        [Fact]
        public void Calling_Find_Routes_Matching_Scenario_3_Returns_Correct_Number_Of_Matches()
        {
            var maximumJourneyTime = 25;
            var expectedNumberOfRoutesWith3Steps = 2;
            var calculatedNumberOfRoutesLessThan25Days = 0;

            var homePortId = (int)RoutePlannerFixture.PortsIds.Liverpool;
            var destinationPortId = (int)RoutePlannerFixture.PortsIds.Liverpool;


            //Let's get all journeys that start with our home port 
            var routesMatchingStart = from journey in _fixture._context.Journeys
                                      join route in _fixture._context.Routes on journey.RouteId equals route.Id
                                      where route.StartPortId == homePortId && journey.Order == 0
                                      select new { journey.NetworkId };

            //Get all routes that have a stop off at our destination
            var routesMatchingEnd = from journey in _fixture._context.Journeys
                                    join route in _fixture._context.Routes on journey.RouteId equals route.Id
                                    where route.DestinationPortId == destinationPortId
                                    select new { journey.NetworkId };

            //Filter the available journeys based on ones matching both lists
            //selecting the journey network Id and Duration
            var availableJourneysWithRoutes = (from journey in _fixture._context.Journeys
                                               join matchingStartRoutes in routesMatchingStart on journey.NetworkId equals matchingStartRoutes.NetworkId
                                               join matchingEndRoutes in routesMatchingEnd on journey.NetworkId equals matchingEndRoutes.NetworkId
                                               join route in _fixture._context.Routes on journey.RouteId equals route.Id
                                               orderby journey.NetworkId, journey.Order
                                               select new { journey.NetworkId, route.DaysDuration }).ToList();

            //Get total journey times for routes
            var journeyTimes = from matchingRoutes in availableJourneysWithRoutes
                                      group matchingRoutes by matchingRoutes.NetworkId into routeJourneyTimes
                                      orderby routeJourneyTimes.Sum(rjt => rjt.DaysDuration)
                                      select new { routeJourneyTimes.Key, JourneyTime = routeJourneyTimes.Sum(rjt => rjt.DaysDuration)}                                     ;

            calculatedNumberOfRoutesLessThan25Days = journeyTimes.Where(jt => jt.JourneyTime <= maximumJourneyTime).Select(jt => jt.Key).Distinct().Count();
            
            Assert.Equal(expectedNumberOfRoutesWith3Steps, calculatedNumberOfRoutesLessThan25Days);
        }

    }
}