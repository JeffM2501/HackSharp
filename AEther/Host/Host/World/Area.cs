using System;
using System.Collections.Generic;


using Host.Entities;
namespace Host.World
{
    public class Area
    {
        public string ID = string.Empty;
        public string Name = string.Empty;
        public string Description = string.Empty;

        public List<Entity> Entities = new List<Entity>();
    }
}
