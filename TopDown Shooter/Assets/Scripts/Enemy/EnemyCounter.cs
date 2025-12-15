using TMPro;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI counter;
    [SerializeField] GameObject door;
    int _killCounter;
    int maxEnemies;

    public void Kills()
    {
        _killCounter ++;
        counter.text = $"{_killCounter}/{maxEnemies}";
        if(_killCounter >= maxEnemies) door.SetActive(false);
    }

    public void AddEnemy()
    {
        ++maxEnemies;
        counter.text = $"{_killCounter}/{maxEnemies}";
    }
}
