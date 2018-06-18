using System.Collections.Generic;
using System.IO;
using System.Reflection;
using LinkEngine.WorldGen;

namespace LinkEngine.RPG
{
    public static class World
    {
        public static List<RPGItem> Items = new List<RPGItem>();
        public static List<Monster> Monsters = new List<Monster>();
        public static List<Quest> Quests = new List<Quest>();
        public static List<Location> Locations = new List<Location>();
        public static List<NPC> NPCs = new List<NPC>();
        public static List<Tile> Tiles = new List<Tile>();
        public static List<Biome> Biomes = new List<Biome>();
        public static List<Ability> Abilities = new List<Ability>();
        public static List<RPGItem> Craftable = new List<RPGItem>();

        public static Character Player { get; set; }

        /// <summary>
        /// Loads Text File with Items
        /// </summary>
        /// <param name="db">Text file with Information</param>
        public static void LoadItemDatabase(string db)
        {
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                StreamReader reader = new StreamReader(assembly.GetManifestResourceStream(db));

                World.Items = new List<RPGItem>();

                while (!reader.EndOfStream)
                {
                    // Grab these from the file
                    int id = int.Parse(reader.ReadLine());
                    string name = reader.ReadLine();
                    string plural = reader.ReadLine();
                    int cost = int.Parse(reader.ReadLine());

                    World.Items.Add(new RPGItem(id, name, plural, cost));
                    reader.ReadLine();
                }
            }
            catch (System.Exception ex)
            {
                // System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// Loads Text File with Tiles
        /// </summary>
        /// <param name="db">Text file with Information</param>
        public static void LoadTileDatabase(string db)
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

                    World.Tiles.Add(new Tile(id, name, dense, 0, 0, type));
                    reader.ReadLine();
                }
            }
            catch (System.Exception ex)
            {
                // System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Loads Text File with Monsters
        /// </summary>
        /// <param name="db">Text file with Information</param>
        public static void LoadMonsterDatabase(string db)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            StreamReader reader = new StreamReader(assembly.GetManifestResourceStream(db));

            Monsters = new List<Monster>();

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

                Monsters.Add(new Monster(id, name, hp, maxhp, exp, gold, spawn));
            }
        }

        /// <summary>
        /// Loads Text File with NPCs
        /// </summary>
        /// <param name="db">Text file with Information</param>
        public static void LoadNPCDatabase(string db)
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
        /// Loads Text File with Biomes
        /// </summary>
        /// <param name="db">Text file with Information</param>
        public static void LoadBiomeDatabase(string db)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            StreamReader reader = new StreamReader(assembly.GetManifestResourceStream(db));
            World.Biomes = new List<Biome>();

            while (!reader.EndOfStream)
            {
                // Indicies for Biome Lists
                // --- South Facing Wall Tile [0]
                // --- North Facing Wall Tile [1]
                // --- EastWest Facing Wall Tile [2]
                // --- Floor Tile [3]
                // --- Chest Tile [4]
                // --- South Facing Door Tile [5]
                // --- North Facing Door Tile [6]
                // --- East Facing Door Tile [7]
                // --- West Facing Door Tile [8]

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

                int monster1 = int.Parse(reader.ReadLine());
                int monster2 = int.Parse(reader.ReadLine());
                int monster3 = int.Parse(reader.ReadLine());

                Tile[] tileAry = new Tile[] { World.TileByID(tile1), World.TileByID(tile2), World.TileByID(tile3), World.TileByID(tile4), World.TileByID(tile5), World.TileByID(tile6), World.TileByID(tile7), World.TileByID(tile8), World.TileByID(tile9) };
                Monster[] monsterAry = new Monster[] { World.MonsterByID(monster1), World.MonsterByID(monster2), World.MonsterByID(monster3) };
                Biome[] biomeAry = new WorldGen.Biome[] { };

                Biomes.Add(new Biome(id, name, tileAry, biomeAry));

                reader.ReadLine();
            }
        }

        public static RPGItem ItemByID(int id)
        {
            foreach (RPGItem item in Items)
            {
                if (item.ID == id)
                {
                    return item;
                }
            }

            return null;
        }
        public static RPGItem ItemByName(string name)
        {
            foreach (RPGItem item in Items)
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

        public static Ability AbilityByID(int id)
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
        public static Ability AbilityByName(string name)
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
