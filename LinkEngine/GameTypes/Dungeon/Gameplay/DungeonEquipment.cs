namespace LinkEngine.Dungeon
{
    public class DungeonEquipment : DungeonItem
    {
        /// <summary>
        /// The index to place this equipment into
        /// </summary>
        public short Slot { get; set; }

        /// <summary>
        /// The strength modifier applied to the player
        /// </summary>
        public short Strength { get; set; }
        /// <summary>
        /// The defense modifier applied to the player
        /// </summary>
        public short Defense { get; set; }

        /// <summary>
        /// Bool flag indicating whether or not this piece of equipment is already equipped
        /// </summary>
        public bool Equipped { get; set; }

        /// <summary>
        /// Creates a new piece of equipment from the given parameters
        /// </summary>
        /// <param name="_id">The ID of the Equipment</param>
        /// <param name="_name">The Name of the Equipment</param>
        /// <param name="_namePlural">The plural Name of the Equipment</param>
        /// <param name="str">The strength modifier</param>
        /// <param name="def">the defense modifier</param>
        public DungeonEquipment (int _id, string _name, string _namePlural, short str, short def) : base(_id, _name, _namePlural, true, false)
        {
            Strength = str;
            Defense = def;
        }
    }
}
