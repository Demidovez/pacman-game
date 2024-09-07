using GameManagerSpace;
using MovementSpace;
using PacmanSpace;
using UnityEngine;
using Zenject;

namespace GhostSpace
{
    public class Ghost : MonoBehaviour
    {
        public Movement Movement { get; private set; }
        public GhostHome Home { get; private set; }
        public GhostFrightened Frightened { get; private set; }
        public GhostChase Chase { get; private set; }
        public GhostScatter Scatter { get; private set; }
        public Transform Target { get; private set; }
        public GhostBehavior InitialBehavior;
        public int ScorePoint = 200;

        private bool _isEnabled = true;

        private GameManager _gameManager;
        
        [Inject]
        public void Construct(
            GameManager gameManager,
            Movement movement,
            GhostHome home,
            GhostFrightened frightened,
            GhostScatter scatter,
            GhostChase chase,
            Pacman pacman
        )
        {
            _gameManager = gameManager;
            Movement = movement;
            Home = home;
            Frightened = frightened;
            Chase = chase;
            Scatter = scatter;
            Target = pacman.transform;
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
            Scatter.Enable();

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
                    _gameManager.GhostEaten(this);
                }
                else
                {
                    _gameManager.PacmanEaten();
                    ResetStart();
                }
            }
        }
    }
}
