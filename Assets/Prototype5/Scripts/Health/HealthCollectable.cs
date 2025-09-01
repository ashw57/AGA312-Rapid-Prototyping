using UnityEngine;

namespace Prototype5
{
    public class HealthCollectable : MonoBehaviour
    {
        [SerializeField] private float healthValue;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Player"))
            {
                Health playerHealth = collision.GetComponent<Health>();
                if (playerHealth != null)
                {
                    playerHealth.AddHealth(healthValue);
                    Destroy(gameObject);
                }                
            }
        }
    }
}

