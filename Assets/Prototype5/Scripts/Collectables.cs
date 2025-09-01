using UnityEngine;

namespace Prototype5
{
    public class Collectables : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                CollectablesManager.Instance.Collect();
                Destroy(gameObject);
            }
        }
    }
}

