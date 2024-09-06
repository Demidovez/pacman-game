using UnityEngine;

namespace MovementSpace
{
    public class Movement : MonoBehaviour
    {
        public float Speed;
        public float SpeedMultiplier = 1f;
        public Vector2 InitDirection;
        public LayerMask ObstacleLayer;

        public Rigidbody2D RigidBody { get; private set; }
        public Vector2 Direction { get; private set; }
        private Vector2 _nextDirection;
        private Vector3 _startingPosition;
        
        private void Awake()
        {
            RigidBody = GetComponent<Rigidbody2D>();
            _startingPosition = transform.position;
        }

        private void Start()
        {
            ResetStart();
        }
        
        private void Update()
        {
            if (_nextDirection != Vector2.zero) {
                SetDirection(_nextDirection);
            }
        }

        private void FixedUpdate()
        {
            Vector2 position = RigidBody.position;
            Vector2 translation = Direction * (Speed * SpeedMultiplier * Time.fixedDeltaTime);
            
            RigidBody.MovePosition(position + translation);
        }

        public void ResetStart()
        {
            SpeedMultiplier = 1f;
            Direction = InitDirection;
            _nextDirection = Vector2.zero;
            transform.position = _startingPosition;
            RigidBody.isKinematic = false;

            enabled = true;
        }

        public void SetDirection(Vector2 direction, bool forced = false)
        {
            if (forced || !Occupied(direction))
            {
                Direction = direction;
                _nextDirection = Vector2.zero;
            }
            else
            {
                _nextDirection = direction;
            }
        }
        
        private bool Occupied(Vector2 direction)
        {
            RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.75f, 0f, direction, 1.5f, ObstacleLayer);
            
            return hit.collider;
        }
    }
}
