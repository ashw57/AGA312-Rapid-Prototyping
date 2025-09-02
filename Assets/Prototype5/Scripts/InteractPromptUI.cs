using UnityEngine;
using UnityEngine.UI;

namespace Prototype5
{
    public class InteractPromptUI : MonoBehaviour
    {
        public static InteractPromptUI Instance;

        [SerializeField] private GameObject promptObject;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);

            Hide(); // Hide on start
        }

        public void Show()
        {
            if (promptObject != null)
                promptObject.SetActive(true);
        }

        public void Hide()
        {
            if (promptObject != null)
                promptObject.SetActive(false);
        }
    }
}