using UnityEngine;

namespace Prototype3
{
    public class Weapon : MonoBehaviour
    {
        public Transform gunTip;
        public GameObject prefab;
        public GameObject Crosshair;

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }

            if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hitinfo, 20f))
            {
                print("Hit Something");
            }
            else
            {
                print("Hit Nothing");
            }
        }

        void Shoot()
        {
            Vector3 shootDirection = (Crosshair.transform.position - gunTip.position).normalized;

             Instantiate(prefab, gunTip.position, Quaternion.LookRotation(shootDirection));
        }
    }
}
