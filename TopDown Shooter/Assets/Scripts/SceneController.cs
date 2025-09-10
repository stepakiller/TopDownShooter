using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] GameObject settingsWindow;
    public void LoadNextScene(int indexScene)
    {
        SceneManager.LoadScene(indexScene);
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
