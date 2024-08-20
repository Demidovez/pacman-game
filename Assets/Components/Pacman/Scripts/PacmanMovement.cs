using System;
using UnityEngine;

namespace PacmanSpace
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PacmanMovement : MonoBehaviour
    {
        public float Speed;
        public float PacmanSpeed;
        public Vector2 InitDirection;

        private Rigidbody2D RigidBody { get; set; }
        public Vector2 Direction { get; private set; }
        private Vector3 StartingPosition { get; set; }
        
        private void Awake()
        {
            RigidBody = GetComponent<Rigidbody2D>();
            StartingPosition = transform.position;
            InitDirection = Vector2.zero;
        }

        private void Start()
        {
            ResetStart();
        }

        private void FixedUpdate()
        {
            Vector2 position = RigidBody.position;
            Vector2 translation = Direction * (Speed * PacmanSpeed * Time.fixedDeltaTime);
            
            RigidBody.MovePosition(position + translation);
        }

        public void ResetStart()
        {
            PacmanSpeed = 1;
            Direction = InitDirection;
            transform.position = StartingPosition;
            RigidBody.isKinematic = false;

            enabled = true;
        }

        public void SetDirection(Vector2 direction, bool forced = false)
        {
            Direction = direction;
        }
    }
}
