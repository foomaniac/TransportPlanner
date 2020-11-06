﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TransportPlanner.Models
{
    /// <summary>
    /// Represents a predefined set of routes from start to end
    /// </summary>
   public class Journey
    {
        /// <summary>
        /// Network Groups Journeys 
        /// </summary>
        public int NetworkId { get; private set; }
        public int Step { get; set; }
        public int RouteId { get; set; }
    }
}
