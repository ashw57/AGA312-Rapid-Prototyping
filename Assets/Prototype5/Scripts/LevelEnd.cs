using UnityEngine;

namespace Prototype5
{
    public class LevelEndUI : MonoBehaviour
    {
        public static LevelEndUI Instance;

        [SerializeField] private GameObject endLevelPanel;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);

            Hide();
        }

        public void Show()
        {
            if (endLevelPanel != null)
                endLevelPanel.SetActive(true);
        }

        public void Hide()
        {
            if (endLevelPanel != null)
                endLevelPanel.SetActive(false);
        }
    }
}