using System;

namespace TransportPlanner.Models
{
    /// <summary>
    /// Represents Port within the Clients Network of available routes
    /// </summary>
    public class Port
    {
        public int Id { get; }
        public string Name { get; }

        public Port(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
