using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransportPlanner.Models;
using TransportPlanner.Services;
using TransportPlanner.Tests.Fixtures;
using Xunit;

namespace TransportPlanner.Tests
{
    /// <summary>
    /// What is the total journey time for the following direct routes (Model should indicate if the journey is invalid):    
    /// Route 1.  Buenos Aires  New York  Liverpool = 10 Days
    /// Route 2.  Buenos Aires  Casablanca  Liverpool = 8 Days
    /// Route 3.  Buenos Aires  Capetown  New York  Liverpool  Casablanca = 19 Days
    /// Route 4.  Buenos Aires  Capetown  Casablanca = INVALID
    /// </summary>
    public class CalculateJourneyTimeTests : IClassFixture<RoutePlannerFixture>
    {
        RoutePlannerFixture _fixture;
        JourneyTimeCalculator _journeyTimeCalculator;
        public CalculateJourneyTimeTests(RoutePlannerFixture fixture)
        {
            _fixture = fixture;
            _journeyTimeCalculator = new JourneyTimeCalculator(_fixture._context);
        }

        /// <summary>
        /// Route 1.  Buenos Aires  New York  Liverpool = 10 Days
        /// </summary>
        [Fact]
        public void Calling_Calculate_Journey_Time_For_Route_1_Returns_10_Days()
        {
            //Arrange
            var expectedJourneyTime = 10;

            var journeyTimeRequest = new JourneyTimeCalculator.Request();
            journeyTimeRequest.Routes.Add(new JourneyTimeCalculator.RequestRoute((int)RoutePlannerFixture.PortsIds.BuenosAires, (int)RoutePlannerFixture.PortsIds.NewYork));
            journeyTimeRequest.Routes.Add(new JourneyTimeCalculator.RequestRoute((int)RoutePlannerFixture.PortsIds.NewYork, (int)RoutePlannerFixture.PortsIds.Liverpool));
            
            //Act
            var response = _journeyTimeCalculator.CalculateJourneyTime(journeyTimeRequest);

            //Assert
            Assert.Equal(expectedJourneyTime, response.JourneyTime);
        }

        /// <summary>
        /// Route 2.  Buenos Aires  Casablanca  Liverpool = 8 Days
        /// </summary>
        [Fact]
        public void Calling_Calculate_Journey_Time_For_Route_2_Returns_8_Days()
        {
            //Arrange
            var expectedJourneyTime = 8;

            //Act
            var journeyTimeRequest = new JourneyTimeCalculator.Request();
            journeyTimeRequest.Routes.Add(new JourneyTimeCalculator.RequestRoute( (int)RoutePlannerFixture.PortsIds.BuenosAires, (int)RoutePlannerFixture.PortsIds.Casablanca));
            journeyTimeRequest.Routes.Add(new JourneyTimeCalculator.RequestRoute( (int)RoutePlannerFixture.PortsIds.Casablanca, (int)RoutePlannerFixture.PortsIds.Liverpool));

            //Act
            var response = _journeyTimeCalculator.CalculateJourneyTime(journeyTimeRequest);

            //Assert
            Assert.NotNull(response);
            Assert.True(response.IsValidRoute);
            Assert.Equal(expectedJourneyTime, response.JourneyTime);

        }

        /// <summary>
        /// Route 3.  Buenos Aires  Capetown  New York  Liverpool  Casablanca = 19 Days
        /// </summary>
        [Fact]
        public void Calling_Calculate_Journey_Time_For_Route_3_Returns_19_Days()
        {
            //Arrange
            var expectedJourneyTime = 19;            

            //Act
            var journeyTimeRequest = new JourneyTimeCalculator.Request();
            journeyTimeRequest.Routes.Add(new JourneyTimeCalculator.RequestRoute((int)RoutePlannerFixture.PortsIds.BuenosAires, (int)RoutePlannerFixture.PortsIds.CapeTown));
            journeyTimeRequest.Routes.Add(new JourneyTimeCalculator.RequestRoute((int)RoutePlannerFixture.PortsIds.CapeTown, (int)RoutePlannerFixture.PortsIds.NewYork));
            journeyTimeRequest.Routes.Add(new JourneyTimeCalculator.RequestRoute((int)RoutePlannerFixture.PortsIds.NewYork, (int)RoutePlannerFixture.PortsIds.Liverpool));
            journeyTimeRequest.Routes.Add(new JourneyTimeCalculator.RequestRoute((int)RoutePlannerFixture.PortsIds.Liverpool, (int)RoutePlannerFixture.PortsIds.Casablanca));

            //Act
            var response = _journeyTimeCalculator.CalculateJourneyTime(journeyTimeRequest);


            //Assert
            Assert.NotNull(response);
            Assert.True(response.IsValidRoute);
            Assert.Equal(expectedJourneyTime, response.JourneyTime);

        }

        /// <summary>
        /// Invalid Route: Buenos Aires  Capetown  Casablanca
        /// </summary>
        [Fact]
        public void Calling_Calculate_Journey_Time_For_Route_4_Returns_Invalid_Route_Response()
        {
            //Arrange            
            var journeyTimeRequest = new JourneyTimeCalculator.Request();
            journeyTimeRequest.Routes.Add(new JourneyTimeCalculator.RequestRoute((int)RoutePlannerFixture.PortsIds.BuenosAires, (int)RoutePlannerFixture.PortsIds.CapeTown));
            journeyTimeRequest.Routes.Add(new JourneyTimeCalculator.RequestRoute((int)RoutePlannerFixture.PortsIds.CapeTown, (int)RoutePlannerFixture.PortsIds.Casablanca));


            //Act
            var response = _journeyTimeCalculator.CalculateJourneyTime(journeyTimeRequest);

            //Assert
            Assert.NotNull(response);
            Assert.False(response.IsValidRoute);
        }

    }
}
