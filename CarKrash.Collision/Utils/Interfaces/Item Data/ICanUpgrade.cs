using System.Collections.Generic;

namespace CarKrash.Collision.Utils
{
    public interface ICanUpgrade
    {
        List<Upgrade> UpgradesForItem { get; }
        void ApplyUpgrade(Upgrade upgrade);
    }
}