using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] LayerMask groundMask;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, groundMask))
        {
            Vector3 targetPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            if (Time.timeScale == 1f)transform.LookAt(targetPos);
        }
    }
}
