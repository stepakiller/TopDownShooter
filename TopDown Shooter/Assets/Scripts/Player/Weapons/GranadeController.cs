using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GranadeController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI counterGranade;
    [SerializeField] Image cooldownImage;
    [SerializeField] GameObject granagePrefab;
    [SerializeField] int maxGranade;
    [SerializeField] float distanceThrowPoint;
    [SerializeField] float thowFocre;
    [SerializeField] float thowHeight;
    [SerializeField] float throwDelay;
    int currentGranade;
    float lastThrowTime;
    bool canThrow = true;
    Rigidbody rb;

    void Start() => currentGranade = maxGranade;

    void Update()
    {
        if (Input.GetKeyDown(Settings.ThrowGranade) && currentGranade > 0 && canThrow)
        {
            currentGranade--;
            ThrowGranage();
            counterGranade.text = currentGranade.ToString();
            canThrow = false;
            lastThrowTime = Time.time;
        }
        
        if (!canThrow)
        {
            float timePassed = Time.time - lastThrowTime;
            float fillAmount = timePassed / throwDelay;
            cooldownImage.fillAmount = fillAmount;
            
            if (timePassed >= throwDelay)
            {
                canThrow = true;
                cooldownImage.fillAmount = 1f;
            }
        }
    }

    void ThrowGranage()
    {
        GameObject granage = Instantiate(granagePrefab, transform.position + transform.forward * distanceThrowPoint, Quaternion.identity);
        rb = granage.GetComponent<Rigidbody>();
        Vector3 direction = transform.forward + Vector3.up * thowHeight;
        rb.AddForce(direction * thowFocre, ForceMode.Impulse);
    }
}
