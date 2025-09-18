using UnityEngine;

public class ChangeWeapon : MonoBehaviour
{
    [SerializeField] GameObject hands;
    [SerializeField] GameObject weapon;
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
            hands.SetActive(true);

            gun.enabled = false;
            weapon.SetActive(false);

        }
        if (Input.GetKeyDown(Settings.gunKey))
        {
            knife.enabled = false;
            hands.SetActive(false);

            gun.enabled = true;
            weapon.SetActive(true);
        }
    }
}
