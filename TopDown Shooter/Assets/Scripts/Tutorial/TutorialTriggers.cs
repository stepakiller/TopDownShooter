using UnityEngine;

public class TutorialTriggers : MonoBehaviour
{
    [SerializeField] LayerMask interactableLayer;
    void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & interactableLayer.value) != 0)
        {
            MissionList.Instance.StageUp();
            Destroy(gameObject);
        }
    }
}
