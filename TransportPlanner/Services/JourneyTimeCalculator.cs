using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using TransportPlanner.Dependencies;
using TransportPlanner.Models;

namespace TransportPlanner.Services
{
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

            foreach (var wayPoint in request.Routes)
            {
                var matchingRoute = _context.Routes.FirstOrDefault(wp => wp.StartPortId == wayPoint.StartPortId && wp.DestinationPortId == wayPoint.DestinationPortId);
                                
                if (matchingRoute == null)
                {
                    response.IsValidRoute = false;
                    break;
                }

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

            public int Step { get; set; }
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
