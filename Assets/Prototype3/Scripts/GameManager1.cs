using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Prototype3
{
    public enum GameState { Start, Playing, Paused, GameOver }

    public class GameManager : Singleton<GameManager>
    {
       [SerializeField] private TextMeshProUGUI gameOverText;
        [SerializeField] private GameObject gameOverUI;
        [SerializeField] private GameState gameState;
        [SerializeField] private int score;

        public GameState GameState => gameState;

        public float timeRemaining = 60f;

        private bool gameEnded = false;

        public static GameManager Instance { get; private set; }

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            gameState = GameState.Playing;
        }

        private void Update()
        {
            if (!gameEnded && gameState == GameState.Playing)
            {
                timeRemaining -= Time.deltaTime;
                if (timeRemaining <= 0f)
                {
                    timeRemaining = 0f;
                    GameOver();
                }
                _UI?.UpdateTimer(timeRemaining);
            }
        }

        public void GameOver()
        {
            if (gameEnded) return;

            gameEnded = true;
            gameOverText.gameObject.SetActive(true);
            gameOverUI.SetActive(true);

            Time.timeScale = 0f;
            gameState = GameState.GameOver;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public void Restart()
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void Pause()
        {
            if (gameEnded) return;

            if (Time.timeScale == 0f)
            {
                Time.timeScale = 1f;
                gameState = GameState.Playing;
            }
            else
            {
                Time.timeScale = 0f;
                gameState = GameState.Paused;
            }
        }

        public void Quit()
        {
            Application.Quit();
        }

        public void AddScore(int _score)
        {
            score += _score;
            _UI?.UpdateScore(score);
        }
    }

}
