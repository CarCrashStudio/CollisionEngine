namespace LinkEngine.RPG
{
    public class PartyMember : RPGEntity
    {
        public PartyMember(PartyMember member) : 
            base(member.ID, member.Name, member.Health, member.MaxHealth, member.Mana, member.MaxMana)
        {

        }
        public PartyMember(int _id, string _name, int _hp, int _maxHp, int _mana, int _maxMana) :
            base(_id, _name, _hp, _maxHp, _mana, _maxMana)
        {

        }
    }
}
