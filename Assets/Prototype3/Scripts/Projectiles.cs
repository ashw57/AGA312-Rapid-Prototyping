using System.Diagnostics.Contracts;
using UnityEngine;

namespace Prototype3
{
    public class Projectiles : GameBehaviour
    {
        public float timer = 3f;
        public float speed = 5f;
        

        void Start()
        {
            Invoke("DestroyProjectile", 3);
        }

        void Update()
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }

        public void DestroyProjectile()
        {
            Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Target"))
            {
                if (collision.gameObject.GetComponent<Target>() != null)
                    collision.gameObject.GetComponent<Target>();
            }
            DestroyProjectile();
        }

    }
}