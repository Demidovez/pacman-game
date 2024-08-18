using System;
using System.Collections.Generic;
using UnityEngine;

namespace NodeSpace
{
    public class Node : MonoBehaviour
    {
        public LayerMask ObstacleLayer;
        public readonly List<Vector2> AvailableDirections = new();

        private void Start()
        {
            AvailableDirections.Clear();
            
            CheckAvailableDirection(Vector2.up);
            CheckAvailableDirection(Vector2.down);
            CheckAvailableDirection(Vector2.left);
            CheckAvailableDirection(Vector2.right);
        }

        private void CheckAvailableDirection(Vector2 direction)
        {
            RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.5f, 0f, direction, 1f, ObstacleLayer);

            if (hit.collider == null)
            {
                AvailableDirections.Add(direction);
            }
        }
    }  
}

