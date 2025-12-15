using UnityEngine;
public class ColumnsShooting : MonoBehaviour
{
    [SerializeField] LayerMask InteractLayer;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] int bulletCount = 12;
    [SerializeField] float bulletSpeed = 8f;
    [SerializeField] float heightOffset = 0.5f;
    
    
    void OnTriggerEnter(Collider other) => TryShoot(other.gameObject);
    
    private void TryShoot(GameObject target)
    {
        
        if (((1 << target.layer) & InteractLayer.value) != 0) ShootAllDirections();
    }

    public void ShootAllDirections()
    {
        float angleStep = 360f / bulletCount;
        float columnBottomY = GetColumnBottomHeight();
        
        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Cos(angle),0, Mathf.Sin(angle));
            
            Vector3 spawnPosition;
            
            spawnPosition = new Vector3(transform.position.x + direction.x * 0.5f, columnBottomY + heightOffset, transform.position.z + direction.z * 0.5f);
            
            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.LookRotation(direction));
            
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = direction * bulletSpeed;
                rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            }
        }
    }
    
    private float GetColumnBottomHeight()
    {
        Collider collider = GetComponent<Collider>();
        return collider.bounds.min.y;
    }
}