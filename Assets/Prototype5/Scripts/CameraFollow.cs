using UnityEngine;

namespace Prototype5
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private float aheadDistance;
        [SerializeField] private float cameraSpeed;
        [SerializeField] private float verticalFollowSpeed = 2f;
        [SerializeField] private float verticalOffset = 2f;
        private float lookAhead;


        private void Update()
        {            
            lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * transform.localScale.x), Time.deltaTime * cameraSpeed);

            float targetY = Mathf.Lerp(transform.position.y, player.position.y + verticalOffset, Time.deltaTime * verticalFollowSpeed);

            transform.position = new Vector3(player.position.x * lookAhead, targetY, transform.position.z);
        }


    }
}

