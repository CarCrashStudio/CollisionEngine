using System.Collections.Generic;

namespace RPG.Engine
{
    public static class World
    {
        public static List<Item> Items = new List<Item>();
        public static List<Monster> Monsters = new List<Monster>();
        public static List<Quest> Quests = new List<Quest>();
        public static List<Location> Locations = new List<Location>();
        public static List<NPC> NPCs = new List<NPC>();
        public static List<Tile> Tiles = new List<Tile>();
        public static List<Biome> Biomes = new List<Biome>();
        public static List<Skill> Skills = new List<Skill>();
        public static List<Item> Craftable = new List<Item>();

        public static WorldMap Map { get; set; }
        public static Character Player { get; set; }

        public static Item ItemByID(int id)
        {
            foreach (Item item in Items)
            {
                if (item.ID == id)
                {
                    return item;
                }
            }

            return null;
        }
        public static Item ItemByName(string name)
        {
            foreach (Item item in Items)
            {
                if (item.Name == name)
                {
                    return item;
                }
            }

            return null;
        }

        public static Monster MonsterByID(int id)
        {
            foreach (Monster monster in Monsters)
            {
                if (monster.ID == id)
                {
                    return monster;
                }
            }

            return null;
        }

        public static Quest QuestByID(int id)
        {
            foreach (Quest quest in Quests)
            {
                if (quest.ID == id)
                {
                    return quest;
                }
            }

            return null;
        }

        public static Location LocationByID(int id)
        {
            foreach (Location location in Locations)
            {
                if (location.ID == id)
                {
                    return location;
                }
            }

            return null;
        }

        public static NPC NPCByID(int id)
        {
            foreach (NPC npc in NPCs)
            {
                if (npc.ID == id)
                {
                    return npc;
                }
            }

            return null;
        }

        public static Tile TileByID(int id)
        {
            foreach (Tile tile in Tiles)
            {
                if (tile.ID == id)
                {
                    return tile;
                }
            }

            return null;
        }

        public static Skill SkillByID(int id)
        {
            foreach (Skill skill in Skills)
            {
                if(skill.ID == id)
                {
                    return skill;
                }
            }
            return null;
        }
        public static Skill SkillByName(string name)
        {
            foreach (Skill skill in Skills)
            {
                if(skill.Name == name)
                {
                    return skill;
                }
            }
            return null;
        }
    }
}
