using UnityEngine;

public class Door : MonoBehaviour, Interactable
{
    Animator anim;
    [SerializeField] string OpenAnim;
    
    [SerializeField] string CloseAnim;
    [SerializeField] float time;
    void Start() => anim = GetComponent<Animator>();

    public void Interact()
    {
        anim.Play(OpenAnim);
        Invoke("CloseDoor", time);
    }

    void CloseDoor() => anim.Play(CloseAnim);
}
