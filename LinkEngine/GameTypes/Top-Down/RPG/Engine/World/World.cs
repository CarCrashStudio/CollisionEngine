using System.Collections.Generic;
using System.IO;
using System.Reflection;
using LinkEngine.WorldGen;

namespace LinkEngine.RPG
{
    public class World : WorldGen.World
    {
        /// <summary>
        /// AAll quests available to the game will be stored here
        /// </summary>
        public List<Quest> Quests = new List<Quest>();
        /// <summary>
        /// All locations available to the game will be stored here
        /// </summary>
        public List<Location> Locations = new List<Location>();
        /// <summary>
        /// All NPCs available to the game
        /// </summary>
        public List<NPC> NPCs = new List<NPC>();
        /// <summary>
        /// all Abilities available to the game
        /// </summary>
        public List<Ability> Abilities = new List<Ability>();
        /// <summary>
        /// All Craftable Items available to the game
        /// </summary>
        public List<RPGItem> Craftable = new List<RPGItem>();

        public Character Player { get; set; }

        /// <summary>
        /// Loads Text File with NPCs
        /// </summary>
        /// <param name="db">Text file with Information</param>
        public void LoadNPCDatabase(string db)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            StreamReader reader = new StreamReader(assembly.GetManifestResourceStream(db));
            Tiles = new List<Tile>();

            while (!reader.EndOfStream)
            {
                // Grab these from the file
                int id = int.Parse(reader.ReadLine());
                string name = reader.ReadLine();
                int dense = int.Parse(reader.ReadLine());
                string type = reader.ReadLine();

                Tiles.Add(new Tile(id, name, dense, 0, 0, type));
            }
        }

        /// <summary>
        /// Finds a Quest by its ID
        /// </summary>
        /// <param name="id">The ID of the Quest to find</param>
        /// <returns></returns>
        public Quest QuestByID(int id)
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

        /// <summary>
        /// Finds a Location by its ID
        /// </summary>
        /// <param name="id">The ID of the location to find</param>
        /// <returns></returns>
        public Location LocationByID(int id)
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

        /// <summary>
        /// Finds and NPC by its ID
        /// </summary>
        /// <param name="id">The ID of the NPC to find</param>
        /// <returns></returns>
        public NPC NPCByID(int id)
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

        /// <summary>
        /// Finds an Ability by its ID
        /// </summary>
        /// <param name="id">The ID of the Ability to find</param>
        /// <returns></returns>
        public Ability AbilityByID(int id)
        {
            foreach (Ability Ability in Abilities)
            {
                if(Ability.ID == id)
                {
                    return Ability;
                }
            }
            return null;
        }
        /// <summary>
        /// Finds an Ability by its Name
        /// </summary>
        /// <param name="name">The Name of the ability to find</param>
        /// <returns></returns>
        public Ability AbilityByName(string name)
        {
            foreach (Ability Ability in Abilities)
            {
                if(Ability.Name == name)
                {
                    return Ability;
                }
            }
            return null;
        }
    }
}
