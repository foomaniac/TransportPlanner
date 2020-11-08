using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using TransportPlanner.Context;
using TransportPlanner.Dependencies;
using TransportPlanner.Models;
using TransportPlanner.Queries;
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
            CapeTownToNewYork,
        }

        public enum NetworkIds
        {
            BuenosAiresCasablancaLiverpool,
            BuenosAiresCapeTownNewYorkLiverpool,
            BuenosAiresNewYorkLiverpool,
            NewYorkLiverpoolCapeTownNewYork,
            BuenosAiresCasablanca,
            LiverpoolCapeTownNewyorkLiverpool,
            LiverpoolCasablancaLiverpool,
            NewYorkLiverpoolCasablancaLiverpoolCapeTownNewYork
        }

        public ITransportPlannerContext _context;
        public IJourneyQuery _journeyQuery;        

        public RoutePlannerFixture()
        {
            _context = new TransportPlannerContext();
            _journeyQuery = new JourneyQuery(_context);

            SeedPortsData();
            SeedRoutes();
            SeedJourneys();
        }

        private void SeedPortsData()
        {
            _context.Ports = new List<Port>();

            //Populate ports provided for testing scenarions
            _context.Ports.Add(new Port((int)PortsIds.NewYork, "New York"));
            _context.Ports.Add(new Port((int)PortsIds.Liverpool, "Liverpool"));
            _context.Ports.Add(new Port((int)PortsIds.Casablanca, "Casablanca"));
            _context.Ports.Add(new Port((int)PortsIds.CapeTown, "Cape Town"));
            _context.Ports.Add(new Port((int)PortsIds.BuenosAires, "Buenos Aires"));
        }

        private void SeedRoutes()
        {
            _context.Routes = new List<Route>();

            //Buenos Aires to New York
            _context.Routes.Add(new Route((int)RouteIds.BuenosAiresToNewYork, (int)PortsIds.BuenosAires, (int)PortsIds.NewYork, 6));

            //Buenos Aires to Casablanca
            _context.Routes.Add(new Route((int)RouteIds.BuenosAiresToCasablanca, (int)PortsIds.BuenosAires, (int)PortsIds.Casablanca, 5));

            //Buenos Aires to Cape Town
            _context.Routes.Add(new Route((int)RouteIds.BuenosAiresToCapeTown, (int)PortsIds.BuenosAires, (int)PortsIds.CapeTown, 4));

            //New York to Liverpool
            _context.Routes.Add(new Route((int)RouteIds.NewYorkToLiverpool, (int)PortsIds.NewYork, (int)PortsIds.Liverpool, 4));

            //Liverpool to Casablanca
            _context.Routes.Add(new Route((int)RouteIds.LiverpoolToCasablanca, (int)PortsIds.Liverpool, (int)PortsIds.Casablanca, 3));

            //Liverpool to Cape Town
            _context.Routes.Add(new Route((int)RouteIds.LiverpoolToCapeTown, (int)PortsIds.Liverpool, (int)PortsIds.CapeTown, 6));

            //Casablanca to Liverpool
            _context.Routes.Add(new Route((int)RouteIds.CasablancaToLiverpool, (int)PortsIds.Casablanca, (int)PortsIds.Liverpool, 3));

            //Casablanca to Cape Town
            _context.Routes.Add(new Route((int)RouteIds.CasablancaToCapeTown, (int)PortsIds.Casablanca, (int)PortsIds.CapeTown, 6));

            //Cape Town to New York
            _context.Routes.Add(new Route((int)RouteIds.CapeTownToNewYork, (int)PortsIds.CapeTown, (int)PortsIds.NewYork, 8));
        }

        private void SeedJourneys()
        {
            _context.Journeys = new List<Journey>();
            _context.JourneyRoutes = new List<JourneyRoute>();

            //Route Network 0 - 8 Days
            //Buenos Aires > Casablanca > Liverpool
            _context.Journeys.Add(new Journey((int)NetworkIds.BuenosAiresCasablancaLiverpool, "Buenos Aires > Casablanca > Liverpool"));
            _context.JourneyRoutes.Add(new JourneyRoute((int)NetworkIds.BuenosAiresCasablancaLiverpool, 0, (int)RouteIds.BuenosAiresToCasablanca));
            _context.JourneyRoutes.Add(new JourneyRoute((int)NetworkIds.BuenosAiresCasablancaLiverpool, 1, (int)RouteIds.CasablancaToLiverpool));

            //Route Network 1 - 16 Days
            //Buenos Aires > Cape Town > New York > Liverpool
            _context.Journeys.Add(new Journey((int)NetworkIds.BuenosAiresCapeTownNewYorkLiverpool, "Buenos Aires > Cape Town > New York > Liverpool"));
            _context.JourneyRoutes.Add(new JourneyRoute((int)NetworkIds.BuenosAiresCapeTownNewYorkLiverpool, 0, (int)RouteIds.BuenosAiresToCapeTown));
            _context.JourneyRoutes.Add(new JourneyRoute((int)NetworkIds.BuenosAiresCapeTownNewYorkLiverpool, 1, (int)RouteIds.CapeTownToNewYork));
            _context.JourneyRoutes.Add(new JourneyRoute((int)NetworkIds.BuenosAiresCapeTownNewYorkLiverpool, 2, (int)RouteIds.NewYorkToLiverpool));

            //Route Network 2 - 10 Days
            //Buenos Aires > New York > Liverpool 
            _context.Journeys.Add(new Journey((int)NetworkIds.BuenosAiresNewYorkLiverpool, "Buenos Aires > New York > Liverpool "));
            _context.JourneyRoutes.Add(new JourneyRoute((int)NetworkIds.BuenosAiresNewYorkLiverpool, 0, (int)RouteIds.BuenosAiresToNewYork));
            _context.JourneyRoutes.Add(new JourneyRoute((int)NetworkIds.BuenosAiresNewYorkLiverpool, 1, (int)RouteIds.NewYorkToLiverpool));

            //Route Network 3 - 18 Days
            //New York > Liverpool > Cape Town > New York
            _context.Journeys.Add(new Journey((int)NetworkIds.NewYorkLiverpoolCapeTownNewYork, "New York > Liverpool > Cape Town > New York"));
            _context.JourneyRoutes.Add(new JourneyRoute((int)NetworkIds.NewYorkLiverpoolCapeTownNewYork, 0, (int)RouteIds.NewYorkToLiverpool));
            _context.JourneyRoutes.Add(new JourneyRoute((int)NetworkIds.NewYorkLiverpoolCapeTownNewYork, 1, (int)RouteIds.LiverpoolToCapeTown));
            _context.JourneyRoutes.Add(new JourneyRoute((int)NetworkIds.NewYorkLiverpoolCapeTownNewYork, 2, (int)RouteIds.CapeTownToNewYork));

            //Route Network 4 - Added to check logic of finding validate routes based on start and end destination
            //Buesno Aires > Casablanca - 5 Days
            _context.Journeys.Add(new Journey((int)NetworkIds.BuenosAiresCasablanca, "Buesno Aires > Casablanca "));
            _context.JourneyRoutes.Add(new JourneyRoute((int)NetworkIds.BuenosAiresCasablanca, 0, (int)RouteIds.BuenosAiresToCasablanca));

            //Route Network 5 
            //Liverpool > Cape Town > NewYork > Liverpool
            _context.Journeys.Add(new Journey((int)NetworkIds.LiverpoolCapeTownNewyorkLiverpool, "Liverpool > Cape Town > NewYork > Liverpool"));
            _context.JourneyRoutes.Add(new JourneyRoute((int)NetworkIds.LiverpoolCapeTownNewyorkLiverpool, 0, (int)RouteIds.LiverpoolToCapeTown));
            _context.JourneyRoutes.Add(new JourneyRoute((int)NetworkIds.LiverpoolCapeTownNewyorkLiverpool, 1, (int)RouteIds.CapeTownToNewYork));
            _context.JourneyRoutes.Add(new JourneyRoute((int)NetworkIds.LiverpoolCapeTownNewyorkLiverpool, 2, (int)RouteIds.NewYorkToLiverpool));

            //Route Network 5 
            //Liverpool > Casablanc > Liverpool
            _context.Journeys.Add(new Journey((int)NetworkIds.LiverpoolCasablancaLiverpool, "Liverpool > Casablanc > Liverpool"));
            _context.JourneyRoutes.Add(new JourneyRoute((int)NetworkIds.LiverpoolCasablancaLiverpool, 0, (int)RouteIds.LiverpoolToCasablanca));
            _context.JourneyRoutes.Add(new JourneyRoute((int)NetworkIds.LiverpoolCasablancaLiverpool, 1, (int)RouteIds.CasablancaToLiverpool));


            //Route Network 6
            //New York > Liverpool > Casablanca > Liverpool > Cape Town > New York
            _context.Journeys.Add(new Journey((int)NetworkIds.NewYorkLiverpoolCasablancaLiverpoolCapeTownNewYork, "New York > Liverpool > Casablanca > Liverpool > Cape Town > New York"));
            _context.JourneyRoutes.Add(new JourneyRoute((int)NetworkIds.NewYorkLiverpoolCasablancaLiverpoolCapeTownNewYork, 0, (int)RouteIds.NewYorkToLiverpool));
            _context.JourneyRoutes.Add(new JourneyRoute((int)NetworkIds.NewYorkLiverpoolCasablancaLiverpoolCapeTownNewYork, 1, (int)RouteIds.LiverpoolToCasablanca));
            _context.JourneyRoutes.Add(new JourneyRoute((int)NetworkIds.NewYorkLiverpoolCasablancaLiverpoolCapeTownNewYork, 2, (int)RouteIds.CasablancaToLiverpool));
            _context.JourneyRoutes.Add(new JourneyRoute((int)NetworkIds.NewYorkLiverpoolCasablancaLiverpoolCapeTownNewYork, 3, (int)RouteIds.LiverpoolToCapeTown));
            _context.JourneyRoutes.Add(new JourneyRoute((int)NetworkIds.NewYorkLiverpoolCasablancaLiverpoolCapeTownNewYork, 4, (int)RouteIds.CapeTownToNewYork));

        }

    }
}
