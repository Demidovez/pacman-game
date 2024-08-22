using NodeSpace;
using UnityEngine;

namespace GhostSpace
{
    public class GhostChase : GhostBehavior
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            Node node = other.GetComponent<Node>();
            
            if (node && enabled && !Ghost.Frightened.enabled)
            {
                Vector2 direction = Vector2.zero;
                float minDistance = float.MaxValue;

                foreach (var availableDirection in node.AvailableDirections)
                {
                    Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y);
                    float distance = (Ghost.Target.position - newPosition).sqrMagnitude;
                    
                    if (distance < minDistance)
                    {
                        direction = availableDirection;
                        minDistance = distance;
                    }
                }
                
                Ghost.Movement.SetDirection(direction);
            }
        }

        private void OnDisable()
        {
            Ghost.Scatter.Enable();
        }
    }
}

