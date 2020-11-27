using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CarKrash.Collision.Utils;

namespace CarKrash.Collision.Unity2D
{
    public class Enemy : Entity
    {
        public string enemyName;
        public ScriptableObjects.LootTable lootTable;

        // the radius in which the player needs to be within for the enemy to "wake up"
        [SerializeField] private float awakeRadius = 2f;
        // the radius in which the player needs to be within in order for the enemy to move to attack
        [SerializeField] private float attackRadius = 1f;
        [SerializeField] private LayerMask playerLayer;
        private Inventory inventory;

        private bool isWalking = false;
        private Vector2 playerPos = Vector2.zero;

        //public bool returnHome;
        //private Vector2 startingPoint;
        //private bool isHome { get { return myRigidBody.position == startingPoint; } }

        //public Vector2 StartingPoint => startingPoint;
        //public float WalkRange => throw new System.NotImplementedException();
        //public float Speed => throw new System.NotImplementedException();

        private void Awake()
        {
            inventory = GetComponent<Inventory>();
            //GameEvents.onPlayerPositionChange += GameEvents_onPlayerPositionChange;
            animator.SetBool("sleeping", true);
            //inventory.AddEquipment(GameManager.Items.Db[0] as Weapon);
        }

        private void GameEvents_onPlayerPositionChange(float x, float y)
        {
            playerPos = new Vector2(x, y);
            //Debug.Log($"Player Position: {playerPos}");
            float distance = Vector2.Distance(playerPos, transform.position);
            if (distance <= awakeRadius && distance > attackRadius)
            {
                animator.SetBool("sleeping", false);
                animator.SetBool("moving", false);
                isWalking = false;
            }
            else if (distance <= attackRadius)
            {
                animator.SetBool("moving", true);

                isWalking = true;
            }
            else
            {
                animator.SetBool("sleeping", true);
                animator.SetBool("moving", false);
                isWalking = false;
            }
        }

        // Start is called before the first frame update
        public override void Start()
        {
            base.Start();
        }

        // Update is called once per frame
        public override void Update()
        {
            // ** TODO: **
            // add player discovery/follow code
            if (isWalking)
            {
                Vector2 pos = new Vector2(transform.position.x, transform.position.y);
                transform.position = Vector2.MoveTowards(pos, playerPos, speed * Time.deltaTime);
                //Debug.Log($"{name} Pos: {transform.position}");
                //Debug.Log(temp);
                //moveEntity(temp);
            }

            Debug.Log(inventory.equipment[(int)EquipmentSlotType.MAINHAND]);
            float distance = Vector2.Distance(playerPos, transform.position);
            if (distance <= attackRadius)
            {
                if (inventory.equipment[(int)EquipmentSlotType.MAINHAND] != null)
                {
                    //((Weapon)inventory.equipment[(int)EquipmentSlotType.MAINHAND]).Attack(this, animator, meleePoint, meleePointDistanceFromPlayer, playerLayer);
                }
            }

            base.Update();
        }

        public void Walk(ref Vector2 velocity)
        {
            throw new System.NotImplementedException();
        }
    }
}