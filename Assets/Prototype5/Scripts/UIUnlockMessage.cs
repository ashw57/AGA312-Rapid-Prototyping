using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

namespace Prototype5
{
    public class UnlockMessageUI : MonoBehaviour
    {
        public static UnlockMessageUI Instance;

        [SerializeField] private TMP_Text messageText; 
        [SerializeField] private float fadeDuration = 2f;
        [SerializeField] private float displayTime = 2f;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);

            if (messageText != null)
                messageText.gameObject.SetActive(false);
        }

        public void ShowMessage()
        {
            if (messageText != null)
                StartCoroutine(FadeMessage());
        }

        private IEnumerator FadeMessage()
        {
            messageText.gameObject.SetActive(true);
            messageText.canvasRenderer.SetAlpha(0f);
            messageText.CrossFadeAlpha(1f, 0.5f, false); // Fade in

            yield return new WaitForSeconds(displayTime);

            messageText.CrossFadeAlpha(0f, fadeDuration, false); // Fade out

            yield return new WaitForSeconds(fadeDuration);

            messageText.gameObject.SetActive(false);
        }
    }
}
