using UnityEngine;

public class Boss2 : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float fireRate = 1f;
    [SerializeField] float bulletForce = 10f;
    enum Stages {stage0, stage1, stage2}
    Stages currentStage;
    GameObject _target;
    float nextFireTime;
    
    void Start() => _target = PlayerMove.Instance.gameObject;
    
    void Update()
    {
        if(currentStage != Stages.stage0)
        {
            if (_target == null) _target = PlayerMove.Instance.gameObject;
            Vector3 direction = _target.transform.position - transform.position;
            direction.y = 0;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
            if (Time.time >= nextFireTime) Shoot();
        }
    }
    
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
        nextFireTime = Time.time + fireRate;
    }

    public void SetStage(int num)
    {
        if(num == 1)
        {
            currentStage = Stages.stage1;
        }
        if(num == 2)
        {
            currentStage = Stages.stage2;
            fireRate = 1.5f;
            bulletForce = 40;
            /* bouncesBeforeReturn += plusbouncesBeforeReturn;
            speed += plusspeed;
            returnSpeed += plusreturnSpeed;
            rotationSpeed += plusrotationSpeed;

            currentMat[0].material = materials[0];
            currentMat[1].material = materials[0];
            currentMat[2].material = materials[0];
            currentMat[3].material = materials[0];
            lightsObjects[4].SetActive(true);
            lightsObjects[5].SetActive(false); */
        }
    }
}
