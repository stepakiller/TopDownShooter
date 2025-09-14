using UnityEngine;

public class ChangeWeapon : MonoBehaviour
{
    FightController knife;
    PlayerShoot gun;

    void Start()
    {
        knife = GetComponent<FightController>();
        gun = GetComponent<PlayerShoot>();
    }
    void Update()
    {
        if (Input.GetKeyDown(Settings.knifeKey))
        {
            knife.enabled = true;
            gun.enabled = false;
        }
        if (Input.GetKeyDown(Settings.gunKey))
        {
            knife.enabled = false;
            gun.enabled = true;
        }
    }
}
