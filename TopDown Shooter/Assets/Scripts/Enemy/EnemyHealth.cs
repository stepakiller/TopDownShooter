using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] ParticleSystem particles;
    [SerializeField] GameObject gears;
    KillCounter killCounter;
    void Start()
    {
        killCounter = FindFirstObjectByType<KillCounter>();
        killCounter.AddKill(1);
    }

    public void GetDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            killCounter.AddKill(-1);
            Instantiate(gears, transform.position, transform.rotation);
            Instantiate(particles, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
