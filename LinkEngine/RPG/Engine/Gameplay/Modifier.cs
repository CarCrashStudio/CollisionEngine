namespace LinkEngine.RPG
{
    public class Modifier
    {
        int modAmount = 0;

        public string Name { get; set; }

        public int ModifierAmount { get { return modAmount * ModifierLevel; ; } }
        public string TargetVariable { get; set; }

        public int ModifierLevel { get; set; }

        public Modifier(string name, string targetVar, int buff)
        {
            Name = name;
            modAmount = buff;
            TargetVariable = targetVar;
        }
    }
}
