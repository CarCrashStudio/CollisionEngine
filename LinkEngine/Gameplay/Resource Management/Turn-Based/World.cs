﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkEngine.GameTypes.Top_Down.Strategy.Turn_Based
{
    public class World : WorldGen.World
    {
        public int TileSize { get; set; }
        public int WorldSize { get; set; }

        public int AvailableFactions { get; set; }
    }
}
