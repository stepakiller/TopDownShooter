using TMPro;
using UnityEngine;

public class ScrapController : MonoBehaviour
{
    public int scrap;
    [SerializeField] TextMeshProUGUI scrapCounter;
    Animator anim;
    void Start()
    {
        if(PlayerPrefs.HasKey("scrap")) scrap = PlayerPrefs.GetInt("scrap");
        anim = scrapCounter.transform.parent.GetComponent<Animator>();
    }
    public void AddScrap()
    {
        scrap += 1;
        UpdateStats();
        anim.Play("Scrap");
    }

    public void SaveScrap()
    {
        PlayerPrefs.SetInt("scrap", scrap);
        PlayerPrefs.Save();
    }

    public void UpdateStats() => scrapCounter.text = scrap.ToString();
}
