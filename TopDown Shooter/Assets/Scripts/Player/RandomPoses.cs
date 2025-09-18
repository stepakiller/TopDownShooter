using System.Linq;
using UnityEngine;

public class RandomPoses : MonoBehaviour
{
    [SerializeField] string[] animNames;
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play(animNames[Random.Range(0, animNames.Length)]);
    }
}
