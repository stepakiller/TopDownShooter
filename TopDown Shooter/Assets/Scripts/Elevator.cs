using System.Collections;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    Animator anim;
    [SerializeField] GameObject blackScreen;
    [SerializeField] GameObject player;
    [SerializeField] LayerMask interactableLayer;

    void Start() => anim = GetComponent<Animator>();
    void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & interactableLayer.value) != 0)
        {
            player.SetActive(false);
            StartCoroutine(WaitForAnimationToEnd());
        }
    }

    IEnumerator WaitForAnimationToEnd()
    {
        anim.Play("ElevatorStart");
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        blackScreen.SetActive(true);
    }
}
