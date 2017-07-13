using System.Drawing;

namespace CsharpRPG.Engine
{
    public class PartyMember : Entity
    {
        public PartyMember(PartyMember member) : 
            base(member.ID, member.Name, member.Location, member.Health, member.MaxHealth, member.Mana, member.MaxMana, member.MaximumDamage, member.MaximumDefense, member.Image, member.world)
        {

        }
        public PartyMember(int _id, string _name, Point _location, int _hp, int _maxHp, int _mana, int _maxMana, int _maximumDamage, int _maxDefense, Bitmap _img, World _world) :
            base(_id, _name, _location, _hp, _maxHp, _mana, _maxMana, _maximumDamage, _maxDefense, _img, _world)
        {

        }
    }
}
