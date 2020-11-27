namespace CarKrash.Collision.Utils
{
    public interface IEntity
    {
        /// <summary>
        /// The FSM for the entity that controls which actions the entity
        /// can or cannot take, based on it's current state.
        /// </summary>
        EntityState CurrentState { get; }
        Attributes Attributes { get; }

        int Defense { get; }
        int ProficiencyBonus { get; }

        WeaponProficiencies[] WeaponProficiencies { get; }
        ArmorProficiencies[] ArmorProficiencies { get; }

        void Start();
        void Update();
        void LateUpdate();
    }
}
