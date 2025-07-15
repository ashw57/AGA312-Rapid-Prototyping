using UnityEngine;

namespace Prototype3
{
    public class Weapon : MonoBehaviour
    {
        public Transform gunTip;
        public GameObject prefab;

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }

        void Shoot()
        {
            Instantiate(prefab, gunTip.position, Quaternion.identity);
        }
    }
}
