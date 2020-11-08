using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using TransportPlanner.Dependencies;
using TransportPlanner.Models;

namespace TransportPlanner.Services
{
    /// <summary>
    /// The client has described having a number of ports, and predefined available routes between them.
    /// The client has expressed a need to test journey times of a different route combinations, based on if the route between ports is valid.
    /// This simple service allows the client to request the journey time by explicitly providing the routes a journey may utilise.
    /// </summary>
   public class JourneyTimeCalculator
    {
        private ITransportPlannerContext _context;

        public JourneyTimeCalculator(ITransportPlannerContext context)
        {
            _context = context;
        }

        public Response CalculateJourneyTime(Request request)
        {
            var response = new Response();

            foreach (var route in request.Routes)
            {
                //Do we have a route that matches the request?
                var matchingRoute = _context.Routes.FirstOrDefault(wp => wp.StartPortId == route.StartPortId && wp.DestinationPortId == route.DestinationPortId);                                
               
                if (matchingRoute == null)
                {
                    //Report that this is not a valid route
                    response.IsValidRoute = false;
                    break;
                }

                //Valid route, so add this route to the total journey time
                response.JourneyTime += matchingRoute.DaysDuration;
            }

            return response;
        }

        public class Request
        {
            public IList<RequestRoute> Routes { get; set; }
            public Request()
            {
                Routes = new List<RequestRoute>();
            }
        }

        public class RequestRoute
        {
            public RequestRoute(int startPortId, int destinationPortId)
            {
                StartPortId = startPortId;
                DestinationPortId = destinationPortId;
            }

            public int StartPortId { get; set; }

            public int DestinationPortId { get; set; }
        }

        public class Response
        {
            public bool IsValidRoute { get; set; }
            public int JourneyTime { get; set; }
        }
    }
}
