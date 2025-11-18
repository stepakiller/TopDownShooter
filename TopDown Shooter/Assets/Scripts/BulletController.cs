using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] GameObject hitEffect;
    [SerializeField] int bouncesCount;
    [SerializeField] LayerMask interactableLayer;
    int currentBounce;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & interactableLayer.value) != 0)
        {
            Health health = collision.gameObject.GetComponent<Health>();
            PlayerHealth health2 = collision.gameObject.GetComponent<PlayerHealth>();
            EnemyHealth health3 = collision.gameObject.GetComponent<EnemyHealth>();
            TurretHealth health4 = collision.gameObject.GetComponent<TurretHealth>();
            if (health != null) health.GetDamage(damage);
            else if (health2 != null) health2.GetDamage(damage);
            else if (health3 != null) health3.GetDamage(damage);
            else if (health4 != null) health4.GetDamage(damage);
            Instantiate(hitEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        if (currentBounce < bouncesCount)
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            currentBounce++;
        }
        else
        {
            Instantiate(hitEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
