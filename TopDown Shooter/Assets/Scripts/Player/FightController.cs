using System.Collections;
using UnityEngine;

public class FightController : MonoBehaviour
{
    [SerializeField] LayerMask enemyLayers;
    [SerializeField] float impactForce;
    [SerializeField] float maxDistance;
    [SerializeField] float reloadTime;
    [SerializeField] Vector3 boxSize = new Vector3(1f, 0.2f, 0.1f);
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
            if (Physics.BoxCast(transform.position, boxSize / 2, transform.forward, out RaycastHit hit, transform.rotation, maxDistance, enemyLayers))
            {
                if (hit.rigidbody != null)  hit.rigidbody.AddForce(-hit.normal * impactForce, ForceMode.Impulse);
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
