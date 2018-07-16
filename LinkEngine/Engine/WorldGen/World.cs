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
        public List<Biome> Biomes = new List<Biome>();

        /// <summary>
        /// All Enemies available to the game
        /// </summary>
        public List<Enemy> Enemies = new List<Enemy>();

        /// <summary>
        /// All locations available to the game will be stored here
        /// </summary>
        public List<Location> Locations = new List<Location>();

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
    }
}

