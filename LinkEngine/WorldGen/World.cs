using LinkEngine.Entities;
using LinkEngine.Gameplay.Items;
using LinkEngine.Gameplay.Quests;
using LinkEngine.Gameplay.Skills;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace LinkEngine.WorldGen
{
    public class World
    {
        /// <summary>
        /// All Tiles available to the game
        /// </summary>
        public static List<Tile> Tiles = new List<Tile>();

        /// <summary>
        /// All Biomes available to the game
        /// </summary>
        public List<Biome> Biomes = new List<Biome>();

        /// <summary>
        /// All Items available to the game
        /// </summary>
        public List<Item> Items = new List<Item>();

        /// <summary>
        /// All Enemies available to the game
        /// </summary>
        public List<Enemy> Enemies = new List<Enemy>();

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
        public List<Item> Craftable = new List<Item>();

        public Player Player { get; set; }

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
        /// Loads Text File with Items
        /// </summary>
        /// <param name="db">Text file with Information</param>
        public void LoadItemDatabase(string db)
        {
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                StreamReader reader = new StreamReader(assembly.GetManifestResourceStream(db));

                Items = new List<Item>();

                while (!reader.EndOfStream)
                {
                    // Grab these from the file
                    int id = int.Parse(reader.ReadLine());
                    string name = reader.ReadLine();
                    string plural = reader.ReadLine();
                    int cost = int.Parse(reader.ReadLine());

                    Items.Add(new Item(id, name, plural, cost));
                    reader.ReadLine();
                }
            }
            catch
            {

            }

        }

        /// <summary>
        /// Loads Text File with Tiles
        /// </summary>
        /// <param name="db">Text file with Information</param>
        public void LoadTileDatabase(string db)
        {
            try
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
                    reader.ReadLine();
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// Loads Text File with Biomes
        /// </summary>
        /// <param name="db">Text file with Information</param>
        public void LoadBiomeDatabase(string db)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            StreamReader reader = new StreamReader(assembly.GetManifestResourceStream(db));
            Biomes = new List<Biome>();

            while (!reader.EndOfStream)
            {
                // Grab these from the file
                int id = int.Parse(reader.ReadLine());
                string name = reader.ReadLine();

                int tile1 = int.Parse(reader.ReadLine());
                int tile2 = int.Parse(reader.ReadLine());
                int tile3 = int.Parse(reader.ReadLine());
                int tile4 = int.Parse(reader.ReadLine());
                int tile5 = int.Parse(reader.ReadLine());
                int tile6 = int.Parse(reader.ReadLine());
                int tile7 = int.Parse(reader.ReadLine());
                int tile8 = int.Parse(reader.ReadLine());
                int tile9 = int.Parse(reader.ReadLine());

                int Enemy1 = int.Parse(reader.ReadLine());
                int Enemy2 = int.Parse(reader.ReadLine());
                int Enemy3 = int.Parse(reader.ReadLine());

                Tile[] tileAry = new Tile[] { TileBy(tile1), TileBy(tile2), TileBy(tile3), TileBy(tile4), TileBy(tile5), TileBy(tile6), TileBy(tile7), TileBy(tile8), TileBy(tile9) };
                Biome[] biomeAry = new WorldGen.Biome[] { };

                Biomes.Add(new Biome(id, name, tileAry, biomeAry));

                reader.ReadLine();
            }
        }

        /// <summary>
        /// Loads Text File with Enemys
        /// </summary>
        /// <param name="db">Text file with Information</param>
        public void LoadEnemyDatabase(string db)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            StreamReader reader = new StreamReader(assembly.GetManifestResourceStream(db));

            Enemies = new List<Enemy>();

            while (!reader.EndOfStream)
            {
                // Grab these from the file
                int id = int.Parse(reader.ReadLine());
                string name = reader.ReadLine();
                int hp = int.Parse(reader.ReadLine());
                int maxhp = int.Parse(reader.ReadLine());
                int mana = int.Parse(reader.ReadLine());
                int maxmana = int.Parse(reader.ReadLine());
                int str = int.Parse(reader.ReadLine());
                int def = int.Parse(reader.ReadLine());
                int exp = int.Parse(reader.ReadLine());
                int gold = int.Parse(reader.ReadLine());
                int spawn = int.Parse(reader.ReadLine());
                reader.ReadLine();

                Enemies.Add(new Enemy(id, name, hp, maxhp, (short)str, (short)def));
            }
        }

        /// <summary>
        /// Finds an Enemy by its ID variable
        /// </summary>
        /// <param name="id">The ID of the enemy to find</param>
        /// <returns>Returns a copy of the enemy found, null if found nothing</returns>
        public Enemy EnemyBy(int id)
        {
            foreach (Enemy enemy in Enemies)
            {
                if (enemy.ID == id)
                {
                    return enemy;
                }
            }

            return null;
        }

        /// <summary>
        /// Finds a Tile based on it's ID variable
        /// </summary>
        /// <param name="id">The ID of the tile to find</param>
        /// <returns>Returns a copy of the tile found, null if found nothing</returns>
        public static Tile TileBy(int id)
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

        /// <summary>
        /// Finds an Item by its ID
        /// </summary>
        /// <param name="id">The ID of the Item to find</param>
        /// <returns>Returns a copy of the item found, null if found nothing</returns>
        public Item ItemBy(int id)
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
        /// <summary>
        /// Finds an Item by its Name
        /// </summary>
        /// <param name="name">The name of the item to find</param>
        /// <returns>Returns a copy of the item found, null if found nothing</returns>
        public Item ItemBy(string name)
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
                if (Ability.ID == id)
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
                if (Ability.Name == name)
                {
                    return Ability;
                }
            }
            return null;
        }
    }
}

