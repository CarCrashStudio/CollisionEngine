namespace LinkEngine.RPG
{
    public class Ability
    {
        public int buffamnt = 0;

        public int ID { get; set; }
        public string Name { get; set; }

        public int BuffAmount { get { return buffamnt * AbilityLevel; ; } } // Amount to buff TargetVariable
        public string TargetVariable { get; set; } // The Target Entity's Health, Strength, Defense, etc. to be buffed or debuffed

        public int AbilityLevel { get; set; } // The level of the Ability, incresing buff and debuff amounts

        public Ability(int id, string name, string targetVar, int buff)
        {
            ID = id;
            Name = name;
            buffamnt = buff;
            TargetVariable = targetVar;
        }
        public Ability(Ability ability)
        {
            ID = ability.ID;
            Name = ability.Name;
            buffamnt = ability.buffamnt;
            TargetVariable = ability.TargetVariable;
        }
        public Ability ()
        {

        }
    }
}
