using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MedKitController : MonoBehaviour
{
    [SerializeField] int maxCount;
    [SerializeField] int getHealth;
    [SerializeField] float healingTime;
    [SerializeField] TextMeshProUGUI countText;
    [SerializeField] Image plus;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] AudioSource soundUse;
    [SerializeField] AudioSource soundPikUP;
    public static MedKitController Instance { get; private set; }
    int count;
    [HideInInspector] public bool isHealing = false;
    float healingTimer;
    void Awake()
    {
        Instance = this;
        UpdateCounter();
    }

    void Update()
    {
        if (Input.GetKeyDown(Settings.medKitKey)) StartHealing();
        if (isHealing)
        {
            healingTimer -= Time.deltaTime;
            timerText.text = healingTimer.ToString("0.0");
            if (healingTimer <= 0)
            {
                count -= 1;
                PlayerPrefs.SetInt("MedKit", count);

                timerText.text = healingTime.ToString("0.0");
                plus.color = new Color(1.000f, 0.000f, 0.000f, 1.000f);
                timerText.gameObject.SetActive(false);

                UpdateCounter();
                PlayerHealth.Instance.GetHealth();
                isHealing = false;
            }
        }
    }
    public bool GetMedkit(int counthp)
    {
        if (count < maxCount)
        {
            count += counthp;

            //soundPikUP.Play();
            PlayerPrefs.SetInt("MedKit", count);
            UpdateCounter();
            return true;
        }
        else return false;
    }

    void StartHealing()
    {
        if (count > 0 && !isHealing && PlayerHealth.Instance.currentHealth < PlayerHealth.Instance.maxHealth)
        {
            isHealing = true;
            plus.color = new Color(0.5f, 0.5f, 0.5f);
            timerText.gameObject.SetActive(true);
            healingTimer = healingTime;
            // soundUse.Play();
        }
    }

    public void CancelHealing()
    {
        if (isHealing)
        {
            isHealing = false;
            timerText.text = healingTime.ToString("0.0");
            plus.color = new Color(1.000f, 0.000f, 0.000f, 1.000f);
            timerText.gameObject.SetActive(false);
            healingTimer = 0;
        }
    }

    void UpdateCounter()
    {
        count = PlayerPrefs.GetInt("MedKit");
        countText.text = count.ToString();
    }
}