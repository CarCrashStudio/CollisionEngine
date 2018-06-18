using LinkEngine.Entities;
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
        public static List<Biome> Biomes = new List<Biome>();

        /// <summary>
        /// All Items available to the game
        /// </summary>
        public static List<Item> Items = new List<Item>();

        /// <summary>
        /// All Enemies available to the game
        /// </summary>
        public List<Enemy> Enemies = new List<Enemy>();

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

                Items = new List<Item>();

                while (!reader.EndOfStream)
                {
                    // Grab these from the file
                    int id = int.Parse(reader.ReadLine());
                    string name = reader.ReadLine();
                    string plural = reader.ReadLine();
                    int cost = int.Parse(reader.ReadLine());

                    Items.Add(new Item(id, name, plural));
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

                    Tiles.Add(new Tile(id, name, dense, 0, 0, type));
                    reader.ReadLine();
                }
            }
            catch (System.Exception ex)
            {
                // System.Windows.Forms.MessageBox.Show(ex.Message);
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
            Biomes = new List<Biome>();

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

                int Enemy1 = int.Parse(reader.ReadLine());
                int Enemy2 = int.Parse(reader.ReadLine());
                int Enemy3 = int.Parse(reader.ReadLine());

                Tile[] tileAry = new Tile[] { TileByID(tile1), TileByID(tile2), TileByID(tile3), TileByID(tile4), TileByID(tile5), TileByID(tile6), TileByID(tile7), TileByID(tile8), TileByID(tile9) };
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
        /// <returns></returns>
        public Enemy EnemyByID(int id)
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
        /// <returns></returns>
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

        /// <summary>
        /// Finds an Item by its ID
        /// </summary>
        /// <param name="id">The ID of the Item to find</param>
        /// <returns></returns>
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
        /// <summary>
        /// Finds an Item by its Name
        /// </summary>
        /// <param name="name">The name of the item to find</param>
        /// <returns></returns>
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
    }
}
