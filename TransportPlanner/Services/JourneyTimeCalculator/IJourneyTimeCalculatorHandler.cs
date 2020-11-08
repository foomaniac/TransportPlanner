using System;
using System.Collections.Generic;
using System.Text;

namespace TransportPlanner.Services.JourneyTimeCalculator
{
   public interface IJourneyTimeCalculatorHandler
    {
        public Response CalculateJourneyTime(Request request);
    }
}
