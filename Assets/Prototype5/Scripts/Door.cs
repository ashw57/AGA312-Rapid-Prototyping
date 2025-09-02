using UnityEngine;

namespace Prototype5
{
    public class Door : MonoBehaviour
    {
        private bool isUnlocked = false;
        private int collectablesInZone = 0;

        [SerializeField] private int collectablesNeedeed = 5;

        private void OnTriggerEnter2D(Collider2D collider)
        {

            if(collider.GetComponent<FloatingCollectable>() != null)
            {
                collectablesInZone++;
                CheckUnlock();
            }

            if (isUnlocked) return;
        }

        private void CheckUnlock()
        {
            if(collectablesInZone >= collectablesNeedeed)
            {
                Unlock();
            }
        }

        public void Unlock()
        {
            if (!isUnlocked)
            {
                isUnlocked = true;
                {
                    isUnlocked = true;
                    Debug.Log("Door Unlocked!");
                        gameObject.SetActive(false);
                }
            }
        }
    }
}

