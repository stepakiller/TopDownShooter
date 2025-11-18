using TMPro;
using UnityEngine;

public class ScrapController : MonoBehaviour
{
    private int scrap;
    [SerializeField] TextMeshProUGUI scrapCounter;
    Animator anim;
    void Start()
    {
        anim = scrapCounter.transform.parent.GetComponent<Animator>();
    }
    public void AddScrap()
    {
        scrap += 1;
        scrapCounter.text = scrap.ToString();
        anim.Play("Scrap");
    }
}
