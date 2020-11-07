using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using TransportPlanner.Tests.Fixtures;
using Xunit;
using Xunit.Sdk;

namespace TransportPlanner.Tests
{
    /// <summary>
    /// Find the shortest journey time for the following routes:
    /// Route 1.  Buenos Aires  Liverpool
    /// Route 2.  New York  New York
    /// </summary>
    public partial class CalculateShortestJourneyTimeTests : IClassFixture<RoutePlannerFixture>
    {
        RoutePlannerFixture _fixture;
        public CalculateShortestJourneyTimeTests(RoutePlannerFixture fixture)
        {
            _fixture = fixture;
        }

        /// <summary>
        /// Route 1.  Buenos Aires  Liverpool
        /// </summary>
        [Fact]
        public void Calling_Calculate_Shortest_Journey_Time_For_Route_1_Returns_8_days()
        {
            var expectedJourneyTime = 8;
            var journeyTimeReturned = 0;

            var homePortId = (int)RoutePlannerFixture.PortsIds.BuenosAires;
            var destinationPortId = (int)RoutePlannerFixture.PortsIds.Liverpool;


            //Let's get all journeys that start with our home port 
            var routesMatchingStart = from journey in _fixture.Journeys
                                      join route in _fixture.Routes on journey.RouteId equals route.Id
                                      where route.StartPortId == homePortId && journey.Order == 0
                                      select new { journey.NetworkId };

            //Get all routes that have a stop off at our destination
            var routesMatchingEnd = from journey in _fixture.Journeys
                                    join route in _fixture.Routes on journey.RouteId equals route.Id
                                    where route.DestinationPortId == destinationPortId
                                    select new { journey.NetworkId };


            var availableJourneysWithDuration = (from journey in _fixture.Journeys
                                                 join matchingStartRoutes in routesMatchingStart on journey.NetworkId equals matchingStartRoutes.NetworkId
                                                 join matchingEndRoutes in routesMatchingEnd on journey.NetworkId equals matchingEndRoutes.NetworkId
                                                 join route in _fixture.Routes on journey.RouteId equals route.Id
                                                 orderby journey.NetworkId, journey.Order
                                                 select new { journey.NetworkId, route.DaysDuration }).ToList();


            var quickestJourneyTime = from matchingRoutes in availableJourneysWithDuration
                                      group matchingRoutes by matchingRoutes.NetworkId into routeJourneyTimes
                                      orderby routeJourneyTimes.Sum(rjt => rjt.DaysDuration)
                                      select new { JourneyTime = routeJourneyTimes.Sum(rjt => rjt.DaysDuration) };


            journeyTimeReturned = quickestJourneyTime.First().JourneyTime;

            Assert.Equal(expectedJourneyTime, journeyTimeReturned);
        }

        /// <summary>
        /// Route 2.  New York  New York
        /// </summary>
        [Fact]
        public void Calling_Calculate_Shortest_Journey_Time_For_Route_2_Returns_18_days()
        {
            var expectedJourneyTime = 18;
            var journeyTimeReturned = 0;

            var homePortId = (int)RoutePlannerFixture.PortsIds.NewYork;
            var destinationPortId = (int)RoutePlannerFixture.PortsIds.NewYork;


            //Let's get all journeys that start with our home port 
            var routesMatchingStart = from journey in _fixture.Journeys
                                      join route in _fixture.Routes on journey.RouteId equals route.Id
                                      where route.StartPortId == homePortId && journey.Order == 0
                                      select new { journey.NetworkId };

            //Get all routes that have a stop off at our destination
            var routesMatchingEnd = from journey in _fixture.Journeys
                                    join route in _fixture.Routes on journey.RouteId equals route.Id
                                    where route.DestinationPortId == destinationPortId
                                    select new { journey.NetworkId };

            //Filter the available journeys based on ones matching both lists
            //selecting the journey network Id and Duration
            var availableJourneysWithRoutes = (from journey in _fixture.Journeys
                                               join matchingStartRoutes in routesMatchingStart on journey.NetworkId equals matchingStartRoutes.NetworkId
                                               join matchingEndRoutes in routesMatchingEnd on journey.NetworkId equals matchingEndRoutes.NetworkId
                                               join route in _fixture.Routes on journey.RouteId equals route.Id
                                               orderby journey.NetworkId, journey.Order
                                               select new { journey.NetworkId, route.DaysDuration }).ToList();


            var quickestJourneyTime = from matchingRoutes in availableJourneysWithRoutes
                                      group matchingRoutes by matchingRoutes.NetworkId into routeJourneyTimes
                                      orderby routeJourneyTimes.Sum(rjt => rjt.DaysDuration)
                                      select new { JourneyTime = routeJourneyTimes.Sum(rjt => rjt.DaysDuration) };


            journeyTimeReturned = quickestJourneyTime.First().JourneyTime;

            Assert.Equal(expectedJourneyTime, journeyTimeReturned);
        }

    }
}
