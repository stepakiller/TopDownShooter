using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FightController : MonoBehaviour
{
    [Header("Настройки атаки")]
    [SerializeField] LayerMask Layers;
    [SerializeField] int damage;
    [SerializeField] float impactForce;
    [SerializeField] float maxDistance;
    [SerializeField] float reloadTime;
    [SerializeField] Vector3 boxSize = new Vector3(1f, 0.2f, 0.1f);
    [SerializeField] Vector3 Offset = new Vector3(0, 0.5f, 0);
    
    [Header("Визуальные элементы")]
    [SerializeField] Image coolDownBar;
    [SerializeField] GameObject hitEffect;
    [SerializeField] AudioSource audioSource;
    
    [Header("Настройки луча")]
    [SerializeField] GameObject beamPrefab;
    [SerializeField] float beamDuration = 0.3f;
    
    bool canHit = true;
    bool isReloading = false;
    float reloadTimer = 0f;
    GameObject beamInstance;
    Coroutine beamHideCoroutine;
    AnimationsController animCon;

    void Start()
    {
        animCon = GetComponentInChildren<AnimationsController>();
        animCon.IsHands(true);
        animCon.IsShoot(false);
        beamInstance = Instantiate(beamPrefab);
        beamInstance.SetActive(false);
    }

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
                canHit = true;
            }
        }
    }

    void GetHit()
    {
        if (!canHit) return;

        animCon.anim.Play("Punch");

        StartCoroutine(ExecuteAfterOneSecond());
    }

    void StartReload()
    {
        canHit = false;
        isReloading = true;
        reloadTimer = 0f;
        coolDownBar.fillAmount = 0f;
        
        //audioSource.Play();
    }

    void DrawSimpleBeam(Vector3 start, Vector3 end)
    {
        Vector3 direction = (end - start).normalized;
        float distance = Vector3.Distance(start, end);
        
        if (distance < 0.01f) 
        {
            HideBeam();
            return;
        }
        beamInstance.transform.position = start + (direction * distance * 0.5f);
        
        if (direction != Vector3.zero) beamInstance.transform.rotation = Quaternion.LookRotation(direction);
        
        beamInstance.transform.localScale = new Vector3(boxSize.x, boxSize.y, distance);
    
        if (beamHideCoroutine != null)
        {
            StopCoroutine(beamHideCoroutine);
        }
        beamInstance.SetActive(true);
        beamHideCoroutine = StartCoroutine(HideBeamAfterDelay(beamDuration));
    }

    IEnumerator HideBeamAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        HideBeam();
    }

    void HideBeam()
    {
        if (beamInstance != null) beamInstance.SetActive(false);
        
        if (beamHideCoroutine != null)
        {
            StopCoroutine(beamHideCoroutine);
            beamHideCoroutine = null;
        }
    }

    IEnumerator ExecuteAfterOneSecond()
    {
        StartReload();
        yield return new WaitForSeconds(0.5f);
        
        Vector3 startPosition = transform.position + Offset;
        if (Physics.BoxCast(startPosition, boxSize / 2, transform.forward, out RaycastHit hit, transform.rotation, maxDistance, Layers))
        {
            Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            
            hit.rigidbody.AddForce(-hit.normal * impactForce, ForceMode.Impulse);
            
            Health health = hit.collider.GetComponent<Health>();
            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
            BossHealth bossHealth = hit.collider.GetComponent<BossHealth>();
            if (health != null) health.GetDamage(damage);
            else if (enemyHealth != null) enemyHealth.GetDamage(damage);
            else if (bossHealth != null) bossHealth.GetDamage(damage);
    
            DrawSimpleBeam(startPosition, hit.point);
        }
        else DrawSimpleBeam(startPosition, startPosition + transform.forward * maxDistance);
            
    }
}
