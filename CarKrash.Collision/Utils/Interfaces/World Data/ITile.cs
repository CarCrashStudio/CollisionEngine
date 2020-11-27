namespace CarKrash.Collision.Utils
{
    //public enum TILE_NAMES
    //{
    //    NORTH_WALL, SOUTH_WALL, EAST_WALL, WEST_WALL,
    //    NORTHWEST_CORNER, NORTHEAST_CORNER, SOUTHWEST_CORNER, SOUTHEAST_CORNER,
    //    FLOOR,
    //    NORTH_DOOR, SOUTH_DOOR, EAST_DOOR, WEST_DOOR,
    //    SMALL_CHEST
    //}
    public interface ITile<T>
    {
        int ID { get; }
        T Sprite { get; }
        float X { get; }
        float Y { get; }

        bool Dense { get; }
    }
}
