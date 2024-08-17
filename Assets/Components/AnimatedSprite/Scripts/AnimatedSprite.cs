using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnimatedSpriteSpace
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class AnimatedSprite : MonoBehaviour
    {
        public SpriteRenderer SpriteRenderer { get; private set; }
        public int AnimationFrame { get; private set; }
        
        public Sprite[] Sprites;
        public float AnimationTime;
        public bool Loop = true;

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
            AnimationFrame = -1;
            Advance();
        }
    }
}
