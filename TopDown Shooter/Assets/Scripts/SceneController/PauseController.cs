using System;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;
    void Start()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !pauseScreen.activeSelf) PauseOn();
        else if (Input.GetKeyDown(KeyCode.Escape) && pauseScreen.activeSelf) PauseOff();
    }

    void PauseOn()
    {
        Time.timeScale = 0f;
        pauseScreen.SetActive(true);
    }

    public void PauseOff()
    {
        Time.timeScale = 1f;
        pauseScreen.SetActive(false);
    }
}
