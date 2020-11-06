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
        /// Port Id Enum to Save on referencing magic numbers
        /// </summary>
        public enum PortsIds
        {
            NewYork,
            Liverpool,
            Casablanca,
            CapeTown,
            BuenosAires
        }

        /// <summary>
        /// Route Id Enum to save on referencing magic numbers
        /// </summary>
        public enum RouteIds
        {
            BuenosAiresToNewYork,
            BuenosAiresToCasablanca,
            BuenosAiresToCapeTown,
            NewYorkToLiverpool,
            LiverpoolToCasablanca,
            LiverpoolToCapeTown,
            CasablancaToLiverpool,
            CasablancaToCapeTown,
            CapeTownToNewYork
        }

        public IList<Port> Ports { get; set; }       
        public IList<Route> Routes { get; set; }        
        public IList<Journey> Journeys { get; set; }

        public RoutePlannerFixture()
        {

            SeedPortsData();
            SeedRoutes();
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

        private void SeedRoutes()
        {
            Routes = new List<Route>();

            //Buenos Aires to New York
            Routes.Add(new Route((int)RouteIds.BuenosAiresToNewYork, (int)PortsIds.BuenosAires, (int)PortsIds.NewYork, 6));

            //Buenos Aires to Casablanca
            Routes.Add(new Route((int)RouteIds.BuenosAiresToCasablanca, (int)PortsIds.BuenosAires, (int)PortsIds.Casablanca, 5));

            //Buenos Aires to Cape Town
            Routes.Add(new Route((int)RouteIds.BuenosAiresToCapeTown, (int)PortsIds.BuenosAires, (int)PortsIds.CapeTown, 4));

            //New York to Liverpool
            Routes.Add(new Route((int)RouteIds.BuenosAiresToCapeTown, (int)PortsIds.NewYork, (int)PortsIds.Liverpool, 4));

            //Liverpool to Casablanca
            Routes.Add(new Route((int)RouteIds.LiverpoolToCasablanca, (int)PortsIds.Liverpool, (int)PortsIds.Casablanca, 3));

            //Liverpool to Cape Town
            Routes.Add(new Route((int)RouteIds.LiverpoolToCapeTown, (int)PortsIds.Liverpool, (int)PortsIds.CapeTown, 6));

            //Casablanca to Liverpool
            Routes.Add(new Route((int)RouteIds.CasablancaToLiverpool, (int)PortsIds.Casablanca, (int)PortsIds.Liverpool, 3));

            //Casablanca to Cape Town
            Routes.Add(new Route((int)RouteIds.CasablancaToCapeTown, (int)PortsIds.Casablanca, (int)PortsIds.CapeTown, 6));

            //Cape Town to New York
            Routes.Add(new Route((int)RouteIds.CapeTownToNewYork, (int)PortsIds.CapeTown, (int)PortsIds.NewYork, 8));
        }

        private void SeedJourneys()
        {
            //Buenos Aires > Casablanca > Liverpool
            Journeys.Add(new Journey());

            //Buenos Aires > Cape Town > > New York > Liverpool
            Journeys.Add(new Journey());

        }

    }
}
