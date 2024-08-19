using System;
using UnityEngine;

namespace PassageSpace
{
    public class Passage : MonoBehaviour
    {
        public Transform Connection;

        private void OnTriggerEnter2D(Collider2D other)
        {
            Vector3 position = Connection.position;
            position.z = other.transform.position.z;

            other.transform.position = position;
        }
    }
}
