using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    [Header("Controls")]
    public static KeyCode fireKey = KeyCode.Mouse0;
    public static KeyCode interactKey = KeyCode.E;
    public static KeyCode runKey = KeyCode.LeftShift;
    public static KeyCode dashKey = KeyCode.LeftControl;  
    [Header("Localization")]
    [SerializeField] Button rusLanguage;
    [SerializeField] Button engLanguage;
    int currentLanguage = 0;
    [SerializeField] Button saveSettings;
    void Start()
    {
        rusLanguage.onClick.AddListener(() => SetLanguage(0));
        engLanguage.onClick.AddListener(() => SetLanguage(1));
        LoadSettings();
    }

    void SaveSettings()
    {

    }

    void LoadSettings()
    {

    }

    public void SetLanguage(int count)
    {
        currentLanguage = count;
        PlayerPrefs.SetInt("language", currentLanguage);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
