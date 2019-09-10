namespace LinkEngine.RPG2D.Entities
{
    /// <summary>
    /// PartyMember is a class that inherits the Adventurer class. PartyMember can be used in combat.
    /// </summary>
    public class PartyMember
    {
        public Adventurer Details { get; set; }
        
        public PartyMember(Adventurer details)
        {
            Details = details;
        }
    }
}
