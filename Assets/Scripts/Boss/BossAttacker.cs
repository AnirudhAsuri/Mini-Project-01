using UnityEngine;

namespace SG
{
    public class BossAttacker : MonoBehaviour
    {
        private Animator anim;
        private PlayerInventory playerInventory;
        private WeaponSlotManager weaponSlotManager;
        private AnimatorManager animatorManager;
        private BossLocomotion bossLocomotion;
        private Transform playerTransform;

        public float attackRange = 5f; // Maximum distance for the SphereCast
        public float attackRadius = 1f; // Radius of the SphereCast
        public string[] attackAnimations = { "OH_Light_Attack_01 Boss", "OH_Light_Attack_02 Boss" }; // Attack choices
        private bool isAttacking;

        private void Awake()
        {
            playerInventory = GetComponent<PlayerInventory>();
            weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
            anim = GetComponentInChildren<Animator>();
            animatorManager = GetComponentInChildren<AnimatorManager>();
            bossLocomotion = GetComponent<BossLocomotion>();
            playerTransform = bossLocomotion.playerTransform; // Get player reference
        }

        private void Update()
        {
            if (anim.GetBool("isInteracting"))
                return; // Prevent multiple attacks

            // Perform the SphereCast
            
            if (Vector3.Distance(transform.position, playerTransform.position) < 3f)
            {
                Attack(playerInventory.rightWeapon);
                Debug.Log("Attacking");
            }
        }

        private void Attack(WeaponItem weapon)
        {
            // Use AnimatorManager to play attack animation
            string[] attackAnimations = { weapon.OH_Light_Attack_1, weapon.OH_Light_Attack_2 };

            // Select a random attack animation from the array
            string attackAnim = attackAnimations[Random.Range(0, attackAnimations.Length)];
            weaponSlotManager.attackingWeapon = weapon;
            animatorManager.PlayTargetAnimation(attackAnim, true);
            Debug.Log(attackAnim);// Set isInteracting to true for root motion
        }
    }
}

