using System.Diagnostics.Contracts;
using UnityEngine;

namespace Prototype3
{
    public class Projectiles : MonoBehaviour
    {
        public float timer = 3f;
        public float speed = 5f;

        void Start()
        {
            Invoke("Die", timer);
        }

        void Update()
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }



        void Die()
        {
            Destroy(gameObject);
        }
    }
}