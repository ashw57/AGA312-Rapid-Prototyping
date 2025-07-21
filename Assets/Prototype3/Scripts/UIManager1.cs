using UnityEngine;
using TMPro;
using System.Globalization;

namespace Prototype3 
{
    public class UIManager : Singleton<UIManager>
    {
        [SerializeField] private TMP_Text scoreText;


        private void Start()
        {
            ResetUI();
        }

        private void ResetUI()
        {
            UpdateScore(0);

        }

        public void UpdateScore(int _score)
        {
            scoreText.text = "Score: " + _score;
        }

    }
}
