using Unity.Mathematics;
using UnityEngine;

public class MedKitPickUp : MonoBehaviour, Interactable
{
    [SerializeField] int hp;
    [SerializeField] ParticleSystem particles;
    public void Interact()
    {
        //Instantiate(particles, transform.position, Quaternion.identity);
        if (MedKitController.Instance.GetMedkit(hp)) Destroy(gameObject);
    }
}
