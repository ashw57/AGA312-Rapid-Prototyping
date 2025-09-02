using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Prototype5
{
    public class SceneFader : MonoBehaviour
    {
        public static SceneFader Instance;

        [SerializeField] private Image fadeImage;
        [SerializeField] private float fadeDuration = 1f;

        private void Start()
        {
            StartCoroutine(Fade(0));
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void FadeOutAndReload()
        {
            StartCoroutine(FadeAndReload());
        }

        private IEnumerator FadeAndReload()
        {
            yield return StartCoroutine(Fade(1)); // Fade to black

            yield return new WaitForSeconds(0.5f);

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private IEnumerator Fade(float targetAlpha)
        {
            float startAlpha = fadeImage.color.a;
            float elapsed = 0f;

            while (elapsed < fadeDuration)
            {
                elapsed += Time.deltaTime;
                float alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / fadeDuration);
                fadeImage.color = new Color(0, 0, 0, alpha);
                yield return null;
            }

            fadeImage.color = new Color(0, 0, 0, targetAlpha);
        }
    }
}
