using System.Collections.Generic;
using LinkEngine.Components;
using LinkEngine.Entities;
namespace LinkEngine.WorldGen
{
    public class Location
    {
        /// <summary>
        /// THe Item the player must have in inventory to enter location
        /// </summary>
        public string ItemRequiredToEnter { get; set; }

        /// <summary>
        /// The monster that can spawn in this location
        /// </summary>
        public Enemy MonsterLivingHere { get; set; }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int Height { get; set; }
        public int Width { get; set; }

        public List<Transition> Transitions { get; set; }

        public Location LocationToNorth { get; set; }
        public Location LocationToEast { get; set; }
        public Location LocationToSouth { get; set; }
        public Location LocationToWest { get; set; }

        public Vector TopLeft_Bound { get; set; }
        public Vector TopRight_Bound { get; set; }
        public Vector BottomLeft_Bound { get; set; }
        public Vector BottomRight_Bound { get; set; }

        public Location(int _id, string _name, string _desc, int width, int length)
        {
            ID = _id;
            Name = _name;
            Description = _desc;
            Width = width;
            Height = length;

            Transitions = new List<Transition>();
        }
    }
}
