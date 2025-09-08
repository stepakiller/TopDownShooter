using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    [Header("Controls")]
    public static KeyCode fireKey = KeyCode.Mouse0;
    public static KeyCode interactKey = KeyCode.E;
    public static KeyCode runKey = KeyCode.LeftShift;

    [SerializeField] Button saveSettings;
    void Start()
    {
        saveSettings.onClick.AddListener(SaveSettings);
        LoadSettings();
    }

    void SaveSettings()
    {

    }
    
    void LoadSettings()
    {

    }
}
