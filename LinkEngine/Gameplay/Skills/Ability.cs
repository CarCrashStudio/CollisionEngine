namespace LinkEngine.Gameplay.Skills
{
    public class Ability
    {
        public int buffamnt = 0;

        public int ID { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Amount to buff the TargetVariable
        /// </summary>
        public int BuffAmount { get { return buffamnt * AbilityLevel; ; } }
        /// <summary>
        /// The target skill this modifier will buff (S.P.E.C.I.A.L)
        /// Can also target Health and max health
        /// </summary>
        public string TargetVariable { get; set; }

        /// <summary>
        /// The level of the ability affects the buffamount
        /// </summary>
        public int AbilityLevel { get; set; }

        /// <summary>
        /// Creates a new Ability from the given parameters
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="targetVar"></param>
        /// <param name="buff"></param>
        public Ability(int id, string name, string targetVar, int buff)
        {
            ID = id;
            Name = name;
            buffamnt = buff;
            TargetVariable = targetVar;
        }
        /// <summary>
        /// Creates a copy of the given Ability
        /// </summary>
        /// <param name="ability">The ability to copy</param>
        public Ability(Ability ability)
        {
            ID = ability.ID;
            Name = ability.Name;
            buffamnt = ability.buffamnt;
            TargetVariable = ability.TargetVariable;
        }
        /// <summary>
        /// Creates a blank Ability
        /// </summary>
        public Ability ()
        {

        }
    }
}
