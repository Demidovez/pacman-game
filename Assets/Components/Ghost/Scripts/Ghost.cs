using System;
using GameManagerSpace;
using UnityEngine;

namespace GhostSpace
{
    public class Ghost : MonoBehaviour
    {
        public GhostMovement Movement { get; private set; }
        public GhostHome Home { get; private set; }
        public GhostFrightened Frightened { get; private set; }
        public GhostChase Chase { get; private set; }
        public GhostScatter Scatter { get; private set; }
        
        public GhostBehavior InitialBehavior;
        public Transform Target;
        public int ScorePoint = 200;

        private bool _isEnabled = true;

        private void Awake()
        {
            Movement = GetComponent<GhostMovement>();
            Home = GetComponent<GhostHome>();
            Frightened = GetComponent<GhostFrightened>();
            Chase = GetComponent<GhostChase>();
            Scatter = GetComponent<GhostScatter>();
        }

        private void Start()
        {
            ResetStart();
        }

        private void OnEnable()
        {
            if (!_isEnabled)
            {
                ResetStart();
            }
        }

        private void OnDisable()
        {
            _isEnabled = false;
        }

        private void ResetStart()
        {
            Movement.ResetStart();
            
            Frightened.Disable();
            Chase.Disable();
            Scatter.Disable();

            if (Home != InitialBehavior)
            {
                Home.Disable();
            }

            if (InitialBehavior != null)
            {
                InitialBehavior.Enable();
            }
        }

        public void SetPosition(Vector3 position)
        {
            position.z = transform.position.z;
            transform.position = position;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("PacMan"))
            {
                if (Frightened.enabled)
                {
                    GameManager.Instance.GhostEaten(this);
                }
                else
                {
                    GameManager.Instance.PacmanEaten();
                    ResetStart();
                }
            }
        }
    }
}
