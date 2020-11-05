using System;

namespace TransportPlanner.Models
{
    public class Port
    {
        int Id { get; set; }
        string Name { get; set; }


        public Port(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
