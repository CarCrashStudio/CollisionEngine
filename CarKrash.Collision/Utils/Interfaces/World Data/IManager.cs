using System.Collections.Generic;

namespace CarKrash.Collision.Utils
{
    public interface IManager<T>
    {
        IRoom ActiveRoom { get; }
        List<ITransition> Transitions { get; }

        DungeonGeneration<T> Dungeon { get; }
        int Seed { get; }

        object[] TileSprites { get; }
        int MaximumRoomCount { get; }
    }
}
