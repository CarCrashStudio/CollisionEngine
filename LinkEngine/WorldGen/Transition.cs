using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkEngine.WorldGen
{
    public class Transition : Tile
    {
        public string RequiredFacingDirection { get; set; }
        public Room NextLocation { get; set; }

        public Transition(int id, string name, int dense, int x, int y, string type, string facing, Room nextLoc) :
            base(id, name, dense, x, y, type)
        {
            RequiredFacingDirection = facing;
            NextLocation = nextLoc;
        }
        public Transition(Tile tile, string facing, Room nextLoc) :
            base(tile.ID, tile.Name, tile.Dense, tile.X, tile.Y, tile.Type)
        {
            RequiredFacingDirection = facing;
            NextLocation = nextLoc;
        }
    }
}
