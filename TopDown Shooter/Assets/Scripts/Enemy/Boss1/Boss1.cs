using System.Collections;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    [SerializeField] LayerMask InteractLayer;
    [SerializeField] int damage;
    [Header("Настройки отскоков")]
    [SerializeField] int bouncesBeforeReturn = 3;
    [SerializeField] float speed = 4f;
    [SerializeField] float returnSpeed = 6f;
    [SerializeField] float rotationSpeed = 180f;
    [SerializeField] float roomSize = 10f;
    [SerializeField] LayerMask wallLayer;
    [Header("На сколько увеличиваются характеристики на втором этапе")]
    [SerializeField] int plusbouncesBeforeReturn;
    [SerializeField] float plusspeed;
    [SerializeField] float plusreturnSpeed;
    [SerializeField] float plusrotationSpeed;

    [Header("Настройки стана")]
    [SerializeField] float reloadStanTime;
    [SerializeField] float StanTime;
    [SerializeField] GameObject[] lightsObjects;
    [SerializeField] Material[] materials;
    Renderer[] currentMat = new Renderer[4];
    Animator anim;
    enum Stages {stage0, stage1, stage2}
    
    Stages currentStage;

    PlayerHealth playerHealth;
    Vector3 roomCenter;
    Vector3 direction;
    int currentBounces = 0;
    bool isReturning = false;

    bool reloadingStan = false;

    void Start()
    {
        playerHealth = FindFirstObjectByType<PlayerHealth>();
        currentMat[0] = lightsObjects[0].GetComponent<Renderer>();
        currentMat[1] = lightsObjects[1].GetComponent<Renderer>();
        currentMat[2] = lightsObjects[2].GetComponent<Renderer>();
        currentMat[3] = lightsObjects[3].GetComponent<Renderer>();
        anim = GetComponent<Animator>();
        roomCenter = transform.position;
        direction = Random.insideUnitSphere;
        direction.y = 0;
        direction.Normalize();
    }

    void Update()
    {
        if(currentStage != Stages.stage0)
        {
            KeepInBounds();
            if (isReturning) ReturnToCenter();
            else Move();
            RotateContinuously();
            if(!reloadingStan) StartCoroutine(ReloadStan());
        }
    }

    public void SetStage(int num)
    {
        if(num == 1)
        {
            currentStage = Stages.stage1;
            anim.Play("Stage1");
        }
        if(num == 2)
        {
            currentStage = Stages.stage2;
            bouncesBeforeReturn += plusbouncesBeforeReturn;
            speed += plusspeed;
            returnSpeed += plusreturnSpeed;
            rotationSpeed += plusrotationSpeed;

            anim.Play("Stage2");
            currentMat[0].material = materials[0];
            currentMat[1].material = materials[0];
            currentMat[2].material = materials[0];
            currentMat[3].material = materials[0];
            lightsObjects[4].SetActive(true);
            lightsObjects[5].SetActive(false);
        }
    }

    IEnumerator ReloadStan()
    {
        reloadingStan = true;
        yield return new WaitForSeconds(reloadStanTime);
        Stages nowStage = currentStage;
        currentStage = Stages.stage0;
        anim.Play("Idle");

        currentMat[0].material = materials[1];
        currentMat[1].material = materials[1];
        currentMat[2].material = materials[2];
        currentMat[3].material = materials[2];
        lightsObjects[4].SetActive(false);
        lightsObjects[5].SetActive(true);

        yield return new WaitForSeconds(StanTime);

        currentMat[0].material = materials[0];
        currentMat[1].material = materials[0];
        currentMat[2].material = materials[0];
        currentMat[3].material = materials[0];
        lightsObjects[4].SetActive(true);
        lightsObjects[5].SetActive(false);

        currentStage = nowStage;
        switch (currentStage)
        {
            case Stages.stage1: anim.Play("Stage1");
            break;

            case Stages.stage2: anim.Play("Stage2");
            break;

            default: anim.Play("Idle");
            break;
        }
        reloadingStan = false;
    }

    void OnTriggerEnter (Collider other)
    {
        if(((1 << other.gameObject.layer) & InteractLayer.value) != 0)
        {
            playerHealth.GetDamage(damage);
        }
    }
    void Move() => transform.Translate(direction * speed * Time.deltaTime, Space.World);

    void RotateContinuously() => transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);

    void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & wallLayer.value) != 0) HandleBounce(collision);
    }

    void HandleBounce(Collision collision)
    {
        currentBounces++;
        Vector3 reflectDir = Vector3.Reflect(direction, collision.contacts[0].normal);
        direction = reflectDir.normalized;
        
        if (currentBounces >= bouncesBeforeReturn)
        {
            StartReturnToCenter();
        }
    }

    void StartReturnToCenter() => isReturning = true;

    public void ReturnToCenter()
    {
        transform.position = Vector3.MoveTowards(transform.position,roomCenter,returnSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, roomCenter) < 0.1f) ResetAfterReturn();
    }

    void ResetAfterReturn()
    {
        isReturning = false;
        currentBounces = 0;
        direction = Random.insideUnitSphere;
        direction.y = 0;
        direction.Normalize();
    }

    void KeepInBounds()
    {
        float halfSize = roomSize / 2f;
        Vector3 pos = transform.position;
        
        pos.x = Mathf.Clamp(pos.x, roomCenter.x - halfSize, roomCenter.x + halfSize);
        pos.z = Mathf.Clamp(pos.z, roomCenter.z - halfSize, roomCenter.z + halfSize);
        
        transform.position = pos;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Vector3 center = Application.isPlaying ? roomCenter : transform.position;
        Gizmos.DrawWireCube(center, new Vector3(roomSize, 0.1f, roomSize));
        
        if (Application.isPlaying)
        {
            float angle = 360f * ((float)currentBounces / bouncesBeforeReturn);
            Gizmos.color = Color.Lerp(Color.green, Color.red, (float)currentBounces / bouncesBeforeReturn);
            Gizmos.DrawWireSphere(transform.position + Vector3.up * 3f, 0.3f);
        }
    }
}
