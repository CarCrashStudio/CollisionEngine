namespace LinkEngine.Gameplay.Modifiers
{
    public class Modifier
    {
        short modAmount = 0;

        /// <summary>
        /// The Name of the modifier
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The amount to add to the player's skill
        /// </summary>
        public short ModifierAmount { get { return (short)(modAmount * ModifierLevel); ; } }
        /// <summary>
        /// The skill to target
        /// </summary>
        public string TargetSkill { get; set; }

        /// <summary>
        /// The Level of the modifier increases its usefulness
        /// </summary>
        public short ModifierLevel { get; set; }

        /// <summary>
        /// Creates a new Modifier from the given parameters
        /// A Modifier can be collected by the player and will boost his skill check
        /// </summary>
        /// <param name="name">The Name of the modifier</param>
        /// <param name="targetVar">The Skill the modifer will boost</param>
        /// <param name="buff">How much to buff the skill</param>
        public Modifier(string name, string targetVar, short buff)
        {
            Name = name;
            modAmount = buff;
            TargetSkill = targetVar;
        }
    }
}
