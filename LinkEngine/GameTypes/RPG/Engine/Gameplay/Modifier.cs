namespace LinkEngine.RPG
{
    public class Modifier
    {
        short modAmount = 0;

        public string Name { get; set; }

        public short ModifierAmount { get { return (short)(modAmount * ModifierLevel); ; } }
        public string TargetVariable { get; set; }

        public short ModifierLevel { get; set; }

        public Modifier(string name, string targetVar, short buff)
        {
            Name = name;
            modAmount = buff;
            TargetVariable = targetVar;
        }
    }
}
