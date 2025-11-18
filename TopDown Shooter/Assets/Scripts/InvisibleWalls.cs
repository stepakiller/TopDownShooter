using UnityEngine;

public class InvisibleWalls : MonoBehaviour
{
    [SerializeField] Transform player;
    GameObject lastWall;

    void Update()
    {
        Vector3 cameraPos = transform.position;
        Vector3 playerPos = player.position;

        RaycastHit hit;
        if (Physics.Raycast(cameraPos, (playerPos - cameraPos).normalized, out hit, Vector3.Distance(cameraPos, playerPos)))
        {
            GameObject hitWall = hit.collider.gameObject;
            if (lastWall != hitWall)
            {
                if (lastWall != null) SetWallActive(lastWall, true);
                SetWallActive(hitWall, false);
                lastWall = hitWall;
            }
        }
        else
        {
            if (lastWall != null)
            {
                SetWallActive(lastWall, true);
                lastWall = null;
            }
        }
    }

    void SetWallActive(GameObject wall, bool active)
    {
        MeshRenderer renderer = wall.GetComponent<MeshRenderer>();
        if (renderer != null) renderer.enabled = active;
    }
}
