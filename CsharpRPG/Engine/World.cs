using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace RPG_Engine
{
    public class World
    {
        public List<Item> Items = new List<Item>();
        public List<Weapon> Weapons = new List<Weapon>();
        public List<Potion> Potions = new List<Potion>();
        public List<Monster> Monsters = new List<Monster>();
        public List<Quest> Quests = new List<Quest>();
        public List<Location> Locations = new List<Location>();
        public List<NPC> NPCs = new List<NPC>();
        public List<Tile> Tiles = new List<Tile>();
        public List<Biome> Biomes = new List<Biome>();

        public int WEAPON_ID_RUSTY_SWORD { get { return 1; } }
        public int WEAPON_ID_CRUDE_AX { get { return 2; } }

        public int POTION_ID_BASIC_HEALTH { get { return 101; } }
        public int POTION_ID_MED_HEALTH { get { return 102; } }
        public int POTION_ID_HIGH_HEALTH { get { return 103; } }

        public int ITEM_ID_SPIDER_SILK { get { return 201; } }

        public int MONSTER_ID_SPIDER { get { return 1; } }

        public int QUEST_ID_BUGSQUASHING { get { return 1; } }

        public int LOCATION_ID_HOUSE { get { return 1; } }
        public int LOCATION_ID_HOUSE_INSIDE { get { return 100; } }
        public int LOCATION_ID_FIELD { get { return 2; } }

        public int NPC_ID_BUGSQUASHER { get { return 1; } }

        public int TILE_ID_VOID { get { return 0; } }
        public int TILE_ID_GRASS { get { return 1; } }
        public int TILE_ID_DIRT { get { return 2; } }
        public int TILE_ID_WATER { get { return 3; } }
        public int TILE_ID_WOODFLOOR { get { return 100; } }

        public PictureBox charForm { get; set; }
        public WorldMap map { get; set; }
        public Combat combat { get; set; }
        public Character player { get; set; }
        public Hud HUD { get; set; }

        public RichTextBox Output { get; set; }
        public Label Stats { get; set; }
        public ListBox Inventory { get; set; }
        public DataGridView Journal { get; set; }


        public int MAX_MAP_SIZE { get { return 9; } }
        public int ICON_SIZE { get { return 32; } }
        public int WIDTH { get { return (MAX_MAP_SIZE + 1) * ICON_SIZE; } }
        public int HEIGHT { get { return (MAX_MAP_SIZE + 1) * ICON_SIZE; } }
        public Point CENTER { get { return new Point((WIDTH / 2) / 32, (HEIGHT / 2) / 32); } }

        public World(PictureBox _charForm)
        {
            charForm = _charForm;
            charForm.Size = new Size(WIDTH, HEIGHT);

            PopulateItems();
            PopulateWeapons();
            PopulatePotions();
            PopulateQuests();
            PopulateMonsters();
            PopulateLocations();
            PopulateNPCs();           
            PopulateTiles();
        }

        void PopulateItems()
        {
            Item spiderSilk = new RPG_Engine.Item(ITEM_ID_SPIDER_SILK, "Spider Silk", "Spider Silk", 5);

            Items.Add(spiderSilk);  
        }
        void PopulateWeapons()
        {
            Weapon rustySword = new RPG_Engine.Weapon(WEAPON_ID_RUSTY_SWORD, "Rusty Sword", "Rusty Swords", 1, 5, 5, true, false);
            Weapon crudeAx = new RPG_Engine.Weapon(WEAPON_ID_CRUDE_AX, "Crude Ax", "Crude Axes", 1, 7, 10, true, false);

            Weapons.Add(rustySword);
            Weapons.Add(crudeAx);
        }
        void PopulatePotions()
        {
            Potion basicHealthPotion = new RPG_Engine.Potion(POTION_ID_BASIC_HEALTH, "Basic Health Potion", "Basic Health Potions", 15, 30);
            Potion medHealthPotion = new RPG_Engine.Potion(POTION_ID_BASIC_HEALTH, "Better Health Potion", "Better Health Potions", 30, 60);
            Potion HighHealthPotion = new RPG_Engine.Potion(POTION_ID_BASIC_HEALTH, "Best Health Potion", "Best Health Potions", 45, 90);

            Potions.Add(basicHealthPotion);
            Potions.Add(medHealthPotion);
            Potions.Add(HighHealthPotion);
        }
        void PopulateQuests()
        {
            Quest bugSquashing = new RPG_Engine.Quest(QUEST_ID_BUGSQUASHING, "Bug Squasher", "Kill 5 Spiders", 10, 5);
            bugSquashing.QuestCompletionItems.Add(new QuestCompletionItem(ItemByID(ITEM_ID_SPIDER_SILK), 2));
            bugSquashing.RewardItem = PotionByID(POTION_ID_BASIC_HEALTH);

            Quests.Add(bugSquashing);
        }
        void PopulateLocations()
        {
            Location homeInside = new Location(LOCATION_ID_HOUSE_INSIDE, "Inside", "You should really clean this place up.");
            homeInside.MonsterLivingHere = null;

            Location field = new RPG_Engine.Location(LOCATION_ID_FIELD, "Fields", "This place is swarmed with spiders");
            field.MonsterLivingHere = MonsterByID(MONSTER_ID_SPIDER);
            field.Transitions = new List<Transition>();

            Location home = new Location(LOCATION_ID_HOUSE, "Your House", "You live here but now you should go adventuring");
            home.MonsterLivingHere = null;
            home.Transitions = new List<Transition>();



            Locations.Add(home);
            Locations.Add(homeInside);
            Locations.Add(field);

            field.LocationToSouth = LocationByID(LOCATION_ID_HOUSE);
            field.Transitions.Add(new Transition(new Point(5, 10), "South", field.LocationToSouth));
            home.LocationToNorth = LocationByID(LOCATION_ID_FIELD);
            home.Transitions.Add(new Transition(new Point(5, -1), "North", home.LocationToNorth));
        }
        void PopulateNPCs()
        {
            NPC bugSquasher = new RPG_Engine.NPC(NPC_ID_BUGSQUASHER, "Bug Squasher", new System.Drawing.Bitmap("icons/NPC.bmp"), new System.Drawing.Point(0, 0), QuestByID(QUEST_ID_BUGSQUASHING), charForm);

            NPCs.Add(bugSquasher);
        }
        void PopulateMonsters()
        {
            
            Monster spider = new RPG_Engine.Monster(MONSTER_ID_SPIDER, "Spider", new System.Drawing.Point(6, 5), 10, 10, 0, 0, 5, 5, 10, 5, 50, new System.Drawing.Bitmap("icons/spider.png"), this, this.charForm);
            spider.LootTable.Add(new LootItem(ItemByID(ITEM_ID_SPIDER_SILK), 100, true));
            spider.MaxHealth = 10;
            spider.Health = spider.MaxHealth;
            spider.MaxMana = 0;
            spider.MaximumDamage = 5;
            spider.MaximumDefense = 5;

            Monsters.Add(spider);
        }
        void PopulateTiles()
        {
            Tile grass = new Tile(TILE_ID_GRASS, "Grass", 0, new Point(0, 0), new Bitmap("icons/grass.bmp"));
            Tile dirt = new Tile(TILE_ID_DIRT, "Dirt", 0, new Point(0, 0), new Bitmap("icons/dirt.bmp"));
            Tile water = new Tile(TILE_ID_WATER, "Water", 1, new Point(0, 0), new Bitmap("icons/water.bmp"));
            Tile blank = new Tile(TILE_ID_VOID, "Void", 1, new Point(0, 0), new Bitmap("icons/void.bmp"));

            Tile woodFloor = new Tile(TILE_ID_WOODFLOOR, "Floor", 0, new Point(0, 0), new Bitmap("icons/woodfloor.bmp"));

            Tiles.Add(grass);
            Tiles.Add(dirt);
            Tiles.Add(water);
            Tiles.Add(blank);

            Tiles.Add(woodFloor);
        }

        public Item ItemByID(int id)
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

        public Weapon WeaponByID(int id)
        {
            foreach (Weapon weapon in Weapons)
            {
                if (weapon.ID == id)
                {
                    return weapon;
                }
            }

            return null;
        }
        public Weapon WeaponByName(string name)
        {
            foreach (Weapon weapon in Weapons)
            {
                if (weapon.Name == name)
                {
                    return weapon;
                }
            }

            return null;
        }

        public Potion PotionByID(int id)
        {
            foreach (Potion potion in Potions)
            {
                if (potion.ID == id)
                {
                    return potion;
                }
            }

            return null;
        }
        public Potion PotionByName(string name)
        {
            foreach (Potion potion in Potions)
            {
                if (potion.Name == name)
                {
                    return potion;
                }
            }

            return null;
        }

        public Monster MonsterByID(int id)
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
        public Monster MonsterByLocation(Point location)
        {
            foreach (Monster monster in Monsters)
            {
                if (monster.Location == location)
                {
                    return monster;
                }
            }

            return null;
        }

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

        public Tile TileByID(int id)
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

        public void Save()
        {
            //StreamWriter writer = File.OpenWrite("PlayerFiles/stats.text");

            //writer.WriteLine(player.Name);
            //writer.WriteLine(player.Level);
            //writer.WriteLine(player.MaximumDamage);
            //writer.WriteLine(player.MaximumDefense);
            //writer.WriteLine(player.Health);
            //writer.WriteLine(player.Mana);
            //writer.WriteLine(player.Class);
            //writer.WriteLine(player.Exp);
            //writer.WriteLine(player.MaxExp);
        }
        public void Load()
        {

        }
    }
}
