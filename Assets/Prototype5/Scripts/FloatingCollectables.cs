using UnityEngine;

namespace Prototype5
{
    public class FloatingCollectable : MonoBehaviour
    {
        private Transform player;
        private float orbitRadius = 0.3f;
        private float orbitSpeed = 50f; // degrees per second
        private float angleOffset;
        private float angle;


        public void SetPlayer(Transform playerTransform)
        {
            player = playerTransform;
        }

        public void SetOrbitIndex(int index, int total)
        {
            angleOffset = 360f * ((float)index / total);
        }

        private void Update()
        {
            if (player == null) return;

            angle += orbitSpeed * Time.deltaTime;
            float totalAngle = angle + angleOffset;

            float rad = totalAngle * Mathf.Deg2Rad;
            Vector3 offset = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0) * orbitRadius;
            transform.position = player.position + offset;
        }
    }
}