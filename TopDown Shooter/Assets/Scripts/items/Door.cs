using UnityEngine;

public class Door : MonoBehaviour, Interactable
{
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Interact()
    {
        anim.Play("OpenDoor");
        Invoke("CloseDoor", 5);
    }

    void CloseDoor()
    {
        anim.Play("CloseDoor");
    }
}
