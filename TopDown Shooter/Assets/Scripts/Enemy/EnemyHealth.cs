using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] GameObject healthIndicator;
    [SerializeField] Material[] materials;
    [SerializeField] ParticleSystem particles;
    [SerializeField] GameObject gears;
    Renderer currentMat;
    EnemyCounter enemyCounter;
    void Start()
    {
        enemyCounter = FindFirstObjectByType<EnemyCounter>();
        currentMat = healthIndicator.GetComponent<Renderer>();
        enemyCounter.AddEnemy();
    }

    public void GetDamage(int damage)
    {
        health -= damage;
        if(health >= 75) currentMat.material = materials[0];
        else if(health <= 75 && health >= 25) currentMat.material = materials[1];
        else if(health <= 25 && health >= 0) currentMat.material = materials[2];
        else if (health <= 0)
        {
            Instantiate(gears, transform.position, transform.rotation);
            Instantiate(particles, transform.position, transform.rotation);
            Destroy(gameObject);
            enemyCounter.Kills();
        }
    }
}
