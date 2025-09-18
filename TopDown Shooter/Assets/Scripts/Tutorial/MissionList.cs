using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class MissionList : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] missionTexts;
    [SerializeField] GameObject medKitsUIcounter, stamina, health, turret, medkits, currentWeapon;
    [SerializeField] BoxCollider door3;
    [SerializeField] GameObject BlackScreen;

    KeyCode[] stageKeyCodes = {
        KeyCode.None,
        KeyCode.None,
        KeyCode.None,
        KeyCode.None,
        KeyCode.None,
        KeyCode.Alpha2,
        KeyCode.None,
        KeyCode.LeftShift,
        KeyCode.LeftShift,
        KeyCode.None,
        KeyCode.E,
        KeyCode.V
        };
    KeyCode[] stageKeyCodesWASD = {
    KeyCode.W,
    KeyCode.A,
    KeyCode.S,
    KeyCode.D
    };
    public static MissionList Instance { get; private set; }
    public static UnityEvent stage1;
    int stage = 0;
    [SerializeField] FightController fightController;
    [SerializeField] PlayerShoot playerShoot;
    [SerializeField] ChangeWeapon changeWeapon;
    float run;
    float distance;
    void Awake()
    {
        PlayerPrefs.SetInt("MedKit", 0);
    }
    void Start()
    {
        Instance = this;
        stamina.SetActive(false);
        health.SetActive(false);
        turret.SetActive(false);
        medkits.SetActive(false);
        currentWeapon.SetActive(false);
        door3.enabled = false;
        fightController.enabled = false;
        playerShoot.enabled = false;
        changeWeapon.enabled = false;
        medKitsUIcounter.SetActive(false);
        run = PlayerMove.Instance.runSpeed;
        PlayerMove.Instance.runSpeed = PlayerMove.Instance.walkSpeed;
        distance = PlayerMove.Instance.dashDistance;
        PlayerMove.Instance.dashDistance = 0;
        StageUp();
    }

    void Update()
    {
        if (stage < missionTexts.Length)
        {
            if (stage == 1)
            {
                foreach (KeyCode key in stageKeyCodesWASD)
                {
                    if (Input.GetKeyDown(key))
                    {
                        StageUp();
                    }
                }
            }
            else if (stage == 5)
            {
                if (Input.GetKeyDown(stageKeyCodes[stage]))
                {
                    door3.enabled = true;
                    StageUp();
                }
            }
            else if (stage == 7)
            {
                if (Input.GetKeyDown(stageKeyCodes[stage]))
                {
                    StageUp();
                }
            }
            else if (stage == 8)
            {
                if (Input.GetKeyDown(stageKeyCodes[stage]))
                {
                    StageUp();
                }
            }
            else if (stage == 10)
            {
                if (Input.GetKeyDown(stageKeyCodes[stage]))
                {
                    StageUp();
                }
            }
            else if (stage == 11)
            {
                if (Input.GetKeyDown(stageKeyCodes[stage]))
                {
                    StageUp();
                    Invoke("TutorialEnd", 15);
                }
            }
        }
    }
    public void StageUp()
    {
        switch (stage)
        {
            case 0:
                {
                    IncludeText(stage);
                    stage++;
                    break;
                }

            case 1:
                {
                    IncludeText(stage);
                    stage++;
                    break;
                }
            case 2:
                {
                    IncludeText(stage);
                    stage++;
                    break;
                }
            case 3:
                {
                    IncludeText(stage);
                    currentWeapon.SetActive(true);
                    fightController.enabled = true;
                    stage++;
                    break;
                }
            case 4:
                {
                    IncludeText(stage);
                    changeWeapon.enabled = true;
                    health.SetActive(true);
                    stage++;
                    break;
                }
            case 5:
                {
                    IncludeText(stage);
                    stage++;
                    break;
                }
            case 6:
                {
                    IncludeText(stage);
                    PlayerMove.Instance.runSpeed = run;
                    PlayerMove.Instance.dashDistance = distance;
                    stamina.SetActive(true);
                    stage++;
                    break;
                }
            case 7:
                {
                    IncludeText(stage);
                    stage++;
                    break;
                }
            case 8:
                {
                    IncludeText(stage);
                    turret.SetActive(true);
                    stage++;
                    break;
                }
            case 9:
                {
                    IncludeText(stage);
                    medKitsUIcounter.SetActive(true);
                    medkits.SetActive(true);
                    stage++;
                    break;
                }
            case 10:
                {
                    IncludeText(stage);
                    stage++;
                    break;
                }
            case 11:
                {
                    IncludeText(stage);
                    stage++;
                    break;
                }
            default:
                {
                    break;
                }
        }
    }
    void IncludeText(int count)
    {
        for (int i = 0; i < missionTexts.Length; i++)
        {
            missionTexts[i].gameObject.SetActive(false);
        }
        missionTexts[count].gameObject.SetActive(true);
    }

    void TutorialEnd()
    {
        BlackScreen.SetActive(true);
    }
}



