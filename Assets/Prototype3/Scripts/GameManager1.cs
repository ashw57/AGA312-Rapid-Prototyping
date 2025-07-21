using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Prototype3
{
    public enum GameState { Start, Playing, Paused, GameOver}

    public class GameManager : Singleton<GameManager>
    {
       // [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI gameOverText;
        //[SerializeField] private TextMeshProUGUI timer;
        [SerializeField] private GameObject gameOverUI;

        [SerializeField] private GameState gameState;
        [SerializeField] private int score;

        public GameState GameState => gameState;

        public float timeRemaining = 60;

        private bool gameEnded = false;

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

        public void AddScore(int _score)
        {
            score += _score;
            _UI.UpdateScore(score);
        }
    }

}
