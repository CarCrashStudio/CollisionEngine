using System.Collections.Generic;

namespace LinkEngine.WorldGen
{
    class ProceduralGeneration
    {
        /*
         * Will Add a minecraft style 'Seed' System for generation where each number in 
         * a string of numbers indicates a certain action to happen in the generation.
         * System can be used for dungeon style games or games requiring infinite worlds
         */
        public string Seed { get; set; }
        System.Random rand = new System.Random();
        List<Chunk> ChunkList = new List<Chunk>();
        List<Biome> BiomeList = new List<Biome>();

        /// <summary>
        /// Generates a new spawn location based on seed
        /// </summary>
        /// <param name="seed">Supports the algorithim of the generation. 
        /// If no seed is provided, a new one will be generated</param>
        public ProceduralGeneration (string seed = null)
        {
            Seed = seed;

            // if no seed was established
            if (Seed == null)
            {
                // Generate a new seed
                Seed = GenerateSeed();
            }
            BuildSpawn();
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
                 * 1: Chunks should connect smoothly (0 - 1)
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
                if(i == 0)
                {
                    seed += rand.Next(1);
                }
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
            Chunk chnk = GenerateChunk ();

            // generate adjacent chunks
            chnk.toNorth = GenerateChunk();
            chnk.toSouth = GenerateChunk();
            chnk.toEast = GenerateChunk();
            chnk.toWest = GenerateChunk();

            // add chunks to the list
            ChunkList.Add(chnk);
            ChunkList.Add(chnk.toNorth);
            ChunkList.Add(chnk.toSouth);
            ChunkList.Add(chnk.toEast);
            ChunkList.Add(chnk.toWest);
        }

        /// <summary>
        /// GenerateChunk will populate the Tiles list inside its own chunk object.
        /// </summary>
        /// <returns></returns>
        Chunk GenerateChunk ()
        {
            // create a blank chunk object
            // create a blank tile object
            Chunk chnk = new WorldGen.Chunk();
            Tile tile;

            // get the biome info
            Biome biome = chnk.containsBiome;

            // loop for chunk size as x and y
            for (int y = 0; y < chnk.Size; y++)
            {
                for (int x = 0; x < chnk.Size; x++)
                {
                    // create a new random tile form biome list
                    tile = new Tile(biome.AvailibleTiles[rand.Next(biome.AvailibleTiles.Count)]);

                    // Give the tile its X and Y coordinates
                    tile.X = x;
                    tile.Y = y;

                    // store tile in chunk
                    chnk.Tiles.Add(tile);
                }
            }

            // return the finished chunk
            return chnk;
        }
    }
}
