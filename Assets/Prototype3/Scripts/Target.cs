using Prototype3;
using UnityEngine;

namespace Prototype3
{
    public enum TargetType
    {
        Slow = 1, Average = 2, Fast = 3
    }
    public class Target : GameBehaviour
    {
        [SerializeField]
        float moveSpeed = 5f;

        [SerializeField]
        TargetType targetType = TargetType.Average;

        private Transform[] wayPoints;  

        private int currentWayPoint = 0;
        private Rigidbody rigidB;

        private void Start()
        {
            rigidB = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (wayPoints == null || wayPoints.Length == 0)
                return; 

            Movement();
        }

        public void SetWayPoints(Transform[] wayPoints)
        {
            this.wayPoints = wayPoints;
            currentWayPoint = 0;
        }

        void Movement()
        {
            Vector3 targetPos = wayPoints[currentWayPoint].position;
            Vector3 dir = (targetPos - transform.position).normalized;
            rigidB.MovePosition(transform.position + dir * moveSpeed * Time.deltaTime);

            // Check if close enough to current waypoint to move to next
            if (Vector3.Distance(transform.position, targetPos) < 0.25f)
            {
                currentWayPoint++;
                if (currentWayPoint >= wayPoints.Length)
                    currentWayPoint = 0; // Loop path
            }
        }

        public void Die()
        {
            int scoreValue = (int)targetType;
            GameManager.instance.AddScore(1);
            Destroy(gameObject);
        }
    }
}
