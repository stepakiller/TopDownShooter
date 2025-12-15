using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesShop : MonoBehaviour
{
    [SerializeField] GameObject playerUI;
    [SerializeField] GameObject upgradesScreen;
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] TextMeshProUGUI price;
    [SerializeField] TextMeshProUGUI currentGears;
    [SerializeField] GameObject[] windows;
    [SerializeField] Button Player;

    [Header("Стрельба")]
    [SerializeField] Button shoot;
    [SerializeField] string titleShoot;
    [SerializeField] string descripShoot;
    [SerializeField] int priceShoot;
    [SerializeField] Button unlockShoot;
    [SerializeField] GameObject lineUpgrade;

    [Header("Рикошеты")]
    [SerializeField] string titleRickoshet;
    [SerializeField] string descripRickoshet;
    [SerializeField] int priceRickoshet;
    [SerializeField] Button selectRickoshet;
    [SerializeField] Button buyRickoshet;

    [Header("Гранаты")]
    [SerializeField] string titleGranades;
    [SerializeField] string descripGranades;
    [SerializeField] int priceGranades;
    [SerializeField] Button selectGranades;
    [SerializeField] Button buyGranades;

    ScrapController scrapController;
    UpgradesController upgradesController;
    void Start()
    {
        scrapController = FindFirstObjectByType<ScrapController>();
        upgradesController = FindFirstObjectByType<UpgradesController>();
        
        Player.onClick.AddListener(OpenPlayerWindow);

        selectGranades.onClick.AddListener(SelectGranade);
        buyGranades.onClick.AddListener(BuyGranade);
        



        shoot.onClick.AddListener(OpenShootWindow);
        unlockShoot.onClick.AddListener(UnlockShoot);
        selectRickoshet.onClick.AddListener(SelectRickoshet);
        buyRickoshet.onClick.AddListener(BuyRickoshet);


        currentGears.text = scrapController.scrap.ToString();
    }

    void Update()
    {
        if(upgradesScreen.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            upgradesScreen.SetActive(false);
            Time.timeScale = 1f;
            playerUI.SetActive(true);
        }
    }

    void OpenPlayerWindow()
    {
        Debug.Log("1");
        for (int i = 0; i < windows.Length; i++)
        {
            windows[i].SetActive(false);
        }
        windows[0].SetActive(true);
        title.text = "";
        description.text = "";
        price.text = "";
    }
    void SelectGranade()
    {
        title.text = titleGranades;
        description.text = descripGranades;
        price.text = $"Цена: {priceGranades}";
        if(!PlayerPrefs.HasKey("Granade")) buyGranades.gameObject.SetActive(true);
    }
    void BuyGranade()
    {
        if(scrapController.scrap >= priceGranades && !PlayerPrefs.HasKey("Granade"))
        {
            scrapController.scrap -= priceGranades;
            scrapController.UpdateStats();
            scrapController.SaveScrap();

            PlayerPrefs.SetInt("Granade", 1);
            currentGears.text = scrapController.scrap.ToString();

            upgradesController.UpdateStats();

            buyGranades.gameObject.SetActive(false);
        }
    }

    void OpenShootWindow()
    {
        Debug.Log("2");
        for (int i = 0; i < windows.Length; i++)
        {
            windows[i].SetActive(false);
        }
        windows[1].SetActive(true);
        title.text = titleShoot;
        description.text = descripShoot;
        price.text = $"Цена: {priceShoot}";
    }

    void UnlockShoot()
    {
        if(scrapController.scrap >= priceShoot && !PlayerPrefs.HasKey("Shoot"))
        {
            scrapController.scrap -= priceShoot;
            scrapController.UpdateStats();
            scrapController.SaveScrap();

            PlayerPrefs.SetInt("Shoot", 1);
            currentGears.text = scrapController.scrap.ToString();

            upgradesController.UpdateStats();

            unlockShoot.gameObject.SetActive(false);
            lineUpgrade.SetActive(true);
        }
    }

    void SelectRickoshet()
    {
        title.text = titleRickoshet;
        description.text = descripRickoshet;
        price.text = $"Цена: {priceRickoshet}";
        if(!PlayerPrefs.HasKey("Rickoshet")) buyRickoshet.gameObject.SetActive(true);
    }

    void BuyRickoshet()
    {
        if(scrapController.scrap >= priceRickoshet && !PlayerPrefs.HasKey("Rickoshet"))
        {
            scrapController.scrap -= priceRickoshet;
            scrapController.UpdateStats();
            scrapController.SaveScrap();
            currentGears.text = scrapController.scrap.ToString();
            PlayerPrefs.SetInt("Rickoshet", 1);
            upgradesController.UpdateStats();
            buyRickoshet.gameObject.SetActive(false);
        }
    }
}
