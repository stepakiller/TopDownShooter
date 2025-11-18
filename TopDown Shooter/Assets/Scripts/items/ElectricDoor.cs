using UnityEngine;

public class ElectricDoor : MonoBehaviour
{
    Animator anim;
    [SerializeField] string OpenAnim;
    
    [SerializeField] string CloseAnim;
    [SerializeField] float time;
    [SerializeField] LayerMask interactableLayer;
    void Start() => anim = GetComponent<Animator>();

    void OnTriggerEnter(Collider collision)
    {
        if (((1 << collision.gameObject.layer) & interactableLayer.value) != 0)
        {
            anim.Play(OpenAnim);
            Invoke("CloseDoor", time);
        }
    }

    void CloseDoor() => anim.Play(CloseAnim);
}
