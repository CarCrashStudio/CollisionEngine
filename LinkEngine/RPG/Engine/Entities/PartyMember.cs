namespace RPG.Engine
{
    public class PartyMember : Entity
    {
        public PartyMember(PartyMember member) : 
            base(member.ID, member.Name, member.Health, member.MaxHealth, member.Mana, member.MaxMana, member.Strength, member.Defense)
        {

        }
        public PartyMember(int _id, string _name, int _hp, int _maxHp, int _mana, int _maxMana, int _maximumDamage, int _maxDefense) :
            base(_id, _name, _hp, _maxHp, _mana, _maxMana, _maximumDamage, _maxDefense)
        {

        }
    }
}
