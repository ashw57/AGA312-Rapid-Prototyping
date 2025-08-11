using TMPro;
using UnityEngine;

namespace Prototype3 {
    public class EggTimer : MonoBehaviour
    {
        [Header("Game Over UI")]
        public GameObject gameOverScreen;

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

            if (gameOverScreen != null)
            {
                gameOverScreen.SetActive(true);
            }
        }
    }
}


