using System.Collections.Generic;

namespace LinkEngine
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

        public void giveExperience(int exp)
        {
            Exp += exp;
        }

        /// <summary>
        /// LevelUp() will check for the player's current Exp and if it is higher than the MaxExp it will increase Level by 1. This function will then increase the MaxExp by a random amount.
        /// </summary>
        public void LevelUp()
        {
            // check if the exp is high enough to level up
            if (Exp >= MaxExp)
            {
                // increase the level
                Level++;

                // increase the maximum amount of exp needed to level up.
                MaxExp += rand.Next(100, MaxExp);
            }
        }
    }
}
