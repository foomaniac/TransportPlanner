using System;
using TransportPlanner.Tests.Fixtures;
using Xunit;

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
            var expectedJourneyTime = 1;
            var journeyTimeReturned = 0;

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
