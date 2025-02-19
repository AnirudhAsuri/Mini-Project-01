using UnityEngine;

namespace SG
{
    public class BossAttacker : MonoBehaviour
    {
        private Animator anim;
        private AnimatorManager animatorManager;
        private BossLocomotion bossLocomotion;
        private Transform playerTransform;

        public float attackRange = 5f; // Maximum distance for the SphereCast
        public float attackRadius = 1f; // Radius of the SphereCast
        public string[] attackAnimations = { "OH_Light_Attack_01 Boss", "OH_Light_Attack_02 Boss" }; // Attack choices
        private bool isAttacking;

        private void Awake()
        {
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
                Attack();
                Debug.Log("Attacking");
            }
        }

        private void Attack()
        {
            // Use AnimatorManager to play attack animation
            string[] attackAnimations = { "OH_Light_Attack_01 Boss", "OH_Light_Attack_02 Boss" };

            // Select a random attack animation from the array
            string attackAnim = attackAnimations[Random.Range(0, attackAnimations.Length)];

            animatorManager.PlayTargetAnimation(attackAnim, true);
            Debug.Log(attackAnim);// Set isInteracting to true for root motion
        }
    }
}

