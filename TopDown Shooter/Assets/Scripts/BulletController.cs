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
            health.GetDamage(damage);
            //Instantiate(hitEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        if (currentBounce < bouncesCount)
        {
            //Instantiate(hitEffect, transform.position, Quaternion.identity);
            currentBounce++;
        }
        else
        {
            //Instantiate(hitEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
