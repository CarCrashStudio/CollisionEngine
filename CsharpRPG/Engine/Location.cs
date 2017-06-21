
using System.Collections.Generic;
using System.Drawing;

namespace RPG_Engine
{
    public class Location
    {
        int id;
        string name;
        string desc;

        public Location(int _id, string _name, string _desc, Item itemReq = null, Item itemRequiredToEnter = null, Quest questAvailableHere = null, Monster monsterLivingHere = null)
        {
            id = _id;
            name = _name;
            desc = _desc;
            ItemRequiredToEnter = itemRequiredToEnter;
            QuestAvailableHere = questAvailableHere;
            MonsterLivingHere = monsterLivingHere;
        }
        public Location(Location location) //Overload For location class
        {
            id = location.ID;
            name = location.Name;
            desc = location.Description;
            ItemRequiredToEnter = location.ItemRequiredToEnter;
            QuestAvailableHere = location.QuestAvailableHere;
            MonsterLivingHere = location.MonsterLivingHere;
        }

        public int ID { get { return id; } set { id = value; } }
        public string Name { get { return name; } set { name = value; } }
        public string Description { get { return desc; } set { desc = value; } }

        public Item ItemRequiredToEnter { get; set; }
        public Quest QuestAvailableHere { get; set; }
        public Monster MonsterLivingHere { get; set; }

        public List<Transition> Transitions { get; set; }

        public Location LocationToNorth { get; set; }
        public Location LocationToEast { get; set; }
        public Location LocationToSouth { get; set; }
        public Location LocationToWest { get; set; }
    }

    public class Transition
    {
        public Point Location { get; set; }
        public string RequiredFacingDirection { get; set; }
        public Location NextLocation { get; set; }

        public Transition(Point location, string facing, Location nextLoc)
        {
            Location = location;
            RequiredFacingDirection = facing;
            NextLocation = nextLoc;
        }
    }
}
