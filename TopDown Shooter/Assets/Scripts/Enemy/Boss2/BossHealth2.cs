using UnityEngine;
using UnityEngine.UI;
public class BossHealth2 : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] Image healthBar;
    [SerializeField] ParticleSystem particles;
    Boss2 boss2;
    ScrapController scrapController;
    StartBossFight2 startBossFight2;
    bool stage2 = false;
    int currentHealth;
    void Start()
    {
        boss2 = FindFirstObjectByType<Boss2>();
        startBossFight2 = FindFirstObjectByType<StartBossFight2>();
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
            boss2.SetStage(2);
        }
        else if (currentHealth <= 0)
        {
            startBossFight2.EndFight();
            Instantiate(particles, transform.position, transform.rotation);
            Destroy(gameObject);
            scrapController.SaveScrap();
        }
    }
}
