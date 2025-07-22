using UnityEngine;
using TMPro;
using System.Globalization;

namespace Prototype3 
{
    public class UIManager : Singleton<UIManager>
    {
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text timerText;


        private void Start()
        {
            ResetUI();
        }

        private void ResetUI()
        {
            UpdateScore(0);
            UpdateTimer(0f);
        }

        public void UpdateScore(int _score)
        {
            scoreText.text = "Score: " + _score;
        }

        public void UpdateTimer(float timeRemaining)
        {
            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);

            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

    }
}
