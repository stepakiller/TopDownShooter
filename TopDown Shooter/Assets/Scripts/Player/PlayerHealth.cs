using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    [SerializeField] GameObject deathScreen;
    [SerializeField] Image healthBar;
    public float currentHealth;
    public static PlayerHealth Instance { get; private set; }

    void Start()
    {
        Instance = this;
        currentHealth = maxHealth;
    }
    public void GetHealth()
    {
        if (currentHealth > 0 && currentHealth < 20) currentHealth = 20;
        else if (currentHealth >= 20 && currentHealth < 40) currentHealth = 40;
        else if (currentHealth >= 40 && currentHealth < 60) currentHealth = 60;
        else if (currentHealth >= 60 && currentHealth < 80) currentHealth = 80;
        else if (currentHealth >= 80 && currentHealth < 100) currentHealth = 100;
        UpdateHealth();
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
            UpdateHealth();
        }
    }

    public void GetDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            deathScreen.SetActive(true);
            Destroy(gameObject);
        }
        UpdateHealth();
    }

    void UpdateHealth()
    {
        healthBar.fillAmount = currentHealth / maxHealth;
    }
}
