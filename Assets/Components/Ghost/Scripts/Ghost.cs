using System;
using UnityEngine;

namespace GhostSpace
{
    public class Ghost : MonoBehaviour
    {
        public GhostMovement Movement { get; private set; }
        public GhostHome Home { get; private set; }
        public GhostFrightened Frightened { get; private set; }
        
        public GhostBehavior InitialBehavior;
        public Transform Target;
        public int ScorePoint = 200;

        private void Awake()
        {
            Movement = GetComponent<GhostMovement>();
            Home = GetComponent<GhostHome>();
            Frightened = GetComponent<GhostFrightened>();
        }

        private void Start()
        {
            ResetStart();
        }

        private void ResetStart()
        {
            gameObject.SetActive(true);
            Movement.ResetStart();

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
        }
    }
}
