using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Prototype3
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

            if (timeRemaining <= 0)
            {
                Time.timeScale = 0;
            }
        }

        public void Restart()
        {
            Time.timeScale = 1;

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void Pause()
        {
            Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        }

        public void Quit()
        {
            Application.Quit();
        }
    }

}
