using UnityEngine;

namespace AnimatedSpriteSpace
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class AnimatedSprite : MonoBehaviour
    {
        public Sprite[] Sprites;
        public float AnimationTime;
        public bool Loop = true;
        
        private SpriteRenderer SpriteRenderer { get; set; }
        private int AnimationFrame { get; set; }

        private void Awake()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            InvokeRepeating(nameof(Advance), AnimationTime, AnimationTime);
        }

        private void Advance()
        {
            if (!SpriteRenderer.enabled)
            {
                return;
            }

            AnimationFrame++;
                
            if (AnimationFrame >= Sprites.Length && Loop)
            {
                AnimationFrame = 0;
            }

            if (AnimationFrame >= 0 && AnimationFrame < Sprites.Length)
            {
                SpriteRenderer.sprite = Sprites[AnimationFrame];
            }
        }

        public void Restart()
        {
            SpriteRenderer.enabled = true;
            
            AnimationFrame = -1;
            Advance();
        }
    }
}
