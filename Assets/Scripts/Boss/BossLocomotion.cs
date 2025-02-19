using UnityEngine;


namespace SG
{
    public class BossLocomotion : MonoBehaviour
    {
        Animator bossAnimator;

        public Transform playerTransform;
        public Rigidbody bossRigidBody;
        public float moveSpeed = 3f;
        public float rotationSpeed = 0.5f;

        private void Update()
        {
            MoveTowardsTarget(moveSpeed, Vector3.up);
        }
        private void Awake()
        {
            bossAnimator = GetComponentInChildren<Animator>();
            bossRigidBody = GetComponent<Rigidbody>();
        }

        public void MoveTowardsTarget(float moveSpeed, Vector3 normalVector)
        {
            Vector3 targetPosition = playerTransform.position;
            Vector3 moveDirection;

        
                moveDirection = (targetPosition - transform.position).normalized;
                moveDirection.y = 0; // Prevent unnecessary vertical movement

                moveDirection *= moveSpeed;
                // Project movement onto the ground pla
            bossRigidBody.velocity = moveDirection;

            Vector3 localMoveDirection = transform.InverseTransformDirection(moveDirection);

            bossAnimator.SetFloat("Horizontal", localMoveDirection.x, 0.1f, Time.deltaTime);
            bossAnimator.SetFloat("Vertical", localMoveDirection.z, 0.1f, Time.deltaTime);

            HandleRotation();

        }

        public void HandleRotation()
        {
            Vector3 targetPosition = playerTransform.position;
            Vector3 targetDirection = targetPosition - transform.position;
            targetDirection.y = 0;
            targetDirection.Normalize();

            Quaternion tr = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, tr, rotationSpeed / Time.deltaTime);
        }
    }
}

