using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using TransportPlanner.Services;
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
        JourneyFinder _journeyFinder;
        public CalculateShortestJourneyTimeTests(RoutePlannerFixture fixture)
        {
            _fixture = fixture;
            _journeyFinder = new JourneyFinder(_fixture._context, _fixture._journeyQuery);

        }

        /// <summary>
        /// Route 1.  Buenos Aires  Liverpool
        /// </summary>
        [Fact]
        public void Calling_Calculate_Shortest_Journey_Time_For_Route_1_Returns_8_days()
        {
            //Arrange
            var expectedJourneyTime = 8;
            var journeyTimeReturned = 0;

            var homePortId = (int)RoutePlannerFixture.PortsIds.BuenosAires;
            var destinationPortId = (int)RoutePlannerFixture.PortsIds.Liverpool;


            //Act
            var routesFoundByService = _journeyFinder.FindJourneys(
             new JourneyFinder.Request()
             {
                 StartPortId = homePortId,
                 DestinationPortId = destinationPortId,
                 FilterCriteria = new JourneyFinder.FilterCriteria(true, null)
             });

            //Assert
            Assert.NotNull(routesFoundByService);
            //Assert that only fastest route is returned
            Assert.Single(routesFoundByService.Journeys);

            journeyTimeReturned = routesFoundByService.Journeys.FirstOrDefault().TotalJourneyTime();

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


            //Act
            var routesFoundByService = _journeyFinder.FindJourneys(
             new JourneyFinder.Request()
             {
                 StartPortId = homePortId,
                 DestinationPortId = destinationPortId,
                 FilterCriteria = new JourneyFinder.FilterCriteria(true, null)
             });

            //Assert
            Assert.NotNull(routesFoundByService);
            //Assert that only fastest route is returned
            Assert.Single(routesFoundByService.Journeys);

            journeyTimeReturned = routesFoundByService.Journeys.FirstOrDefault().TotalJourneyTime();            

            Assert.Equal(expectedJourneyTime, journeyTimeReturned);
        }

    }
}
