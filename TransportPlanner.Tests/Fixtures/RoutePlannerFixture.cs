using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using TransportPlanner.Models;
using Xunit;

namespace TransportPlanner.Tests.Fixtures
{
   public class RoutePlannerFixture
    {
        /// <summary>
        /// Port Id Enum to Save on referencing numbers
        /// </summary>
        public enum PortsIds
        {
            NewYork,
            Liverpool,
            Casablanca,
            CapeTown,
            BuenosAires
        }

        public List<Port> Ports { get; set; }       
        public List<WayPoint> WayPoints { get; set; }        

        public RoutePlannerFixture()
        {

            SeedPortsData();
            SeedWayPoints();
        }

        private void SeedPortsData()
        {
            Ports = new List<Port>();

            //Populate ports provided for testing scenarions
            Ports.Add(new Port((int)PortsIds.NewYork, "New York"));
            Ports.Add(new Port((int)PortsIds.Liverpool, "Liverpool"));
            Ports.Add(new Port((int)PortsIds.Casablanca, "Casablanca"));
            Ports.Add(new Port((int)PortsIds.CapeTown, "Cape Town"));
            Ports.Add(new Port((int)PortsIds.BuenosAires, "Buenos Aires"));
        }

        private void SeedWayPoints()
        {
            WayPoints = new List<WayPoint>();

            //Buenos Aires to New York
            WayPoints.Add(new WayPoint(1, (int)PortsIds.BuenosAires, (int)PortsIds.NewYork, 6));

            //Buenos Aires to Casablanca
            WayPoints.Add(new WayPoint(2, (int)PortsIds.BuenosAires, (int)PortsIds.Casablanca, 5));

            //Buenos Aires to Cape Town
            WayPoints.Add(new WayPoint(3, (int)PortsIds.BuenosAires, (int)PortsIds.CapeTown, 4));

            //New York to Liverpool
            WayPoints.Add(new WayPoint(4, (int)PortsIds.NewYork, (int)PortsIds.Liverpool, 4));

            //Liverpool to Casablanca
            WayPoints.Add(new WayPoint(5, (int)PortsIds.Liverpool, (int)PortsIds.Casablanca, 3));

            //Liverpool to Cape Town
            WayPoints.Add(new WayPoint(6, (int)PortsIds.Liverpool, (int)PortsIds.CapeTown, 6));

            //Casablanca to Liverpool
            WayPoints.Add(new WayPoint(7, (int)PortsIds.Casablanca, (int)PortsIds.Liverpool, 3));

            //Casablanca to Cape Town
            WayPoints.Add(new WayPoint(7, (int)PortsIds.Casablanca, (int)PortsIds.CapeTown, 6));

            //Cape Town to New York
            WayPoints.Add(new WayPoint(7, (int)PortsIds.CapeTown, (int)PortsIds.NewYork, 8));
        }

    }
}
