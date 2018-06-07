namespace RPG
{
    public class NPC
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        public Quest QuestAvailableHere { get; set; }
        public Shop ShopAvailibleHere { get; set; }

        public NPC(int _id, string _name, int x, int y, Quest _questAvailibleHere, Shop _shopAvailibleHere)
        {
            ID = _id;
            Name = _name;
            X = x;
            Y = y;
            QuestAvailableHere = _questAvailibleHere;
            ShopAvailibleHere = _shopAvailibleHere;
        }
        public NPC(NPC npc)
        {
            ID = npc.ID;
            Name = npc.Name;
            QuestAvailableHere = npc.QuestAvailableHere;
            ShopAvailibleHere = npc.ShopAvailibleHere;
        }
    }
}
