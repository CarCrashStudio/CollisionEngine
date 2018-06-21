using LinkEngine.Entities;
using System.Collections.Generic;

namespace LinkEngine.RPG
{
    public class Monster : Enemy
    {
        /// <summary>
        /// The chance of an enemy spawning. 1 (uncommon) - 100 (common)
        /// </summary>
        public int SpawnChance { get; set; }

        public List<Entity> Party { get; set; }
        public List<Entity> PartyDead { get; set; }

        /// <summary>
        /// Creates a new Monster Entity from the parameters given
        /// </summary>
        /// <param name="_id">The ID of the Monster</param>
        /// <param name="_name">The Name of the Monster</param>
        /// <param name="_hp">The Health of the Monster</param>
        /// <param name="_maxHp">The max Health of the Monster</param>
        /// <param name="str">The Strength of the Monster</param>
        /// <param name="def">The Defense of the Monster</param>
        /// <param name="_rewardExp">The amount of experience gained from killing this monster</param>
        /// <param name="_rewardGold">The amount of gold gained from killing this monster</param>
        /// <param name="_spawnChance">the chance of this monster spawning. 1 (uncommon) - 100 (common)</param>
        public Monster(int _id, string _name, int _hp, int _maxHp, short str, short def, int _rewardExp, int _rewardGold, int _spawnChance) :
            base(_id, _name, _hp, _maxHp, str, def)
        {
            RewardExperiencePoints = _rewardExp;
            RewardGold = _rewardGold;

            LootTable = new List<LootItem>();

            SpawnChance = _spawnChance;
        }
        /// <summary>
        /// Creates a copy of an already created monster
        /// </summary>
        /// <param name="monster">The monster to copy</param>
        public Monster(Monster monster) :
            base(monster.ID, monster.Name, monster.Health, monster.MaxHealth, monster.Strength, monster.Defense)
        {
            RewardExperiencePoints = monster.RewardExperiencePoints;
            RewardGold = monster.RewardGold;

            LootTable = new List<LootItem>();

            SpawnChance = monster.SpawnChance;
        }

        /// <summary>
        /// Removes a party member from the active list and places it in the dead list to be revived
        /// </summary>
        /// <param name="partymember">The party member to kill</param>
        public void KillPartyMember(Entity partymember)
        {
            Party.Remove(partymember);
            PartyDead.Add(partymember);
        }
    }
}
