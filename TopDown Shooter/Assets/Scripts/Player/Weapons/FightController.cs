using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FightController : MonoBehaviour
{
    [SerializeField] LayerMask Layers;
    [SerializeField] int damage;
    [SerializeField] float impactForce;
    [SerializeField] float maxDistance;
    [SerializeField] float reloadTime;
    [SerializeField] Vector3 boxSize = new Vector3(1f, 0.2f, 0.1f);
    [SerializeField] Image coolDownBar;
    [SerializeField] GameObject hitEffect;
    [SerializeField] AudioSource audioSource;
    bool canHit = true;
    bool isReloading = false;
    float reloadTimer = 0f;

    void Update()
    {
        if (Input.GetKeyDown(Settings.fireKey) && canHit) GetHit();
        if (isReloading)
        {
            reloadTimer += Time.deltaTime;
            float fillAmount = Mathf.Clamp01(reloadTimer / reloadTime);
            coolDownBar.fillAmount = fillAmount;
            
            if (reloadTimer >= reloadTime)
            {
                isReloading = false;
                reloadTimer = 0f;
                coolDownBar.fillAmount = 1f;
            }
        }
    }

    void GetHit()
    {
        if (canHit)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            if (Physics.BoxCast(transform.position, boxSize / 2, transform.forward, out RaycastHit hit, transform.rotation, maxDistance, Layers))
            {
                Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                if (hit.rigidbody != null) hit.rigidbody.AddForce(-hit.normal * impactForce, ForceMode.Impulse);
                if (hit.collider.GetComponent<Health>())
                {
                    Health hp = hit.collider.GetComponent<Health>();
                    hp.GetDamage(damage);
                }
                else if (hit.collider.GetComponent<EnemyHealth>())
                {
                    EnemyHealth hp = hit.collider.GetComponent<EnemyHealth>();
                    hp.GetDamage(damage);
                }
                canHit = false;
                isReloading = true;
                reloadTimer = 0f;
                coolDownBar.fillAmount = 0f;
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
