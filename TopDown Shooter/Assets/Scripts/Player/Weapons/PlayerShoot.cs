using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class PlayerShoot : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float bulletForce;
    [SerializeField] float fireRate;
    bool canShoot = true;

    [Header("Threshold")]
    [SerializeField] float heatPerShot;
    [SerializeField] float cooldownRate;
    [SerializeField] float overheatThreshold;
    [SerializeField] Image heatSlider;
    float currentHeat;
    float nextFireTime;
    void Update()
    {
        CoolWeapon();
        heatSlider.fillAmount = currentHeat / overheatThreshold;
        if (Input.GetKey(Settings.fireKey) && canShoot && Time.time >= nextFireTime)
        {
            if (currentHeat < overheatThreshold) Shoot();
            else canShoot = false;
        }
    }

    void Shoot()
    {
        currentHeat += heatPerShot;
        currentHeat = Mathf.Clamp(currentHeat, 0f, overheatThreshold);

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
        Destroy(bullet, 7.5f);

        nextFireTime = Time.time + fireRate;

        if (currentHeat >= overheatThreshold) canShoot = false;
    }

    void CoolWeapon()
    {
        if (currentHeat > 0f)
        {
            currentHeat -= cooldownRate * Time.deltaTime;
            currentHeat = Mathf.Max(0f, currentHeat);

            if (currentHeat <= overheatThreshold * 0.7f && !canShoot) canShoot = true;
        }
    }
}
