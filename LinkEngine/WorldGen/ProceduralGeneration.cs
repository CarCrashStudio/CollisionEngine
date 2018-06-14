using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace LinkEngine.WorldGen
{
    class ProceduralGeneration
    {
        /*
         * Will Add a minecraft style 'Seed' System for generation where each number in 
         * a string of numbers indicates a certain action to happen in the generation.
         * Sytem can be used for duungeon style games or games requiring infinite worlds
         */
        public string Seed { get; set; }
        System.Random rand = new System.Random();
        List<Chunk> ChunkList = new List<Chunk>();
        List<Biome> BiomeList = new List<Biome>();

        StreamWriter writer;

        public ProceduralGeneration (string seed = null)
        {
            Seed = seed;

            // if no seed was established
            if (Seed == null)
            {
                // Generate a new seed
                Seed = GenerateSeed();
            }
        }

        /// <summary>
        /// Creates a new seed string based on RNG
        /// </summary>
        /// <returns></returns>
        string GenerateSeed ()
        {
            string seed = "";
            // seeds will be 10 characters long. 
            // each character can be 0-9 and will determine the generator action.
            for (int i = 0; i < 10; i++)
            {
                /* Seed determines
                 * 1: 
                 * 2:
                 * 3:
                 * 4: 
                 * 5:
                 * 6:
                 * 7:
                 * 8:
                 * 9:
                 * 10:
                 */
                seed += rand.Next(9).ToString();
            }
            return seed;
        }

        /// <summary>
        /// BuildWorld will create a text file containing the IDs of all tiles to be generated in game.
        /// The file should be saved to the projects resource manifest
        /// </summary>
        void BuildSpawn ()
        {
            // world should be built in chunks
            // Chunks will have a size of 10 x 10 tiles

            // create a new blank chunk
            Chunk chnk = new Chunk();

            // loop for chnk and its adjacent chunks
            for (int i = 0; i < 5; i++)
            {
                // assign the chunk a biome to pull tiles from
                chnk.containsBiome = BiomeList[rand.Next(BiomeList.Count)];

                // randomly select a tile
                // this should follow seed generation once the system is implemented better
                Tile tile = new Tile(chnk.containsBiome.AvailibleTiles[rand.Next(chnk.containsBiome.AvailibleTiles.Count)]);
                chnk.Tiles.Add(tile);

                // write id to file
                Assembly assembly = Assembly.GetExecutingAssembly();
                writer = new StreamWriter(assembly.GetManifestResourceStream("world.txt"));

                writer.WriteLine(tile.ID);
            }
        }
    }
}
