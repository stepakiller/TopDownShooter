using UnityEngine;

public class UpgradeStation : MonoBehaviour, Interactable
{
    [SerializeField] GameObject upgradesScreen;
    [SerializeField] GameObject playerUI;
    
    public void Interact()
    {
        Time.timeScale = 0f;
        upgradesScreen.SetActive(true);
        playerUI.SetActive(false);
    }
}
