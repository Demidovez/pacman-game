using GameManagerSpace;
using UnityEngine;
using Zenject;

namespace PelletSpace
{
    [RequireComponent(typeof(Collider2D))]
    public class Pellet : MonoBehaviour
    {
        public int Points = 10;
        
        protected GameManager GameManager;

        [Inject]
        public void Construct(GameManager gameManager)
        {
            GameManager = gameManager;
        }

        protected virtual void Eat()
        {
            GameManager.PelletEaten(this);
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
