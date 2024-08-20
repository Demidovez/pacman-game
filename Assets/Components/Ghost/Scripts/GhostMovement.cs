using UnityEngine;

namespace GhostSpace
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class GhostMovement : MonoBehaviour
    {
        public float Speed;
        public float GhostSpeed;
        public Vector2 InitDirection;
        public LayerMask LayerMask;

        public Rigidbody2D RigidBody { get; private set; }
        public Vector2 Direction { get; private set; }
        public Vector2 NextDirection { get; private set; }
        public Vector3 StartingPosition { get; private set; }
        
        private void Awake()
        {
            RigidBody = GetComponent<Rigidbody2D>();
            StartingPosition = transform.position;
        }

        private void Start()
        {
            ResetStart();
        }

        private void FixedUpdate()
        {
            Vector2 position = RigidBody.position;
            Vector2 translation = Direction * (Speed * GhostSpeed * Time.fixedDeltaTime);
            
            RigidBody.MovePosition(position + translation);
        }

        public void ResetStart()
        {
            GhostSpeed = 1;
            Direction = InitDirection;
            NextDirection = Vector2.zero;
            transform.position = StartingPosition;
            RigidBody.isKinematic = false;

            enabled = true;
        }

        public void SetDirection(Vector2 direction, bool forced = false)
        {
            if (forced || !Occupied(direction))
            {
                Direction = direction;
                NextDirection = Vector2.zero;
            }
            else
            {
                NextDirection = direction;
            }
        }

        private bool Occupied(Vector2 direction)
        {
            RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.75f, 0.0f, direction, 1.5f);

            return hit.collider;
        }
    }
}
