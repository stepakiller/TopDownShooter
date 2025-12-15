using UnityEngine;

public class Chekpoint : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform[] spawns;
    
    void Start()
    {
        if(PlayerPrefs.HasKey("Chekpoint"))
        {
            if(PlayerPrefs.GetInt("Chekpoint") == 1)
            {
                player.position = spawns[1].position;
            }
            else player.position = spawns[0].position;
        }
    }
}
