using UnityEngine;

public class ChangeWeapon : MonoBehaviour
{
    [SerializeField] GameObject hands;
    [SerializeField] GameObject weapon;
    FightController knife;
    PlayerShoot gun;
    AnimationsController animCon;

    void Start()
    {
        animCon = GetComponentInChildren<AnimationsController>();
        knife = GetComponent<FightController>();
        gun = GetComponent<PlayerShoot>();
    }
    void Update()
    {
        if (Input.GetKeyDown(Settings.knifeKey))
        {
            animCon.IsHands(true);
            animCon.IsShoot(false);

            knife.enabled = true;
            hands.SetActive(true);

            gun.enabled = false;
            weapon.SetActive(false);

        }
        if (Input.GetKeyDown(Settings.gunKey))
        {
            animCon.IsHands(false);
            animCon.IsShoot(true);


            knife.enabled = false;
            hands.SetActive(false);

            gun.enabled = true;
            weapon.SetActive(true);
        }
    }
}
