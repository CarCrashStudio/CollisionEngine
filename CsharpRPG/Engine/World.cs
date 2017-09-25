using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CsharpRPG.Engine
{
    public class World
    {
        public string FilePath = "[ProgramFilesFolder]\\Rogue\\Input\\";

        public List<Item> Items = new List<Item>();
        public List<Monster> Monsters = new List<Monster>();
        public List<Quest> Quests = new List<Quest>();
        public List<Location> Locations = new List<Location>();
        public List<NPC> NPCs = new List<NPC>();
        public List<Tile> Tiles = new List<Tile>();
        public List<Biome> Biomes = new List<Biome>();
        public List<HUDObject> HUDObjects = new List<HUDObject>();
        public List<HUDObject> Clickables = new List<HUDObject>();
        public List<Skill> Skills = new List<Skill>();

        public int WEAPON_ID_RUSTY_SWORD { get { return 1; } }
        public int WEAPON_ID_CRUDE_AX { get { return 2; } }
        public int WEAPON_ID_BOW { get { return 3; } }

        public int POTION_ID_BASIC_HEALTH { get { return 101; } }
        public int POTION_ID_MED_HEALTH { get { return 102; } }
        public int POTION_ID_HIGH_HEALTH { get { return 103; } }

        public int ITEM_ID_SPIDER_SILK { get { return 201; } }
        public int ITEM_ID_STICK { get { return 202; } }

        public int MONSTER_ID_SPIDER { get { return 1; } }

        public int QUEST_ID_BUGSQUASHING { get { return 1; } }

        public int LOCATION_ID_HOUSE { get { return 1; } }
        public int LOCATION_ID_HOUSE_INSIDE { get { return 100; } }
        public int LOCATION_ID_FIELD { get { return 2; } }

        public int NPC_ID_BUGSQUASHER { get { return 1; } }
        public int NPC_ID_JOHNRIED { get { return 2; } }
        public int NPC_ID_SHOPKEEP { get { return 3; } }

        public int TILE_ID_VOID { get { return 0; } }
        public int TILE_ID_GRASS { get { return 1; } }
        public int TILE_ID_DIRT { get { return 2; } }
        public int TILE_ID_WATER { get { return 3; } }
        public int TILE_ID_WOODFLOOR { get { return 100; } }
        public int TILE_ID_CROP { get { return 203; } }
        public int TILE_ID_YOURHOUSE { get { return 1002; } }
        public int TILE_ID_BARN { get { return 1003; } }

        public int SKILL_ID_ATTACK { get { return 0; } }
        public int SKILL_ID_BURN { get { return 1; } }
        public int SKILL_ID_WEAKNESS { get { return 3; } }

        public PictureBox HudForm { get; set; }
        public WorldMap map { get; set; }
        public Combat combat { get; set; }
        public Character player { get; set; }
        public Hud HUD { get; set; }

        public RichTextBox Output { get; set; }
        public Label Stats { get; set; }
        public ListBox Inventory { get; set; }
        public DataGridView Journal { get; set; }

        public InventoryForm inventory;
        public CharacterForm charSheet;

        // COMBAT SCREEN
        public HUDObject PHealthCombat { get; set; }
        public HUDObject DHealthCombat { get; set; }

        public Bitmap strImg;
        public Bitmap defImg;

        public int MAX_MAP_SIZE { get { return 39; } }
        public int ICON_SIZE { get { return 32; } }
        public int WIDTH { get { return (MAX_MAP_SIZE + 1) * ICON_SIZE; } }
        public int HEIGHT { get { return (MAX_MAP_SIZE + 1) * ICON_SIZE; } }
        public static int fontSize { get { return 16; } }
        public Point CENTER 
        {
            get
            {
                if (!(WIDTH > HudForm.Width) || !(HEIGHT > HudForm.Height))
                {
                    return new Point((WIDTH / 2) / 32, (HEIGHT / 2) / 32);
                }
                else { return new Point(HudForm.Width / 2, HudForm.Height / 2); }
            }
        }
        public enum TileType
        {
            Ground = 0,
            Path = 1,
            Building = 2,
            Deco = 3
        }
        int PbOffset = 0;

        Screen screen = Screen.PrimaryScreen;

        public World(DataGridView journal, RichTextBox output, InventoryForm inv, CharacterForm chara, PictureBox _HudForm, Bitmap _CharStatBar, Bitmap _CharImgBox, Bitmap _strImg, Bitmap _defImg, Character _player, int pboffset)
        {
            HudForm = _HudForm;
            player = _player;
            player.world = this;
            Journal = journal;
            Output = output;

            inventory = inv;
            charSheet = chara;

            PbOffset = pboffset;

            PopulateItems();
            PopulateWeapons();
            PopulatePotions();
            PopulateQuests();
            PopulateSkills();
            PopulateMonsters();
            PopulateNPCs();
            PopulateLocations();
            PopulateTiles();
            PopulateHUDObjects(_CharStatBar, _CharImgBox, _strImg, _defImg);

            map = new WorldMap("worldmap", this);
        }

        void PopulateItems()
        {
            Item spiderSilk = new Item(ITEM_ID_SPIDER_SILK, "Spider Silk", "Spider Silk", 5, new Bitmap(Properties.Resources.Spider_Silk), this);
            Item stick = new Item(ITEM_ID_STICK, "Stick", "Sticks", 5, new Bitmap(Properties.Resources.Spider_Silk), this);

            Items.Add(spiderSilk);
            Items.Add(stick);  
        }
        void PopulateWeapons()
        {
            Weapon rustySword = new Weapon(WEAPON_ID_RUSTY_SWORD, "Rusty Sword", "Rusty Swords", 1, 5, 5, (int)Character.Slot.MainHand, new Bitmap(Properties.Resources.Rusty_Sword), this);
            Weapon crudeAx = new Weapon(WEAPON_ID_CRUDE_AX, "Crude Ax", "Crude Axes", 1, 7, 10, (int)Character.Slot.MainHand, new Bitmap(Properties.Resources.Crude_Ax), this);
            Weapon bow = new Weapon(WEAPON_ID_BOW, "Bow", "Bows", 1, 5, 5, (int)Character.Slot.MainHand, new Bitmap(Properties.Resources.Rusty_Sword), this);
            bow.Recipe.Add(new CraftingItem(ItemByID(ITEM_ID_STICK), 3));
            bow.Recipe.Add(new CraftingItem(ItemByID(ITEM_ID_SPIDER_SILK), 3));

            Items.Add(rustySword);
            Items.Add(crudeAx);
        }
        void PopulatePotions()
        {
            Potion basicHealthPotion = new Potion(POTION_ID_BASIC_HEALTH, "Basic Health Potion", "Basic Health Potions", 15, 30, new Bitmap(Properties.Resources.Basic_Health_Potion), this);
            Potion medHealthPotion = new Potion(POTION_ID_BASIC_HEALTH, "Better Health Potion", "Better Health Potions", 30, 60, new Bitmap(Properties.Resources.Better_Health_Potion), this);
            Potion HighHealthPotion = new Potion(POTION_ID_BASIC_HEALTH, "Best Health Potion", "Best Health Potions", 45, 90, new Bitmap(Properties.Resources.Best_Health_Potion), this);

            Items.Add(basicHealthPotion);
            Items.Add(medHealthPotion);
            Items.Add(HighHealthPotion);
        }
        void PopulateQuests()
        {
            Quest bugSquashing = new Quest(QUEST_ID_BUGSQUASHING, "Bug Squasher", "Kill 5 Spiders", 10, 5);
            bugSquashing.QuestCompletionItems = new List<QuestCompletionItem>();
            bugSquashing.QuestCompletionItems.Add(new QuestCompletionItem(ItemByID(ITEM_ID_SPIDER_SILK), 2));
            bugSquashing.RewardItem = ItemByID(POTION_ID_BASIC_HEALTH);

            Quests.Add(bugSquashing);
        }
        void PopulateNPCs()
        {
            NPC bugSquasher = new NPC(NPC_ID_BUGSQUASHER, "Bug Squasher", new System.Drawing.Bitmap(Properties.Resources.NPC), new System.Drawing.Point(7, 2), new Quest(QuestByID(QUEST_ID_BUGSQUASHING)), null, this);
            NPCs.Add(bugSquasher);

            Shop shop = new Shop(this);
            shop.Inventory.Add(new InventoryItem(ItemByID(201), 4));

            NPC ShopKeep = new NPC(NPC_ID_SHOPKEEP, "Shop Keep", new Bitmap(Properties.Resources.NPC), new Point(8, 4), null, shop, this);
            NPCs.Add(ShopKeep);

            NPC JohnRied = new NPC(NPC_ID_JOHNRIED, "John Ried", new Bitmap(1, 1), new Point(), null, null, this);
            NPCs.Add(JohnRied);
        }
        void PopulateLocations()
        {
            Location homeInside = new Location(LOCATION_ID_HOUSE_INSIDE, "Inside", "You should really clean this place up.");
            homeInside.MonsterLivingHere = null;
            homeInside.Transitions = new List<Transition>();

            Location field = new Location(LOCATION_ID_FIELD, "Fields", "This place is swarmed with spiders");
            field.MonsterLivingHere = MonsterByID(MONSTER_ID_SPIDER);

            field.Boundries.Add(new Point(0, 0));
            field.Boundries.Add(new Point(19, 19));

            field.NPCsLivingHere.Add(new NPC(NPCByID(NPC_ID_BUGSQUASHER)));
            field.NPCsLivingHere.Add(new NPC(NPCByID(NPC_ID_SHOPKEEP)));

            field.Transitions = new List<Transition>();

            Location home = new Location(LOCATION_ID_HOUSE, "Your House", "You live here but now you should go adventuring");
            home.MonsterLivingHere = null;
            home.Transitions = new List<Transition>();
            home.Boundries.Add(new Point(0, 20));
            home.Boundries.Add(new Point(19, 39));


            Locations.Add(home);
            Locations.Add(homeInside);
            Locations.Add(field);

            Locations[2].LocationToSouth = LocationByID(LOCATION_ID_HOUSE);
            Locations[2].Transitions.Add(new Transition(new Point(6, 20), "South", field.LocationToSouth, new Point(6,-1)));
            Locations[1].Transitions.Add(new Transition(new Point(6, 18), "South", Locations[0], new Point(3, 8)));
            Locations[0].LocationToNorth = LocationByID(LOCATION_ID_FIELD);
            Locations[0].Transitions.Add(new Transition(new Point(6, -1), "North", home.LocationToNorth, new Point(6,20)));
            Locations[0].Transitions.Add(new Transition(new Point(3, 8), "North", Locations[1], new Point(6, 18)));
        }
        void PopulateMonsters()
        {
            
            Monster spider = new Monster(MONSTER_ID_SPIDER, "Spider", new System.Drawing.Point(6, 5), 10, 10, 0, 0, 5, 5, 10, 5, 50, new System.Drawing.Bitmap(Properties.Resources.spider), this, this.HudForm);
            spider.LootTable.Add(new LootItem(ItemByID(ITEM_ID_SPIDER_SILK), 100, true));

            Monsters.Add(spider);
        }
        void PopulateTiles()
        {
            Tile grass = new Tile(TILE_ID_GRASS, "Grass", 0, new Point(0, 0), new Bitmap(CsharpRPG.Properties.Resources.grass), TileType.Ground.ToString());
            Tile dirt = new Tile(TILE_ID_DIRT, "Dirt", 0, new Point(0, 0), new Bitmap(Properties.Resources.dirt), TileType.Path.ToString());
            Tile water = new Tile(TILE_ID_WATER, "Water", 1, new Point(0, 0), new Bitmap(Properties.Resources.water), TileType.Ground.ToString());
            Tile blank = new Tile(TILE_ID_VOID, "Void", 1, new Point(0, 0), new Bitmap(Properties.Resources._void), TileType.Ground.ToString());

            Tile woodFloor = new Tile(TILE_ID_WOODFLOOR, "Floor", 0, new Point(0, 0), new Bitmap(Properties.Resources.woodfloor), TileType.Ground.ToString());

            Tile crop = new Tile(TILE_ID_CROP, "Crop", 1, new Point(), new Bitmap(Properties.Resources.Crop), TileType.Deco.ToString());

            Tile YourHouse = new Tile(TILE_ID_YOURHOUSE, "YourHouse", 1, new Point(0, 0), new Bitmap(Properties.Resources.YourHouse), TileType.Building.ToString());
            Tile barn = new Tile(TILE_ID_BARN, "Barn", 1, new Point(), new Bitmap(Properties.Resources.Barn), TileType.Building.ToString());

            Tiles.Add(grass);
            Tiles.Add(dirt);
            Tiles.Add(water);
            Tiles.Add(blank);

            Tiles.Add(woodFloor);

            Tiles.Add(crop);

            Tiles.Add(YourHouse);
            Tiles.Add(barn);
        }
        void PopulateSkills()
        {
            Skills = new List<Skill>();

            Skill Attack = new Skill(SKILL_ID_ATTACK, "Attack", new Bitmap(32, 32), "Health", 0, 0);
            Skill Burn = new Skill(SKILL_ID_BURN, "Burn", new Bitmap(32, 32), "Health", -5, 2);
            Skill Weakness = new Skill(SKILL_ID_WEAKNESS, "Weakness", new Bitmap(32, 32), "Strength", -5, (int)Skill.Types.Debuff);

            Skills.Add(Attack);
            Skills.Add(Burn);
            Skills.Add(Weakness);
        }
        void PopulateHUDObjects(Bitmap _CharStatBar, Bitmap _CharImgBox, Bitmap _strImg, Bitmap _defImg)
        {
            HUD = new Hud(this, PbOffset);

            List<Point> temp = new List<Point>();
            temp.Add(new Point(0, PbOffset));
            temp.Add(new Point(temp[0].X + _CharImgBox.Width, temp[0].Y + _CharImgBox.Height));

            HUDObject CharImgBox = new HUDObject(temp, _CharImgBox);

            temp = new List<Point>();
            temp.Add(new Point(CharImgBox.Boundries[0].X + 10, CharImgBox.Boundries[0].Y + 10));
            HUDObject CharImg = new HUDObject(temp, player.Image);

            temp = new List<Point>();
            temp.Add(new Point(CharImgBox.Boundries[1].X, PbOffset));
            temp.Add(new Point(temp[0].X + _CharStatBar.Width, temp[0].Y + _CharStatBar.Height));
            HUDObject CharStatBar = new HUDObject(temp, _CharStatBar);

            temp = new List<Point>();
            temp.Add(new Point(CharStatBar.FindCenterofBounds().X - 45, (CharStatBar.Boundries[1].Y - 40)));
            HUDObject MainHealthBar = new HUDObject(temp, new Bitmap(Properties.Resources.HealthBar10));

            temp = new List<Point>();
            temp.Add(new Point(CharStatBar.FindCenterofBounds().X - 45, CharStatBar.Boundries[1].Y - 25));
            HUDObject MainExpBar = new HUDObject(temp, new Bitmap(Properties.Resources.ExpBar__10_));

            temp = new List<Point>();
            temp.Add(new Point(MainHealthBar.Boundries[0].X, MainHealthBar.Boundries[0].Y - 80));
            HUDObject NameLevelString = new HUDObject(temp, null, player.Name + " (" + player.Level + ")");

            temp = new List<Point>();
            temp.Add(new Point(MainHealthBar.Boundries[0].X, NameLevelString.Boundries[0].Y + 25));
            HUDObject Class = new HUDObject(temp, null, player.Class);

            temp = new List<Point>();
            temp.Add(new Point(MainHealthBar.Boundries[0].X, NameLevelString.Boundries[0].Y + 45));
            HUDObject Strength = new HUDObject(temp, _strImg, ": " + player.Strength.ToString());

            temp = new List<Point>();
            temp.Add(new Point(MainHealthBar.Boundries[0].X + 70, NameLevelString.Boundries[0].Y + 45));
            HUDObject Defense = new HUDObject(temp, _defImg, ": " + player.Defense.ToString());

            HUDObjects.Add(CharImgBox);
            HUDObjects.Add(CharImg);
            HUDObjects.Add(CharStatBar);
            HUDObjects.Add(MainHealthBar);
            HUDObjects.Add(MainExpBar);
            HUDObjects.Add(NameLevelString);
            HUDObjects.Add(Class);
            HUDObjects.Add(Strength);
            HUDObjects.Add(Defense);

            // Clickables
            temp = new List<Point>();
            temp.Add(new Point(0, (screen.Bounds.Height - 32) + PbOffset));
            temp.Add(new Point(32, screen.Bounds.Height));
            HUDObject InventoryButton = new HUDObject(temp, new Bitmap(Properties.Resources.bagbutton));
            InventoryButton.Name = "Bag";
            Clickables.Add(InventoryButton);

            temp = new List<Point>();
            temp.Add(new Point(InventoryButton.Boundries[1].X, (screen.Bounds.Height - 32) + PbOffset));
            temp.Add(new Point(temp[0].X + 32, screen.Bounds.Height));
            HUDObject StatsButon = new HUDObject(temp, new Bitmap(Properties.Resources.statbutton));
            StatsButon.Name = "Stats";
            Clickables.Add(StatsButon);

            temp = new List<Point>();
            temp.Add(new Point(screen.Bounds.Width - 32, (screen.Bounds.Height - 32) - PbOffset));
            temp.Add(new Point(screen.Bounds.Width, screen.Bounds.Height - PbOffset));
            HUDObject CloseButton = new HUDObject(temp, new Bitmap(Properties.Resources.exitbutton));
            CloseButton.Name = "Close";
            Clickables.Add(CloseButton);

            temp = new List<Point>();
            temp.Add(new Point(0, 0));
            PHealthCombat = new HUDObject(temp);
            DHealthCombat = new HUDObject(temp);
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
        public Item ItemByName(string name)
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

        public Skill SkillByID(int id)
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
        public Skill SkillByName(string name)
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
