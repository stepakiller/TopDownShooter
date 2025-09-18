using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField] float bulletForce;
    [SerializeField] float fireRate;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;
    GameObject _target;
    float nextFireTime;
    void Start()
    {
        _target = PlayerMove.Instance.gameObject;
    }

    void Update()
    {
        if (_target == null) _target = PlayerMove.Instance.gameObject;
        Vector3 direction = _target.transform.position - transform.position;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        if (Time.time >= nextFireTime) Shoot();
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
        Destroy(bullet, 7.5f);
        nextFireTime = Time.time + fireRate;
    }
    void OnDestroy()
    {
        MissionList.Instance.StageUp();
    }
}
