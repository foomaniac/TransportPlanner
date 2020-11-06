using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransportPlanner.Tests.Fixtures;
using Xunit;

namespace TransportPlanner.Tests
{
    /// <summary>
    /// What is the total journey time for the following direct routes (your model should indicate if the journey is invalid):    
    /// Route 1.  Buenos Aires  New York  Liverpool = 10 Days
    /// Route 2.  Buenos Aires  Casablanca  Liverpool = 8 Days
    /// Route 3.  Buenos Aires  Capetown  New York  Liverpool  Casablanca = 19 Days
    /// Route 4.  Buenos Aires  Capetown  Casablanca = INVALID
    /// </summary>
    public class CalculateJourneyTimeTests : IClassFixture<RoutePlannerFixture>
    {
        RoutePlannerFixture _fixture;

        public CalculateJourneyTimeTests(RoutePlannerFixture fixture)
        {
            _fixture = fixture;
        }

        /// <summary>
        /// Invalid Route: Buenos Aires  Capetown  Casablanca
        /// </summary>
        [Fact]
        public void Calling_Calculate_Journey_Time_For_Route_4_Returns_Invalid_Route_Response()
        {
            //Arrange
            bool isValidRoute = true;
            var journeyTimeRequest = new JourneyTimeRequest();
            journeyTimeRequest.RouteWayPoints.Add(new RouteWayPoint(0, (int)RoutePlannerFixture.PortsIds.BuenosAires, (int)RoutePlannerFixture.PortsIds.CapeTown));
            journeyTimeRequest.RouteWayPoints.Add(new RouteWayPoint(1, (int)RoutePlannerFixture.PortsIds.CapeTown, (int)RoutePlannerFixture.PortsIds.Casablanca));

            //Act
            foreach (var wayPoint in journeyTimeRequest.RouteWayPoints)
            {
                isValidRoute = _fixture.Routes.Any(wp => wp.StartPortId == wayPoint.FromPortId && wp.DestinationPortId == wayPoint.ToPortId);
                if (!isValidRoute) break;
            }

            //Assert
            Assert.False(isValidRoute);
        }

        /// <summary>
        /// Route 1.  Buenos Aires  New York  Liverpool = 10 Days
        /// </summary>
        [Fact]
        public void Calling_Calculate_Journey_Time_For_Route_1_Returns_10_Days()
        {
            //Arrange
            var expectedJourneyTime = 10;
            var journeyTimeResponse = 0;

            var journeyTimeRequest = new JourneyTimeRequest();
            journeyTimeRequest.RouteWayPoints.Add(new RouteWayPoint(0, (int)RoutePlannerFixture.PortsIds.BuenosAires, (int)RoutePlannerFixture.PortsIds.NewYork));
            journeyTimeRequest.RouteWayPoints.Add(new RouteWayPoint(1, (int)RoutePlannerFixture.PortsIds.NewYork, (int)RoutePlannerFixture.PortsIds.Liverpool));
            
            //Act
            foreach (var wayPoint in journeyTimeRequest.RouteWayPoints)
            {
                var matchingWayPoint = _fixture.Routes.FirstOrDefault(wp => wp.StartPortId == wayPoint.FromPortId && wp.DestinationPortId == wayPoint.ToPortId);
                if(matchingWayPoint != null)
                {
                    journeyTimeResponse += matchingWayPoint.DaysDuration;
                }                
            }

            //Assert
            Assert.Equal(expectedJourneyTime, journeyTimeResponse);
        }

        /// <summary>
        /// Route 2.  Buenos Aires  Casablanca  Liverpool = 8 Days
        /// </summary>
        [Fact]
        public void Calling_Calculate_Journey_Time_For_Route_2_Returns_8_Days()
        {
            //Arrange
            var expectedJourneyTime = 8;
            var journeyTimeResponse = 0;

            //Act
            var journeyTimeRequest = new JourneyTimeRequest();
            journeyTimeRequest.RouteWayPoints.Add(new RouteWayPoint(0, (int)RoutePlannerFixture.PortsIds.BuenosAires, (int)RoutePlannerFixture.PortsIds.Casablanca));
            journeyTimeRequest.RouteWayPoints.Add(new RouteWayPoint(1, (int)RoutePlannerFixture.PortsIds.Casablanca, (int)RoutePlannerFixture.PortsIds.Liverpool));

            //Act
            foreach (var wayPoint in journeyTimeRequest.RouteWayPoints)
            {
                var matchingWayPoint = _fixture.Routes.FirstOrDefault(wp => wp.StartPortId == wayPoint.FromPortId && wp.DestinationPortId == wayPoint.ToPortId);
                if (matchingWayPoint != null)
                {
                    journeyTimeResponse += matchingWayPoint.DaysDuration;
                }
            }

            //Assert
            Assert.Equal(expectedJourneyTime, journeyTimeResponse);

        }

        /// <summary>
        /// Route 3.  Buenos Aires  Capetown  New York  Liverpool  Casablanca = 19 Days
        /// </summary>
        [Fact]
        public void Calling_Calculate_Journey_Time_For_Route_3_Returns_19_Days()
        {
            //Arrange
            var expectedJourneyTime = 19;
            var journeyTimeResponse = 0;

            //Act
            var journeyTimeRequest = new JourneyTimeRequest();
            journeyTimeRequest.RouteWayPoints.Add(new RouteWayPoint(0, (int)RoutePlannerFixture.PortsIds.BuenosAires, (int)RoutePlannerFixture.PortsIds.CapeTown));
            journeyTimeRequest.RouteWayPoints.Add(new RouteWayPoint(1, (int)RoutePlannerFixture.PortsIds.CapeTown, (int)RoutePlannerFixture.PortsIds.NewYork));
            journeyTimeRequest.RouteWayPoints.Add(new RouteWayPoint(2, (int)RoutePlannerFixture.PortsIds.NewYork, (int)RoutePlannerFixture.PortsIds.Liverpool));
            journeyTimeRequest.RouteWayPoints.Add(new RouteWayPoint(3, (int)RoutePlannerFixture.PortsIds.Liverpool, (int)RoutePlannerFixture.PortsIds.Casablanca));

            //Act
            foreach (var wayPoint in journeyTimeRequest.RouteWayPoints)
            {
                var matchingWayPoint = _fixture.Routes.FirstOrDefault(wp => wp.StartPortId == wayPoint.FromPortId && wp.DestinationPortId == wayPoint.ToPortId);
                if (matchingWayPoint != null)
                {
                    journeyTimeResponse += matchingWayPoint.DaysDuration;
                }
            }

            //Assert
            Assert.Equal(expectedJourneyTime, journeyTimeResponse);

        }

        /// <summary>
        /// WIP Code
        /// </summary>
        public class JourneyTimeRequest
        {
            public IList<RouteWayPoint> RouteWayPoints { get; set; }
            public JourneyTimeRequest()
            {
                RouteWayPoints = new List<RouteWayPoint>();
            }
        }

        public class RouteWayPoint
        {

            public RouteWayPoint(int step, int fromPortId, int toPortId)
            {
                Step = step;
                FromPortId = fromPortId;
                ToPortId = toPortId;
            }

            public int Step { get; set; }
            public int FromPortId { get; set; }

            public int ToPortId { get; set; }
        }

    }
}
