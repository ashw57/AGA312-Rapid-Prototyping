using TMPro;
using UnityEngine;

namespace Prototype4 {
    public class EggTimer : MonoBehaviour
    {
        [Header("Game Over UI")]
        public GameObject gameOverScreen;
        public TextMeshProUGUI correctAnswersText;

        public EquationGenerator equationGenerator;

        public float startTime = 100f;
        private float currentTime;
        private bool isRunning = false;

        public TextMeshPro timerText;

        void Start()
        {
            currentTime = startTime;
            StartTimer();
            UpdateTimerDisplay();
        }

        void Update()
        {
            if (isRunning)
            {
                currentTime -= Time.deltaTime;

                if (currentTime <= 0)
                {
                    currentTime = 0;
                    isRunning = false;
                    OnTimerEnd();
                }

                UpdateTimerDisplay();
            }
        }

        void UpdateTimerDisplay()
        {
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);

            if (timerText != null)
                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        public void StartTimer()
        {
            currentTime = startTime;
            isRunning = true;
        }

        public void StopTimer()
        {
            isRunning = false;
        }

        void OnTimerEnd()
        {
            Debug.Log("Time's up!");

            if (equationGenerator != null)
            {
                equationGenerator.enabled = false; // Optionally stop generation/interaction
            }

            if (gameOverScreen != null)
            {
                gameOverScreen.SetActive(true);
            }

            if (correctAnswersText != null && equationGenerator != null)
            {
                int correct = equationGenerator.GetCorrectAnswerCount();
                int rewards = equationGenerator.GetRewardCount();

                correctAnswersText.text = "Correct Answers: " + correct + "\n" +
                                          "Rewards Collected: " + rewards;
            }
        }
    }
}


