using System.Collections;
using TMPro;
using UnityEngine;

public class MedKitController : MonoBehaviour
{
    [SerializeField] int maxCount;
    [SerializeField] int getHealth;
    [SerializeField] float healingTime;
    [SerializeField] TextMeshProUGUI countText;
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
            if (healingTimer <= 0)
            {
                count -= 1;
                PlayerPrefs.SetInt("MedKit", count);
                UpdateCounter();
                isHealing = false;
                //лечение игрока
                PlayerHealth.Instance.GetHealth(getHealth);
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
            healingTimer = healingTime;
            // soundUse.Play();
        }
    }

    public void CancelHealing()
    {
        if (isHealing)
        {
            isHealing = false;
            healingTimer = 0;
        }
    }

    void UpdateCounter()
    {
        count = PlayerPrefs.GetInt("MedKit");
        countText.text = count.ToString();
    }
}