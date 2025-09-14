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

    public void Exit()
    {
        Application.Quit();
    }

    public void SettingsWin(bool ToF)
    {
        settingsWindow.SetActive(ToF);
    }
}
