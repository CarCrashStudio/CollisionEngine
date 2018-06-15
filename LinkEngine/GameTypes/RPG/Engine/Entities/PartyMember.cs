using LinkEngine.Entities;

namespace LinkEngine.RPG
{
    public class PartyMember : Entity
    {
        public PartyMember(PartyMember member) : 
            base(member.ID, member.Name, member.Health, member.MaxHealth)
        {

        }
        public PartyMember(int _id, string _name, int _hp, int _maxHp) :
            base(_id, _name, _hp, _maxHp)
        {

        }
    }
}
