using GameManagerSpace;
using Zenject;

namespace PelletSpace
{
    public class PowerPellet : Pellet
    {
        public float Duration = 8f;

        protected override void Eat()
        {
            GameManager.PowerPelletEaten(this);
        }
    }
}

