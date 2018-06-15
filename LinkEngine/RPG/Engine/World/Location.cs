using System.Collections.Generic;
using LinkEngine.WorldGen;
using LinkEngine.Entities;

namespace LinkEngine.RPG
{
    public class Location
    {
        int id;
        string name;
        string desc;

        public int ID { get { return id; } set { id = value; } }
        public string Name { get { return name; } set { name = value; } }
        public string Description { get { return desc; } set { desc = value; } }

        public int Length { get; set; }
        public int Width { get; set; } 

        public List<Tile> Tiles { get; set; }
            
        public Item ItemRequiredToEnter { get; set; }
        public Quest QuestAvailableHere { get; set; }

        public Monster MonsterLivingHere { get; set; }
        public List<NPC> NPCsLivingHere { get; set; }

        public List<Transition> Transitions { get; set; }

        public Location LocationToNorth { get; set; }
        public Location LocationToEast { get; set; }
        public Location LocationToSouth { get; set; }
        public Location LocationToWest { get; set; }

        public Location(int _id, string _name, string _desc, Item itemReq = null, Item itemRequiredToEnter = null, Quest questAvailableHere = null, Monster monsterLivingHere = null)
        {
            id = _id;
            name = _name;
            desc = _desc;
            ItemRequiredToEnter = itemRequiredToEnter;
            QuestAvailableHere = questAvailableHere;
            MonsterLivingHere = monsterLivingHere;
            NPCsLivingHere = new List<NPC>();
            Tiles = new List<Tile>();
        }
        public Location (int _id, string _name, string _desc, int width, int length)
        {
            ID = _id;
            Name = _name;
            Description = _desc;
            Width = width;
            Length = length;
            NPCsLivingHere = new List<NPC>();
            Tiles = new List<Tile>();
            Transitions = new List<Transition>();
        }
        public Location(Location location) //Overload For location class
        {
            id = location.ID;
            name = location.Name;
            desc = location.Description;
            ItemRequiredToEnter = location.ItemRequiredToEnter;
            QuestAvailableHere = location.QuestAvailableHere;
            MonsterLivingHere = location.MonsterLivingHere;

            NPCsLivingHere = new List<NPC>();
            Tiles = new List<Tile>();
            foreach (NPC npc in location.NPCsLivingHere)
            {
                NPCsLivingHere.Add(npc);
            }
        }
    }
    public class Transition : Tile
    {
        public string RequiredFacingDirection { get; set; }
        public Location NextLocation { get; set; }

        public Transition(int id, string name, int dense, int x, int y, string type, string facing, Location nextLoc) :
            base(id, name, dense, x, y, type)
        {
            RequiredFacingDirection = facing;
            NextLocation = nextLoc;
        }
        public Transition(Tile tile, string facing, Location nextLoc) :
            base(tile.ID, tile.Name, tile.Dense, tile.X, tile.Y, tile.Type)
        {
            RequiredFacingDirection = facing;
            NextLocation = nextLoc;
        }
    }
}
