using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace Prototype2
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI gameOverText;
        [SerializeField] private TextMeshProUGUI timer;
        [SerializeField] private GameObject gameOverUI;

        public int score;
        public float timeRemaining = 60;

        private bool gameEnded = false;

        void Start()
        {
            UpdateScore(0);
            UpdateTime(timeRemaining);
            gameOverText.gameObject.SetActive(false);

            UpdateCursorState(true); // Lock and hide cursor once gameplay commences

        }


        void Update()
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTime(timeRemaining);
            }
            else if (!gameEnded)
            {
                UpdateTime(0);
                GameOver();
            }
        }

        public void UpdateScore(int scoreToAdd)
        {
            score += scoreToAdd;
            scoreText.text = "Score: " + score;
        }

        public void UpdateTime(float timeLeft)
        {
            timer.text = "Time remaining: " + Mathf.CeilToInt(timeLeft) + "s";
        }

        public void GameOver()
        {
            gameEnded = true;

            gameOverText.gameObject.SetActive(true);
            gameOverUI.gameObject.SetActive(true);

            UpdateCursorState(false); //Unlock and show cursor and make game over menu accessible

            if (timeRemaining <= 0)
            {
                Time.timeScale = 0;
            }
        }

        private void UpdateCursorState(bool isGameRunning)
        {
            if (isGameRunning)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        public void Restart()
        {
            Time.timeScale = 1;

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
