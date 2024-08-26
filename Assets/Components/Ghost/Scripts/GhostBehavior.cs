using UnityEngine;

namespace GhostSpace
{
    [RequireComponent(typeof(Ghost))]
    public class GhostBehavior : MonoBehaviour
    {
        protected Ghost Ghost { get; private set; }
        public float Duration;

        private void Awake()
        {
            Ghost = GetComponent<Ghost>();
        }

        public void Enable()
        {
            Enable(Duration);
        }

        public virtual void Enable(float duration)
        {
            enabled = true;
            
            CancelInvoke();
            Invoke(nameof(Disable), duration);
        }

        public virtual void Disable()
        {
            enabled = false;
            
            CancelInvoke();
        }
    }
}
