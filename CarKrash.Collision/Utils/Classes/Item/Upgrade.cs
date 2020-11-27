namespace CarKrash.Collision.Utils
{
    public class Upgrade
    {
        private int level = 1;
        private Modifier modifier;
        private bool applied = false;

        public int Level => level;
        public Modifier Modifier => modifier * (float)(level * 0.2f);
        public bool Applied { get => applied; set => applied = value; }
    }
}