using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    [SerializeField] Toggle vSync;
    [Header("Controls")]
    public static KeyCode knifeKey = KeyCode.Alpha1;
    public static KeyCode gunKey = KeyCode.Alpha2;
    public static KeyCode granadeKey = KeyCode.Mouse0;
    public static KeyCode fireKey = KeyCode.Mouse0;
    public static KeyCode interactKey = KeyCode.E;
    public static KeyCode runKey = KeyCode.LeftShift;
    public static KeyCode dashKey = KeyCode.LeftControl;  
    public static KeyCode medKitKey = KeyCode.V;
    public static KeyCode crouchtKey = KeyCode.LeftControl;  
    [Header("Localization")]
    [SerializeField] Button rusLanguage;
    [SerializeField] Button engLanguage;
    int currentLanguage = 0;
    [SerializeField] Button saveSettings;
    void Start()
    {
        QualitySettings.vSyncCount = 1;
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
