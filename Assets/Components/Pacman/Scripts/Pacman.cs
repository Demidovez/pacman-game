using System;
using AnimatedSpriteSpace;
using MovementSpace;
using UnityEngine;

namespace PacmanSpace
{
    public class Pacman : MonoBehaviour
    {
        [SerializeField] private AnimatedSprite _deathSequence;

        private SpriteRenderer _spriteRenderer;
        private CircleCollider2D _circleCollider;
        private Movement _movement;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _circleCollider = GetComponent<CircleCollider2D>();
            _movement = GetComponent<Movement>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                _movement.SetDirection(Vector2.up);
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                _movement.SetDirection(Vector2.down);
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _movement.SetDirection(Vector2.left);
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                _movement.SetDirection(Vector2.right);
            }

            float angle = Mathf.Atan2(_movement.Direction.y, _movement.Direction.x);
            transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
        }

        public void ResetStart()
        {
            enabled = true;
            _spriteRenderer.enabled = true;
            _circleCollider.enabled = true;
            _deathSequence.enabled = false;
            _movement.ResetStart();
            gameObject.SetActive(true);
        }
        
        public void DeathSequence()
        {
            enabled = false;
            _spriteRenderer.enabled = false;
            _circleCollider.enabled = false;
            _movement.enabled = false;
            _deathSequence.enabled = true;
            _deathSequence.Restart();
        }
    }
}
