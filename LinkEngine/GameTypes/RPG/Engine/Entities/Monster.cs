using LinkEngine.Entities;
using System.Collections.Generic;

namespace LinkEngine.RPG
{
    public class Monster : Enemy
    {        
        public int SpawnChance { get; set; }

        public Monster(int _id, string _name, int _hp, int _maxHp, int _rewardExp, int _rewardGold, int _spawnChance) :
            base(_id, _name, _hp, _maxHp)
        {
            RewardExperiencePoints = _rewardExp;
            RewardGold = _rewardGold;

            LootTable = new List<LootItem>();

            SpawnChance = _spawnChance;
        }
        public Monster(Monster monster) :
            base(monster.ID, monster.Name, monster.Health, monster.MaxHealth)
        {
            RewardExperiencePoints = monster.RewardExperiencePoints;
            RewardGold = monster.RewardGold;

            LootTable = new List<LootItem>();

            SpawnChance = monster.SpawnChance;
        }
    }
}
