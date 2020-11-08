using System;
using System.Collections.Generic;
using System.Text;
using TransportPlanner.Models;

namespace TransportPlanner.Services.JourneyFinder
{
    public class Response
    {
        public bool FoundMatchingJourneys { get; set; }
        public List<Journey> Journeys { get; }
        public Response()
        {
            Journeys = new List<Journey>();
        }
    }
}
