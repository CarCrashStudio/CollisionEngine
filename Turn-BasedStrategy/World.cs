namespace LinkEngine.Strategy.TurnBased
{
    public class World : WorldGen.World
    {
        public int TileSize { get; set; }
        public int WorldSize { get; set; }

        public WorldGen.Tile[,] WorldMap { get; set; }

        public int AvailableFactions { get; set; }

        public World ()
        {
            WorldMap = new WorldGen.Tile[WorldSize, WorldSize];
        }
    }
}
