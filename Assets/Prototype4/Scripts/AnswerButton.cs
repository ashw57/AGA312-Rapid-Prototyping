using UnityEngine;
using TMPro;
using System.Collections;

namespace Prototype4
{
    public class AnswerButton3D : MonoBehaviour
    {
        public int value;
        private int correctValue;
        private EquationGenerator generator;

        [SerializeField] private TextMeshProUGUI feedbackTextIncorrect;
        [SerializeField] private TextMeshProUGUI feedbackTextCorrect;

        public void Setup(int value, int correctValue, EquationGenerator generator,
                           TextMeshProUGUI feedbackCorrect, TextMeshProUGUI feedbackIncorrect)
        {
            this.value = value;
            this.correctValue = correctValue;
            this.generator = generator;
            this.feedbackTextCorrect = feedbackCorrect;
            this.feedbackTextIncorrect = feedbackIncorrect;
        }

        private void OnMouseDown()
        {
            if (value == correctValue)
            {
                GetComponent<Renderer>().material.color = Color.green;

                if (feedbackTextCorrect != null)
                    feedbackTextCorrect.text = "Correct!";

                generator.OnCorrectAnswer();

                StartCoroutine(NextQuestionWithDelay());
            }
            else
            {
                GetComponent<Renderer>().material.color = Color.red;

                if (feedbackTextIncorrect != null)
                    feedbackTextIncorrect.text = "Wrong. Try again!";

                // Remove a reward on incorrect answer
                generator.RemoveReward();
            }
        }

        private IEnumerator NextQuestionWithDelay()
        {
            yield return new WaitForSeconds(1f);
            generator.GenerateRandomEquation();
        }
    }
}