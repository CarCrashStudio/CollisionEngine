namespace RPG.Engine
{
    public class Tile// Class to hold tile information 
    {
        const int ICON_SIZE = 32; // 32x32 icons

        int dense;

        public int ID { get; set; }
        public string Name { get; set; }
        public int Dense { get { return dense; } set { dense = value; } } // Tile Density (Can you walk over it or not)

        public int X { get; set; }
        public int Y { get; set; }

        public string Type { get; set; }

        public Tile(int _id, string _name, int _dense, int x, int y, string type)
        {
            ID = _id;
            Name = _name;
            dense = _dense;
            Type = type;
            X = x;
            Y = y;
        }
        public Tile(Tile tile)
        {
            ID = tile.ID;
            Name = tile.Name;
            dense = tile.Dense;
            Type = tile.Type;
            X = tile.X;
            Y = tile.Y;
        }
    }
}
