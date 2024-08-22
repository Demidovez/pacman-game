using GameManagerSpace;
using UnityEngine;

namespace PelletSpace
{
    [RequireComponent(typeof(Collider2D))]
    public class Pellet : MonoBehaviour
    {
        public int Points = 10;

        protected virtual void Eat()
        {
            GameManager.Instance.PelletEaten(this);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("PacMan"))
            {
                Eat();
            }
        }
    }
}
