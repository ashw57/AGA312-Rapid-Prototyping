using UnityEngine;

namespace Prototype3
{
    public class Weapon : MonoBehaviour
    {
        public Transform gunTip;
        public GameObject prefab;
        public GameObject Crosshair;

        public ParticleSystem muzzleFlash;

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }

        void Shoot()
        {
            if (muzzleFlash != null)
                muzzleFlash.Play();

            Vector3 shootDirection = (Crosshair.transform.position - gunTip.position).normalized;

             Instantiate(prefab, gunTip.position, Quaternion.LookRotation(shootDirection));
        }
    }
}
