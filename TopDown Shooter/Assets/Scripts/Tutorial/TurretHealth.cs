using UnityEngine;
using UnityEngine.UI;
public class TurretHealth : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] Image healthBarFill;
    [SerializeField] ParticleSystem particles;
    int maxHealth;

    void Start()
    {
        maxHealth = health;
    }

    public void GetDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Instantiate(particles, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        UpdateHealthUI();
    }

    void UpdateHealthUI() => healthBarFill.fillAmount = (float)health / maxHealth;
}
