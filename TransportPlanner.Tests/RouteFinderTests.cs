using System;
using System.Linq;
using TransportPlanner.Dependencies;
using TransportPlanner.Services;
using TransportPlanner.Services.JourneyFinder;
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
        readonly RoutePlannerFixture _fixture;
        readonly IJourneyFinderHandler _journeyFinder;
        public RouteFinderTests(RoutePlannerFixture fixture)
        {
            _fixture = fixture;
            _journeyFinder = new JourneyFinderHandler(_fixture.Context, _fixture.JourneyQuery);
        }

        /// <summary>
        /// Route 1. Find the number of routes from Liverpool to Liverpool with a maximum number of 3 stops.
        /// </summary>
        [Fact]
        public void Calling_Find_Routes_Matching_Scenario_1_Returns_Correct_Number_Of_Matches()
        {
            //Arrange
            var maxiumNumberOfRoutes = 2;
            var expectedNumberOfRoutesWith3Steps = 1;
            var homePortId = (int)RoutePlannerFixture.PortsIds.Liverpool;
            var destinationPortId = (int)RoutePlannerFixture.PortsIds.Liverpool;

            //Act
            var routesFoundByService = _journeyFinder.FindJourneys(new Request(
                 homePortId,
                 destinationPortId,
                new FilterCriteria(false, null, maxiumNumberOfRoutes, null)
            ));

            //Assert            
            Assert.NotNull(routesFoundByService);
            Assert.True(routesFoundByService.FoundMatchingJourneys);

            int calculatedNumberOfRoutesWith3Steps = routesFoundByService.Journeys.Count();
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
            var homePortId = (int)RoutePlannerFixture.PortsIds.BuenosAires;
            var destinationPortId = (int)RoutePlannerFixture.PortsIds.Liverpool;

            var routesFoundByService = _journeyFinder.FindJourneys(new Request(
                homePortId,
                 destinationPortId,
                new FilterCriteria(false, null, null, requiredNumberOfStops)
            ));

            //Assert
            Assert.NotNull(routesFoundByService);
            Assert.True(routesFoundByService.FoundMatchingJourneys);

            int calculatedNumberOfRoutesWith4Steps = routesFoundByService.Journeys.Count();
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
            var homePortId = (int)RoutePlannerFixture.PortsIds.Liverpool;
            var destinationPortId = (int)RoutePlannerFixture.PortsIds.Liverpool;

            var routesFoundByService = _journeyFinder.FindJourneys(new Request(
            homePortId,
                 destinationPortId,
                 new FilterCriteria(false, maximumJourneyTime, null, null)
            ));

            //Assert
            Assert.NotNull(routesFoundByService);
            Assert.True(routesFoundByService.FoundMatchingJourneys);
            Assert.Equal(expectedNumberOfRoutesWith3Steps, routesFoundByService.Journeys.Count);
        }

    }
}