using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkEngine.WorldGen
{
    public class Room
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }

        public List<Tile> Tiles { get; set; }

        public List<Transition> Transitions { get; set; }

        public Room LocationToNorth { get; set; }
        public Room LocationToEast { get; set; }
        public Room LocationToSouth { get; set; }
        public Room LocationToWest { get; set; }

        public Room(int _id, string _name, string _desc, int width, int length)
        {
            ID = _id;
            Name = _name;
            Description = _desc;
            Width = width;
            Length = length;
            Tiles = new List<Tile>();
            Transitions = new List<Transition>();
        }
    }
}
