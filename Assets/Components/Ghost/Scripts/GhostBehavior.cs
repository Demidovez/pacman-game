using UnityEngine;
using Zenject;

namespace GhostSpace
{
    [RequireComponent(typeof(Ghost))]
    public class GhostBehavior : MonoBehaviour
    {
        protected Ghost Ghost { get; private set; }
        public float Duration;

        [Inject]
        public void Construct(Ghost ghost)
        {
            Ghost = ghost;
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
