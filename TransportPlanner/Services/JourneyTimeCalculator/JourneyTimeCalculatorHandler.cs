using System;
using System.Linq;
using TransportPlanner.Dependencies;

namespace TransportPlanner.Services.JourneyTimeCalculator
{
    /// <summary>
    /// The client has described having a number of ports, and predefined available routes between them.
    /// The client has expressed a need to test journey times of a different route combinations, based on if the route between ports is valid.
    /// This simple service allows the client to request the journey time by explicitly providing the routes a journey may utilise.
    /// </summary>
   public class JourneyTimeCalculatorHandler : IJourneyTimeCalculatorHandler
    {
        private readonly ITransportPlannerContext _context;

        public JourneyTimeCalculatorHandler(ITransportPlannerContext context)
        {
            _context = context;
        }

        public Response CalculateJourneyTime(Request request)
        {
            if (request is null)
            {
                throw new ArgumentException($"Invalid request submitted");
            }

            if (!request.Routes.Any() || request.Routes.Count < 2)
            {
                throw new ArgumentException($"Must provide at least 2 routes to calculate Journey Time");
            }
            
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
                response.IsValidRoute = true;
                response.JourneyTime += matchingRoute.DaysDuration;
            }

            return response;
        }




    }
}
