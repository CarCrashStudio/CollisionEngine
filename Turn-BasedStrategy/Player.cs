namespace LinkEngine.Strategy.TurnBased
{
    public class Player
    {
        /// <summary>
        /// The Faction is who the player will play as ingame
        /// </summary>
        public Faction Faction { get; set; }

        /// <summary>
        /// The camera controlling how the player see the world
        /// </summary>
        public Components.Camera camera { get; set; }
    }
}
