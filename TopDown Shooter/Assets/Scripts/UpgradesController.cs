using UnityEngine;

public class UpgradesController : MonoBehaviour
{
    [SerializeField] GranadeController granadeController;
    [SerializeField] GameObject granadesUI;
    [SerializeField] ChangeWeapon changeWeapon;
    [SerializeField] BulletController bulletController;
    void Start()
    {
        UpdateStats();
    }

    public void UpdateStats()
    {
        if(PlayerPrefs.HasKey("Shoot")) changeWeapon.enabled = true;
        if(PlayerPrefs.HasKey("Rickoshet"))
        {
            bulletController.bouncesCount = 1;
        }
        else bulletController.bouncesCount = 0;
        
        if(PlayerPrefs.HasKey("Granade"))
        {
            granadesUI.SetActive(true);
            granadeController.enabled = true;
        }
    }
}
