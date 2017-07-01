using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace CsharpRPG.Engine
{
    public class Monster : Entity
    {        
        int rewardExp;
        int rewardGold;

        public int RewardExperiencePoints { get { return rewardExp; } set { rewardExp = value; } }
        public int RewardGold { get { return rewardGold; } set { rewardGold = value; } }

        public List<LootItem> LootTable { get; set; }

        public int SpawnChance { get; set; }

        public Monster(int _id, string _name, Point _location, int _hp, int _maxHp, int _mana, int _maxMana, int _maximumDamage, int _maxDefense, int _rewardExp, int _rewardGold, int _spawnChance, Bitmap _img, World _world, PictureBox _HudForm) :
            base(_id, _name, _location, _hp, _maxHp, _mana, _maxMana, _maximumDamage, _maxDefense, _img, _HudForm, _world)
        {
            rewardExp = _rewardExp;
            rewardGold = _rewardGold;

            LootTable = new List<LootItem>();
            world = _world;

            SpawnChance = _spawnChance;
        }
        public Monster(Monster monster) :
            base(monster.ID, monster.Name, monster.Location, monster.Health, monster.MaxHealth, monster.Mana, monster.MaxMana, monster.MaximumDamage, monster.MaximumDefense, monster.Image, monster.world.HudForm, monster.world)
        {
            rewardExp = monster.rewardExp;
            rewardGold = monster.RewardGold;

            LootTable = new List<LootItem>();
            world = monster.world;

            SpawnChance = monster.SpawnChance;
        }
    }
}
