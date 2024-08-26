using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GhostSpace
{
    public class GhostHome : GhostBehavior
    {
        public Transform Inside;
        public Transform Outside;

        private void OnEnable()
        {
            StopAllCoroutines();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (enabled && other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
            {
                Ghost.Movement.SetDirection(-Ghost.Movement.Direction);
            }
        }

        private void OnDisable()
        {
            if (gameObject.activeInHierarchy)
            {
                StartCoroutine(ExitTransition());
            }
        }

        private IEnumerator ExitTransition()
        {
            Ghost.Movement.SetDirection(Vector2.up, true);
            Ghost.Movement.RigidBody.isKinematic = true;
            Ghost.Movement.enabled = false;
            
            Vector3 position = transform.position;
            
            float duration = 1f;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                Ghost.SetPosition(Vector3.Lerp(position, Inside.position, elapsed / duration));
                elapsed += Time.deltaTime;

                yield return null;
            }

            elapsed = 0;
            
            while (elapsed < duration)
            {
                Ghost.SetPosition(Vector3.Lerp(Inside.position, Outside.position, elapsed / duration));
                elapsed += Time.deltaTime;

                yield return null;
            }
            
            Ghost.Movement.SetDirection(new Vector2(Random.value < 0.5f ? -1f: 1f, 0f ), true);
            Ghost.Movement.RigidBody.isKinematic = false;
            Ghost.Movement.enabled = true;
        }
    }
}
