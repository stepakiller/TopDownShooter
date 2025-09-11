using UnityEngine;

public class ItemPickUp : MonoBehaviour, Interactable
{
    public void Interact()
    {
        Destroy(gameObject);
    }
}
