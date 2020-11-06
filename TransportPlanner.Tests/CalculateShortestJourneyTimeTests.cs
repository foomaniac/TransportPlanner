using System;
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
        public void Calling_Calculate_Shortest_Journey_Time_For_Route_1_Returns_X_days()
        {
            var expectedJourneyTime = 8;
            var journeyTimeReturned = 0;

            var homePortId = (int)RoutePlannerFixture.PortsIds.BuenosAires;
            var destinationPortId = (int)RoutePlannerFixture.PortsIds.Liverpool;

            
            //Let's get all journeys that start with our home port 
            var routesMatchingStart = from journey in _fixture.Journeys
                                      join route in _fixture.Routes on journey.RouteId equals route.Id
                                      where route.StartPortId == homePortId
                                      select new { journey.NetworkId };

            var routesMatchingEnd = from journey in _fixture.Journeys
                                    join route in _fixture.Routes on journey.RouteId equals route.Id
                                    where route.DestinationPortId == destinationPortId
                                    select new { journey.NetworkId };


            var journeyRouteDetails = (from journey in _fixture.Journeys
                                       join matchingStartRoutes in routesMatchingStart on journey.NetworkId equals matchingStartRoutes.NetworkId
                                       join matchingEndRoutes in routesMatchingEnd on journey.NetworkId equals matchingEndRoutes.NetworkId
                                       join route in _fixture.Routes on journey.RouteId equals route.Id
                                       orderby journey.NetworkId, journey.Order
                                       select new { journey.NetworkId, journey.Order, route.Id, route.DaysDuration }).ToList();


            var quickestJourneyTime = journeyRouteDetails.GroupBy(grp => grp.NetworkId, )

            Assert.Equal(expectedJourneyTime, journeyTimeReturned);
        }

        /// <summary>
        /// Route 2.  New York  New York
        /// </summary>
        [Fact]
        public void Calling_Calculate_Shortest_Journey_Time_For_Route_2_Returns_X_days()
        {
            var expectedJourneyTime = 2;
            var journeyTimeReturned = 0;

            Assert.Equal(expectedJourneyTime, journeyTimeReturned);
        }

    }
}
