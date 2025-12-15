using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] GameObject settingsWindow;
    public void LoadSceneWithLoadingScreen(string sceneName)
    {
        PlayerPrefs.SetString("SceneToLoad", sceneName);
        SceneManager.LoadScene("LoadingScene");
    }
    public void LoadSceneWithOutLoadingScreen(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void BlackScreen()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void SettingsWin(bool ToF)
    {
        settingsWindow.SetActive(ToF);
    }
}
