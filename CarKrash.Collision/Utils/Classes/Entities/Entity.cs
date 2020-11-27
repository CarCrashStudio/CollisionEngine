namespace CarKrash.Collision.Utils
{
    public enum EntityState { WALK, ATTACK, STAGGER, IDLE, INTERACTING }
    public class Entity : IEntity
    {
        const int DEFENSE_CONSTANT = 5;
        protected byte level = 1;

        private float speed = 4f;

        protected EntityState currentState;
        protected Attributes attributes;

        protected int defense = 0;
        protected int proficiencyBonus = 2;

        private ArmorProficiencies[] gainedArmorProficiencies;
        private WeaponProficiencies[] gainedWeaponProficiencies;

        public virtual EntityState CurrentState => currentState;
        public virtual Attributes Attributes => attributes;
        public virtual int Defense => defense;
        public virtual int ProficiencyBonus => proficiencyBonus;

        public WeaponProficiencies[] WeaponProficiencies => gainedWeaponProficiencies;
        public ArmorProficiencies[] ArmorProficiencies => gainedArmorProficiencies;

        public virtual void Start()
        {
            attributes.Start();
        }
        public virtual void Update()
        {

        }
        public void LateUpdate()
        {
            defense = DEFENSE_CONSTANT + attributes.totalModifiers.Dexterity;
        }

        //protected void moveEntity(Vector2 velocity)
        //{
        //    if (velocity != Vector2.zero)
        //    {
        //        meleePoint.transform.position = new Vector3(transform.position.x + velocity.x * meleePointDistanceFromPlayer, transform.position.y + velocity.y * meleePointDistanceFromPlayer, 0);
        //    }
        //    if ((currentState == EntityState.WALK || currentState == EntityState.IDLE) && velocity != Vector2.zero)
        //    {
        //        myRigidBody.position += velocity * speed * Time.deltaTime;

        //        animator.SetFloat("Horizontal", velocity.x);
        //        animator.SetFloat("Vertical", velocity.y);
        //        animator.SetBool("moving", true);
        //    }
        //    else
        //    {
        //        animator.SetBool("moving", false);
        //    }
        //}
    }
}