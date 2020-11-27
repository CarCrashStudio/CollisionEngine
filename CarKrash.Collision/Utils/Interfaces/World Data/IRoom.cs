using System.Collections.Generic;
namespace CarKrash.Collision.Utils
{
    public interface IRoom
    {
        int ID { get; set; }

        int NorthID { get; set; }
        int SouthID { get; set; }
        int EastID { get; set; }
        int WestID { get; set; }

        float OriginX { get; set; }
        float OriginY { get; set; }

        int Width { get; }
        int Height { get; }
    }
}
