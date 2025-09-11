using UnityEngine;
using TMPro;

public class Localization : MonoBehaviour
{
    [SerializeField] string[] languages;
    TextMeshProUGUI text;
    int language;

    void Start()
    {
        language = PlayerPrefs.GetInt("language");
        text = GetComponent<TextMeshProUGUI>();
        text.text = languages[language].ToString();
    }
}
