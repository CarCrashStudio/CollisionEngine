using System.Collections.Generic;

namespace LinkEngine.WorldGen
{
    public class ProceduralGeneration
    {
        enum TileTypes { PASSABLE=0, UNPASSABLE=1, WATER=2}
        /*
         * Will Add a minecraft style 'Seed' System for generation where each number in 
         * a string of numbers indicates a certain action to happen in the generation.
         * System can be used for dungeon style games or games requiring infinite worlds
         */
        public string Seed { get; set; }
        public short ViewDistance { get; set; }

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
            // Build the starting 3x3 chunk of map
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
            chnk.toNorth = GenerateChunk("North", chnk);
            chnk.toSouth = GenerateChunk("South", chnk);
            chnk.toEast = GenerateChunk("East", chnk);
            chnk.toWest = GenerateChunk("West", chnk);

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
        /// <param name="adajcent">The side of the current chunk this new chunk is adjacent to</param>
        /// <param name="previousChunk">The previous chunk to match current chunk to. If null, function will create chunk as normal</param>
        /// <returns></returns>
        public Chunk GenerateChunk (string adajcent = null, Chunk previousChunk = null)
        {
            // create a blank chunk object
            // create a blank tile object
            Chunk chnk = new Chunk();
            Tile tile;

            // get the biome info
            Biome biome = chnk.containsBiome;

            // loop for chunk size as x and y
            for (int y = 0; y < chnk.Size; y++)
            {
                for (int x = 0; x < chnk.Size; x++)
                {
                    // create a new random tile form biome list
                    tile = new Tile(biome.availableTiles[rand.Next(biome.availableTiles.Count)]);

                    // Give the tile its X and Y coordinates
                    tile.X = x;
                    tile.Y = y;

                    // store tile in chunk
                    chnk.Tiles.Add(tile);
                }
            }

            // Check if this chunk is adajcent to another
            if (adajcent != null)
            {
                switch (adajcent)
                {
                    case "North":
                        chnk.toSouth = previousChunk;
                        break;
                    case "South":
                        chnk.toNorth = previousChunk;
                        break;
                    case "East":
                        chnk.toWest = previousChunk;
                        break;
                    case "West":
                        chnk.toEast = previousChunk;
                        break;
                }
            }

            // return the finished chunk
            return chnk;
        }

        /*
         * An algorithim needs to be put in place to eliminate the randomness of the current system.
         * Function should check the tile type of the prev
         */
    }
}
