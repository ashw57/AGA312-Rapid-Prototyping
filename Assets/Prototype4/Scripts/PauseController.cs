using UnityEngine;
using UnityEngine.SceneManagement;

namespace Prototype4
{
    public class PauseController : MonoBehaviour
    {
        public GameObject pausePanel;
        private bool paused;

        void Start()
        {
            paused = false;
            if (pausePanel != null)
                pausePanel.SetActive(paused);

            Time.timeScale = 1;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Pause();
        }

        public void Pause()
        {
            paused = !paused;

            if (pausePanel != null)
                pausePanel.SetActive(paused);

            Time.timeScale = paused ? 0 : 1;
        }

        public void ResumeGame()
        {
            paused = false;

            if (pausePanel != null)
                pausePanel.SetActive(false);

            Time.timeScale = 1;
        }

        public void RestartGame()
        {
            Time.timeScale = 1; 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void QuitGame()
        {
            Time.timeScale = 1; 
            Debug.Log("Quitting Game...");
            Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; 
#endif
        }
    }
}
