using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CsharpRPG.Engine
{
    public class World
    {
        public List<Item> Items = new List<Item>();
        public List<Monster> Monsters = new List<Monster>();
        public List<Quest> Quests = new List<Quest>();
        public List<Location> Locations = new List<Location>();
        public List<NPC> NPCs = new List<NPC>();
        public List<Tile> Tiles = new List<Tile>();
        public List<Biome> Biomes = new List<Biome>();
        public List<HUDObject> HUDObjects = new List<HUDObject>();
        public List<Skill> Skills = new List<Skill>();

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

        public int SKILL_ID_ATTACK { get { return 0; } }
        public int SKILL_ID_BURN { get { return 1; } }

        public PictureBox HudForm { get; set; }
        public WorldMap map { get; set; }
        public Combat combat { get; set; }
        public Character player { get; set; }
        public Hud HUD { get; set; }

        public RichTextBox Output { get; set; }
        public Label Stats { get; set; }
        public ListBox Inventory { get; set; }
        public DataGridView Journal { get; set; }

        public List<HUDObject> Clickables { get; set; }
        public List<HUDObject> InventoryItems { get; set; }

        // HUD STATS
        public HUDObject CharImgBox { get; set; }
        public HUDObject CharImg { get; set; }
        public HUDObject CharStatBar { get; set; }
        public HUDObject MainHealthBar { get; set; }
        public HUDObject MainExpBar { get; set; }
        public HUDObject NameLevelString { get; set; }
        public HUDObject Class { get; set; }
        public HUDObject Strength { get; set; }
        public HUDObject Defense { get; set; }
        public HUDObject InventoryItem { get; set; }
        public HUDObject Gold { get; set; }

        // HUD INVENTORIES
        public HUDObject InventoryBox { get; set; }
        public HUDObject QuestBox { get; set; }

        // HUD BUTTONS
        public HUDObject InventoryButton { get; set; }
        public HUDObject CloseButton { get; set; }

        // COMBAT SCREEN
        public HUDObject PHealthCombat { get; set; }
        public HUDObject DHealthCombat { get; set; }

        public Bitmap strImg;
        public Bitmap defImg;

        public int MAX_MAP_SIZE { get { return 19; } }
        public int ICON_SIZE { get { return 32; } }
        public int WIDTH { get { return (MAX_MAP_SIZE + 1) * ICON_SIZE; } }
        public int HEIGHT { get { return (MAX_MAP_SIZE + 1) * ICON_SIZE; } }
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

        public World(PictureBox _HudForm, Bitmap _CharStatBar, Bitmap _CharImgBox, Bitmap _strImg, Bitmap _defImg, Character _player)
        {
            HudForm = _HudForm;
            player = _player;
            player.world = this;

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
        }

        void PopulateItems()
        {
            Item spiderSilk = new Item(ITEM_ID_SPIDER_SILK, "Spider Silk", "Spider Silk", 5, new Bitmap("icons/items/Spider Silk.png"));

            Items.Add(spiderSilk);  
        }
        void PopulateWeapons()
        {
            Weapon rustySword = new Weapon(WEAPON_ID_RUSTY_SWORD, "Rusty Sword", "Rusty Swords", 1, 5, 5, true, false, new Bitmap("icons/items/Rusty Sword.png"));
            Weapon crudeAx = new Weapon(WEAPON_ID_CRUDE_AX, "Crude Ax", "Crude Axes", 1, 7, 10, true, false, new Bitmap("icons/items/Crude Ax.png"));

            Items.Add(rustySword);
            Items.Add(crudeAx);
        }
        void PopulatePotions()
        {
            Potion basicHealthPotion = new Potion(POTION_ID_BASIC_HEALTH, "Basic Health Potion", "Basic Health Potions", 15, 30, new Bitmap("icons/items/Basic Health Potion.png"));
            Potion medHealthPotion = new Potion(POTION_ID_BASIC_HEALTH, "Better Health Potion", "Better Health Potions", 30, 60, new Bitmap("icons/items/Better Health Potion.png"));
            Potion HighHealthPotion = new Potion(POTION_ID_BASIC_HEALTH, "Best Health Potion", "Best Health Potions", 45, 90, new Bitmap("icons/items/Best Health Potion.png"));

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
            NPC bugSquasher = new NPC(NPC_ID_BUGSQUASHER, "Bug Squasher", new System.Drawing.Bitmap("icons/NPC.bmp"), new System.Drawing.Point(0, 0), new Quest(QuestByID(QUEST_ID_BUGSQUASHING)), this);
            NPCs.Add(bugSquasher);
        }
        void PopulateLocations()
        {
            Location homeInside = new Location(LOCATION_ID_HOUSE_INSIDE, "Inside", "You should really clean this place up.");
            homeInside.MonsterLivingHere = null;

            Location field = new Location(LOCATION_ID_FIELD, "Fields", "This place is swarmed with spiders");
            field.MonsterLivingHere = MonsterByID(MONSTER_ID_SPIDER);

            field.NPCsLivingHere = new List<NPC>();
            field.NPCsLivingHere.Add(new NPC(NPCByID(NPC_ID_BUGSQUASHER)));

            field.Transitions = new List<Transition>();

            Location home = new Location(LOCATION_ID_HOUSE, "Your House", "You live here but now you should go adventuring");
            home.MonsterLivingHere = null;
            home.Transitions = new List<Transition>();

            Locations.Add(home);
            Locations.Add(homeInside);
            Locations.Add(field);

            Locations[2].LocationToSouth = LocationByID(LOCATION_ID_HOUSE);
            Locations[2].Transitions.Add(new Transition(new Point(5, 10), "South", field.LocationToSouth));
            Locations[0].LocationToNorth = LocationByID(LOCATION_ID_FIELD);
            Locations[0].Transitions.Add(new Transition(new Point(5, -1), "North", home.LocationToNorth));
        }
        void PopulateMonsters()
        {
            
            Monster spider = new Monster(MONSTER_ID_SPIDER, "Spider", new System.Drawing.Point(6, 5), 10, 10, 0, 0, 5, 5, 10, 5, 50, new System.Drawing.Bitmap("icons/spider.png"), this, this.HudForm);
            spider.LootTable.Add(new LootItem(ItemByID(ITEM_ID_SPIDER_SILK), 100, true));

            

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
        void PopulateSkills()
        {
            Skills = new List<Skill>();

            Debuff Attack = new Debuff(SKILL_ID_ATTACK, "Attack", new Bitmap(32, 32), "Health", 5);
            Debuff Burn = new Debuff(SKILL_ID_BURN, "Burn", new Bitmap(32, 32), "Health", 5);

            Skills.Add(Attack);
            Skills.Add(Burn);
        }
        void PopulateHUDObjects(Bitmap _CharStatBar, Bitmap _CharImgBox, Bitmap _strImg, Bitmap _defImg)
        {            
            HUD = new Hud(this);

            Clickables = new List<HUDObject>();
            InventoryItems = new List<HUDObject>();

            List<Point> temp = new List<Point>();
            temp.Add(new Point(0, 0));
            temp.Add(new Point(temp[0].X + _CharImgBox.Width, temp[0].Y + _CharImgBox.Height));

            CharImgBox = new HUDObject(temp, _CharImgBox);

            temp = new List<Point>();
            temp.Add(new Point(CharImgBox.Boundries[0].X + 10, CharImgBox.Boundries[0].Y + 10));
            CharImg = new HUDObject(temp, player.Image);

            temp = new List<Point>();
            temp.Add(new Point(CharImgBox.Boundries[1].X, 0));
            temp.Add(new Point(temp[0].X + _CharStatBar.Width, temp[0].Y + _CharStatBar.Height));
            CharStatBar = new HUDObject(temp, _CharStatBar);

            temp = new List<Point>();
            temp.Add(new Point(CharStatBar.FindCenterofBounds().X - 45, CharStatBar.FindCenterofBounds().Y + 25));
            MainHealthBar = new HUDObject(temp, new Bitmap("icons/HUDBars/HealthBar/HealthBar10.png"));

            temp = new List<Point>();
            temp.Add(new Point(CharStatBar.FindCenterofBounds().X - 45, CharStatBar.FindCenterofBounds().Y + 40));
            MainExpBar = new HUDObject(temp, new Bitmap("icons/HUDBars/ExpBar/ExpBar (10).png"));

            temp = new List<Point>();
            temp.Add(new Point(MainHealthBar.Boundries[0].X, MainHealthBar.Boundries[0].Y - 80));
            NameLevelString = new HUDObject(temp, null, player.Name + " (" + player.Level + ")");

            temp = new List<Point>();
            temp.Add(new Point(MainHealthBar.Boundries[0].X, NameLevelString.Boundries[0].Y + 25));
            Class = new HUDObject(temp, null, player.Class);

            temp = new List<Point>();
            temp.Add(new Point(MainHealthBar.Boundries[0].X, NameLevelString.Boundries[0].Y + 45));
            Strength = new HUDObject(temp, _strImg, ": " + player.MaximumDamage.ToString());

            temp = new List<Point>();
            temp.Add(new Point(MainHealthBar.Boundries[0].X + 70, NameLevelString.Boundries[0].Y + 45));
            Defense = new HUDObject(temp, _defImg, ": " + player.MaximumDefense.ToString());

            temp = new List<Point>();
            temp.Add(new Point(0, HudForm.Height - 32));
            temp.Add(new Point(32, HudForm.Height));
            InventoryButton = new HUDObject(temp, new Bitmap("icons/HUDBars/bagbutton.png"));
            InventoryButton.Name = "Bag";
            Clickables.Add(InventoryButton);

            temp = new List<Point>();
            temp.Add(new Point(HudForm.Width - 32, HudForm.Height - 32));
            temp.Add(new Point(HudForm.Width, HudForm.Height));
            CloseButton = new HUDObject(temp, new Bitmap("icons/HUDBars/exitbutton.png"));
            CloseButton.Name = "Close";
            Clickables.Add(CloseButton);

            temp = new List<Point>();
            temp.Add(new Point(InventoryButton.Boundries[0].X, InventoryButton.Boundries[0].Y - 400));
            temp.Add(new Point(InventoryButton.Boundries[0].X + 300, InventoryButton.Boundries[0].Y));
            InventoryBox = new HUDObject(temp, new Bitmap("icons/HUDBars/bagbox.png"));

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
