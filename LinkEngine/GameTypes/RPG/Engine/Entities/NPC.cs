namespace LinkEngine.RPG
{
    public class NPC
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        public Quest QuestAvailableHere { get; set; }
        // public Shop ShopavailableHere { get; set; }

        public NPC(int _id, string _name, int x, int y)//, Quest _questavailableHere) // Shop _shopavailableHere)
        {
            ID = _id;
            Name = _name;
            X = x;
            Y = y;
            // QuestAvailableHere = _questavailableHere;
            // ShopavailableHere = _shopavailableHere;
        }
        public NPC(NPC npc)
        {
            ID = npc.ID;
            Name = npc.Name;
            // QuestAvailableHere = npc.QuestAvailableHere;
            // ShopavailableHere = npc.ShopavailableHere;
        }
    }
}
