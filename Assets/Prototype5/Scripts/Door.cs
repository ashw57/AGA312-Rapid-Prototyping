using UnityEngine;

namespace Prototype5
{
    public class Door : MonoBehaviour
    {
        private bool isUnlocked = false;
        private bool playerInRange = false;

        public void Unlock()
        {
            if (!isUnlocked)
            {
                               
                    isUnlocked = true;
                    Debug.Log("Door Unlocked!");                
            }
        }

        private void Update()
        {
            if (isUnlocked && playerInRange && Input.GetKeyDown(KeyCode.E))
            {
                EndLevel();
            }
        }

        private void EndLevel()
        {
            Time.timeScale = 0;

            InteractPromptUI.Instance.Hide();

            LevelEndUI.Instance.Show();
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.CompareTag("Player"))
            {
                playerInRange = true;

                if (isUnlocked) 
                {
                    InteractPromptUI.Instance.Show();
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            if (collider.CompareTag("Player"))
            {
                playerInRange = false;
                InteractPromptUI.Instance.Hide();
            }
        }
    }
}

