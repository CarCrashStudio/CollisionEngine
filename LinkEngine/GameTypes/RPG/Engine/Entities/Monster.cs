using LinkEngine.Entities;
using System.Collections.Generic;

namespace LinkEngine.RPG
{
    public class Monster : Enemy
    {        
        int rewardExp;
        int rewardGold;

        public short Strength { get; set; }
        public short Defense { get; set; }

        public int RewardExperiencePoints { get { return rewardExp; } set { rewardExp = value; } }
        public int RewardGold { get { return rewardGold; } set { rewardGold = value; } }

        public List<LootItem> LootTable { get; set; }

        public int SpawnChance { get; set; }

        public Monster(int _id, string _name, int _hp, int _maxHp, int _rewardExp, int _rewardGold, int _spawnChance) :
            base(_id, _name, _hp, _maxHp)
        {
            rewardExp = _rewardExp;
            rewardGold = _rewardGold;

            LootTable = new List<LootItem>();

            SpawnChance = _spawnChance;
        }
        public Monster(Monster monster) :
            base(monster.ID, monster.Name, monster.Health, monster.MaxHealth)
        {
            rewardExp = monster.rewardExp;
            rewardGold = monster.RewardGold;

            LootTable = new List<LootItem>();

            SpawnChance = monster.SpawnChance;
        }
    }
}
