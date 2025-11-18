using UnityEngine;

public class KillCounter : MonoBehaviour
{
    [SerializeField] GameObject door;
    int lives;
    public void AddKill(int count)
    {
        lives += count;
        if(lives <= 0) door.SetActive(false);
    }
}
