using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Prototype3
{

    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private TextMeshProUGUI gameOverText;
        [SerializeField] private GameObject gameOverUI;
        [SerializeField] private int score;

        private bool gameEnded = false;

        public static GameManager Instance { get; private set; }

        void Start()
        {
            //Cursor.lockState = CursorLockMode.Locked;
           // Cursor.visible = false;
        }

        public void GameOver()
        {
            if (gameEnded) return;

            gameEnded = true;
            gameOverText.gameObject.SetActive(true);
            gameOverUI.SetActive(true);

            Time.timeScale = 0f;

            //Cursor.lockState = CursorLockMode.None;
           // Cursor.visible = true;
        }

        public void Restart()
        {
            Time.timeScale = 1f;
           // Cursor.lockState = CursorLockMode.Locked;
            //Cursor.visible = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }


        public void AddScore(int _score)
        {
            score += _score;
            _UI?.UpdateScore(score);
        }
    }

}
