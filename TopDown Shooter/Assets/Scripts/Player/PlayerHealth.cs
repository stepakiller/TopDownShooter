using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    [SerializeField] TextMeshProUGUI helthText;
    [HideInInspector] public int currentHealth;
    public static PlayerHealth Instance { get; private set; }

    void Start()
    {
        Instance = this;
        currentHealth = maxHealth;
    }

    void Update()
    {

    }

    public void GetHealth(int countHealth)
    {
        currentHealth += countHealth;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
    }
}
