using UnityEngine;

public class Granade : MonoBehaviour
{
    [SerializeField] float explosionDelay;
    [SerializeField] float explosionRadius;
    [SerializeField] float explosionForce;
    [SerializeField] int damage;
    [SerializeField] float thicknessCircle;
    [SerializeField] Color startColor;
    [SerializeField] Color endColor;

    GameObject circleObject;
    LineRenderer circleRenderer;
    float timer;
    
    void Start()
    {
        timer = explosionDelay;
        CreateExplosionRadiusCircle();
        Invoke("Explode", explosionDelay);
    }

    void CreateExplosionRadiusCircle()
    {
        circleObject = new GameObject("ExplosionRadius");
        circleObject.transform.SetParent(transform);
        circleObject.transform.localPosition = Vector3.zero;
        circleRenderer = circleObject.AddComponent<LineRenderer>();
        circleRenderer.positionCount = 51;
        circleRenderer.useWorldSpace = true;
        circleRenderer.widthMultiplier = thicknessCircle;
        circleRenderer.loop = true;
        circleRenderer.material = new Material(Shader.Find("Sprites/Default"));
        DrawCircle();
    }
    
    void Update()
    {
        if (circleObject != null)
        {
            timer -= Time.deltaTime;
            UpdateCircleColor();
            DrawCircle();
        }
    }
    
    void UpdateCircleColor()
    {
        float progress = timer / explosionDelay;
        Color baseColor = Color.Lerp(endColor, startColor, progress);

        if (progress < 0.3f)
        {
            float blink = Mathf.PingPong(Time.time * 10f, 1f);
            baseColor.a = blink * 0.8f;
        }
        circleRenderer.startColor = baseColor;
        circleRenderer.endColor = baseColor;
    }
    
    void DrawCircle()
    {
        float angle = 0f;
        Vector3 center = transform.position;
        
        for (int i = 0; i < 51; i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * explosionRadius;
            float z = Mathf.Cos(Mathf.Deg2Rad * angle) * explosionRadius;
            
            Vector3 point = center + new Vector3(x, 0.05f, z);
            circleRenderer.SetPosition(i, point);
            angle += 360f / 50f;
        }
    }
    
    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        
        foreach (Collider nearbyObject in colliders)
        {
            Health health = nearbyObject.GetComponent<Health>();
            if (health != null) health.GetDamage(damage);

            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null) rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
        }
        Destroy(gameObject);
    }
}