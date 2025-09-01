using UnityEngine;

namespace Prototype5
{
    public class Door : MonoBehaviour
    {
        private bool isUnlocked = false;

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

