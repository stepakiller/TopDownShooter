using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    [SerializeField] TextMeshProUGUI helthText;
    [SerializeField] GameObject deathScreen;
    public int currentHealth;
    public static PlayerHealth Instance { get; private set; }

    void Start()
    {
        Instance = this;
        currentHealth = maxHealth;
    }

    public void GetHealth(int countHealth)
    {
        currentHealth += countHealth;
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
        }
        UpdateHealth();
    }

    void UpdateHealth()
    {
        //helthText.text = currentHealth.ToString();
    }
}
