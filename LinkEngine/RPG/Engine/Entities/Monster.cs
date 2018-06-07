﻿using System.Collections.Generic;

namespace LinkEngine
{
    public class Monster : Entity
    {        
        int rewardExp;
        int rewardGold;

        public int RewardExperiencePoints { get { return rewardExp; } set { rewardExp = value; } }
        public int RewardGold { get { return rewardGold; } set { rewardGold = value; } }

        public List<LootItem> LootTable { get; set; }

        public int SpawnChance { get; set; }

        public Monster(int _id, string _name, int _hp, int _maxHp, int _mana, int _maxMana, int _rewardExp, int _rewardGold, int _spawnChance) :
            base(_id, _name, _hp, _maxHp, _mana, _maxMana)
        {
            rewardExp = _rewardExp;
            rewardGold = _rewardGold;

            LootTable = new List<LootItem>();

            SpawnChance = _spawnChance;
        }
        public Monster(Monster monster) :
            base(monster.ID, monster.Name, monster.Health, monster.MaxHealth, monster.Mana, monster.MaxMana)
        {
            rewardExp = monster.rewardExp;
            rewardGold = monster.RewardGold;

            LootTable = new List<LootItem>();

            SpawnChance = monster.SpawnChance;
        }
    }
}
