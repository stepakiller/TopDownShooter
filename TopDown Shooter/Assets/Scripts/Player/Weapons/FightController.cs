using System.Collections;
using UnityEngine;

public class FightController : MonoBehaviour
{
    [SerializeField] LayerMask Layers;
    [SerializeField] int damage;
    [SerializeField] float impactForce;
    [SerializeField] float maxDistance;
    [SerializeField] float reloadTime;
    [SerializeField] Vector3 boxSize = new Vector3(1f, 0.2f, 0.1f);
    [SerializeField] ParticleSystem particles;
    [SerializeField] AudioSource audioSource;
    bool canHit = true;
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(Settings.fireKey) && canHit)
        {
            GetHit();
        }
    }

    void GetHit()
    {
        if (canHit)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            if (Physics.BoxCast(transform.position, boxSize / 2, transform.forward, out RaycastHit hit, transform.rotation, maxDistance, Layers))
            {
                if (hit.rigidbody != null)  hit.rigidbody.AddForce(-hit.normal * impactForce, ForceMode.Impulse);
                Health hp = hit.collider.GetComponent<Health>();
                hp.GetDamage(damage);
                canHit = false;
                StartCoroutine(Reload());
            }
        }
    }
    IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        canHit = true;
    }
}
