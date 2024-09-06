using System;
using MovementSpace;
using UnityEngine;
using Zenject;

namespace GhostSpace
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class GhostEyes : MonoBehaviour
    {
        public Sprite Up;
        public Sprite Down;
        public Sprite Left;
        public Sprite Right;

        private SpriteRenderer _spriteRenderer;
        private Movement _movement;

        [Inject]
        public void Construct(Movement movement)
        {
            _movement = movement;
        }

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (_movement.Direction == Vector2.up)
            {
                _spriteRenderer.sprite = Up;
            } else if (_movement.Direction == Vector2.down)
            {
                _spriteRenderer.sprite = Down;
            } else if (_movement.Direction == Vector2.left)
            {
                _spriteRenderer.sprite = Left;
            } else if (_movement.Direction == Vector2.right)
            {
                _spriteRenderer.sprite = Right;
            }
        }
    }
}