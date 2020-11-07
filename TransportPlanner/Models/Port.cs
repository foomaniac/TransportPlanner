using System;

namespace TransportPlanner.Models
{
    public class Port
    {
        public int Id { get; private set; }
        public string Name { get; set; }

        public Port(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
