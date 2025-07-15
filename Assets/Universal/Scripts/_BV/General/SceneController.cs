using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadScene(string _sceneName) => SceneManager.LoadScene(_sceneName);



    public void LoadTitle() => LoadScene("Title");

    public void Quit() => Application.Quit();

    public void ReloadScene() => LoadScene(SceneManager.GetActiveScene().name);
}
