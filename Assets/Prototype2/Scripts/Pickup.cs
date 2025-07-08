using System.Diagnostics;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public float lifetime = 5f;

     void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);     
        }
    }

}
