using AnimatedSpriteSpace;
using NodeSpace;
using UnityEngine;

namespace GhostSpace
{
    public class GhostFrightened : GhostBehavior
    {
        public SpriteRenderer Body;
        public SpriteRenderer Eyes;
        public SpriteRenderer Blue;
        public SpriteRenderer White;

        private bool _eaten;

        public override void Enable(float duration)
        {
            base.Enable(duration);

            Body.enabled = false;
            Eyes.enabled = false;
            Blue.enabled = true;
            White.enabled = false;
            
            Invoke(nameof(Flash), duration / 2f);
        }

        public override void Disable()
        {
            base.Disable();
            
            Body.enabled = true;
            Eyes.enabled = true;
            Blue.enabled = false;
            White.enabled = false;
        }

        private void Eaten()
        {
            _eaten = true;

            Ghost.Movement.ResetStart();
            Ghost.InitialBehavior.Enable(Duration);
            
            Body.enabled = false;
            Eyes.enabled = true;
            Blue.enabled = false;
            White.enabled = false;
        }

        private void Flash()
        {
            if (!_eaten)
            {
                Blue.enabled = false;
                White.enabled = true;
                
                White.GetComponent<AnimatedSprite>().Restart();
            }
        }

        private void OnEnable()
        {
            Blue.GetComponent<AnimatedSprite>().Restart();
            Ghost.Movement.SpeedMultiplier = 0.5f;

            _eaten = false;
        }

        private void OnDisable()
        {
            Ghost.Movement.SpeedMultiplier = 1f;
            _eaten = false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Node node = other.GetComponent<Node>();

            if (node && enabled)
            {
                Vector2 direction = Vector2.zero;

                float maxDistance = float.MinValue;

                foreach (var availableDirection in node.AvailableDirections)
                {
                    Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y);
                    float distance = (Ghost.Target.position - newPosition).sqrMagnitude;

                    if (distance > maxDistance)
                    {
                        direction = availableDirection;
                        maxDistance = distance;
                    }
                }
                
                Ghost.Movement.SetDirection(direction);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (enabled && other.gameObject.layer == LayerMask.NameToLayer("PacMan"))
            {
                Eaten();
            }
        }
    }
}
