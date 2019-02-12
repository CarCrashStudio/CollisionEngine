using LinkEngine.Components;
using LinkEngine.WorldGen;
using System.Collections.Generic;

namespace LinkEngine.Entities
{
    public class Player : Entity
    {
        System.Random rand = new System.Random();

        /// <summary>
        /// The current level of the Player
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// The amassed experience of the Player
        /// </summary>
        public int Exp { get; set; }
        /// <summary>
        /// the required exp to level up
        /// </summary>
        public int MaxExp { get; set; }

        /// <summary>
        /// the amount of gold the player currently has
        /// </summary>
        public int Gold { get; set; }

        /// <summary>
        /// The location the Player is currently in
        /// </summary>
        public Location CurrentLocation { get; set; }

        /// <summary>
        /// Creates a new Player Object. When created it will populate the player with a PlayerController Component
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="health"></param>
        /// <param name="maxHealth"></param>
        public Player(int id, string name, int health, int maxHealth) : base (id, name, health, maxHealth)
        {
        }

        /// <summary>
        /// ShiftMap moves the map in any direction
        /// Used when keeping the player center screen
        /// </summary>
        /// <param name="x">the amount to shift the map on the X axis</param>
        /// <param name="y">the amount to shift the map on the Y axis</param>
        public void ShiftMap(int x, int y)
        {
            // MapLoc = new Point(MapLoc.X + x, MapLoc.Y + y);
        }

        /// <summary>
        /// CalibrateMap will adjust the map location so that the player can be centered on the screen
        /// </summary>
        /// <param name="centerX"></param>
        /// <param name="centerY"></param>
        void CalibrateMap(int centerX, int centerY)
        {
            //MapLoc = new Point(
            //        // X Coordinate
            //        centerX - camera.X,

            //        // Y Coordinate
            //        centerY - camera.Y
            //    );
        }

        public void giveExperience(int exp)
        {
            Exp += exp;
        }
        public void LevelUp()
        {
            if (Exp >= MaxExp)
            {
                Level++;
                MaxExp += rand.Next(100, MaxExp);
            }
        }
    }
}
