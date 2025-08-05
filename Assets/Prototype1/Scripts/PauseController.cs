using JetBrains.Annotations;
using UnityEngine;

namespace Prototype1
{
    public class PauseController : MonoBehaviour
    {
        public GameObject pausePanel;
        private bool paused;

        void Start()
        {
            paused = false;
            pausePanel.SetActive(paused);
            Time.timeScale = 1;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Pause();
        }

        public void Pause()
        {
            paused = !paused;
            pausePanel.SetActive(paused);
            //? = Return | : = else
            Time.timeScale = paused ? 0 : 1;
        }
    }

}
