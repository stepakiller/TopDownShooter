using UnityEngine;

public class InteractionController : MonoBehaviour
{
    Interactable currentInteractable;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            Interactable interactable = other.GetComponent<Interactable>();
            //появление кнопки Е над обьектом
            if (Input.GetKeyDown(Settings.interactKey))
            {
                interactable.Interact();
            }
        }
    }
}
