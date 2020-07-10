using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collision2D.ProceduralGeneration
{
    public class World
    {
        private List<Utils.Entities.Entity> _entities = new List<Utils.Entities.Entity>();
        private List<Utils.Tile> _ground = new List<Utils.Tile>();
        private List<Utils.Tile> _deco = new List<Utils.Tile>();

        public List<Utils.Tile> GroundTiles { get => _ground; set => _ground = value; }
        public List<Utils.Tile> DecorationTiles { get => _deco; set => _deco = value; }
        public List<Utils.Entities.Entity> Entities { get => _entities; set => _entities = value; }
        public World()
        {
        }
    }
}
