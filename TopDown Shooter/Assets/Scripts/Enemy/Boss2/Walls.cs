using System.Collections;
using UnityEngine;

public class Walls : MonoBehaviour
{
    [SerializeField] LayerMask InteractLayer;
    [SerializeField] float timeInvisible;
    int hp = 3;
    MeshRenderer meshRenderer;
    MeshCollider meshCollider;
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshCollider = GetComponent<MeshCollider>();
    }
    void OnCollisionEnter (Collision collision)
    {
        if(((1 << collision.gameObject.layer) & InteractLayer.value) != 0 && hp > 0)
        {
            hp -= 1;
            if(hp <= 0) StartCoroutine(InvisibleWalls());
        }
    }

    IEnumerator InvisibleWalls()
    {
        meshRenderer.enabled = false;
        meshCollider.enabled = false;
        yield return new WaitForSeconds(timeInvisible);
        meshRenderer.enabled = true;
        meshCollider.enabled = true;
        hp = 3;
    }
}
