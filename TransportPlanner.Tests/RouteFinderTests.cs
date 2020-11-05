using System;
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

        /// <summary>
        /// Route 1. Find the number of routes from Liverpool to Liverpool with a maximum number of 3 stops.
        /// </summary>
        [Fact]
        public void Calling_Find_Routes_Matching_Scenario_1_Returns_Correct_Number_Of_Matches()
        {
            var expectedJourneyTime = 1;
            var journeyTimeReturned = 0;

            Assert.Equal(expectedJourneyTime, journeyTimeReturned);
        }

        /// <summary>
        /// Route 2. Find the number of routes from Buenos Aires to Liverpool where exactly 4 stops are made.
        /// </summary>
        [Fact]
        public void Calling_Find_Routes_Matching_Scenario_2_Returns_Correct_Number_Of_Matches()
        {
            var expectedJourneyTime = 2;
            var journeyTimeReturned = 0;

            Assert.Equal(expectedJourneyTime, journeyTimeReturned);
        }

        /// <summary>
        /// Route 3. Find the number of routes from Liverpool to Liverpool where the journey time is less than or equal to 25 days.
        /// </summary>
        [Fact]
        public void Calling_Find_Routes_Matching_Scenario_3_Returns_Correct_Number_Of_Matches()
        {
            var expectedJourneyTime = 2;
            var journeyTimeReturned = 0;

            Assert.Equal(expectedJourneyTime, journeyTimeReturned);
        }

    }
}
