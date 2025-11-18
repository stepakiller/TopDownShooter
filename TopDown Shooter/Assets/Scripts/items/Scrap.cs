using UnityEngine;

public class Scrap : MonoBehaviour
{
    [SerializeField] LayerMask interactableLayer;
    ScrapController scrapController;
    void Start()
    {
        scrapController = FindFirstObjectByType<ScrapController>();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & interactableLayer.value) != 0)
        {
            scrapController.AddScrap();
            Destroy(gameObject);
        }
    }
}
