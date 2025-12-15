using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] Image healthBar;
    [SerializeField] ParticleSystem particles;
    Boss1 boss1;
    ScrapController scrapController;
    StartBossFight startBossFight;
    bool stage2 = false;
    int currentHealth;
    void Start()
    {
        boss1 = FindFirstObjectByType<Boss1>();
        startBossFight = FindFirstObjectByType<StartBossFight>();
        currentHealth = maxHealth;
    }

    public void GetDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealth();
    }
    void UpdateHealth()
    {
        healthBar.fillAmount = (float)currentHealth / maxHealth;
        float healthPercent = (float)currentHealth / maxHealth * 100f;
        
        if (healthPercent <= 50f && !stage2)
        {
            stage2 = true;
            boss1.ReturnToCenter();
            boss1.SetStage(2);
        }
        else if (currentHealth <= 0)
        {
            startBossFight.EndFight();
            Instantiate(particles, transform.position, transform.rotation);
            Destroy(gameObject);
            scrapController.SaveScrap();
        }
    }
}
