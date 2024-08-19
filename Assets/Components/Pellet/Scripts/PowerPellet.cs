using GameManagerSpace;

namespace PelletSpace
{
    public class PowerPellet : Pellet
    {
        public float Duration = 8f;

        protected override void Eat()
        {
            GameManager.Instance.PowerPelletEaten(this);
        }

    }
}

