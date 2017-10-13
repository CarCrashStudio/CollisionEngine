using System.Collections.Generic;
using System.Drawing;

namespace CsharpRPG.Engine
{
    public class Location
    {
        int id;
        string name;
        string desc;

        public int ID { get { return id; } set { id = value; } }
        public string Name { get { return name; } set { name = value; } }
        public string Description { get { return desc; } set { desc = value; } }
            
        public Item ItemRequiredToEnter { get; set; }
        public Quest QuestAvailableHere { get; set; }

        public Monster MonsterLivingHere { get; set; }
        public List<NPC> NPCsLivingHere { get; set; }

        public List<Transition> Transitions { get; set; }
        public List<Point> Boundries { get; set; }

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
            Boundries = new List<Point>();
            NPCsLivingHere = new List<NPC>();
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
            foreach (NPC npc in location.NPCsLivingHere)
            {
                NPCsLivingHere.Add(npc);
            }

            Boundries = new List<Point>();
            Boundries.Add(location.Boundries[0]);
            Boundries.Add(location.Boundries[1]);
        }
    }

    public class Transition
    {
        public Point Location { get; set; }
        public string RequiredFacingDirection { get; set; }
        public Location NextLocation { get; set; }
        public Point TargetTransition { get; set; }

        public Transition(Point location, string facing, Location nextLoc, Point target)
        {
            Location = location;
            RequiredFacingDirection = facing;
            NextLocation = nextLoc;
            TargetTransition = target;
        }
    }
}
