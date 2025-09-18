using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
public class SceneLoader : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI loadingText;
    [SerializeField] Image loadingBackGround;
    [SerializeField] Sprite[] screens;
    int dotCount = 0;

    void Start()
    {
        loadingBackGround.sprite = screens[UnityEngine.Random.Range(0, screens.Length)];
        string sceneToLoad = PlayerPrefs.GetString("SceneToLoad");
        StartCoroutine(LoadSceneAsync(sceneToLoad));
        StartCoroutine(DotsAnimation());
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            if (operation.progress >= 0.9f)
            {
                yield return new WaitForSeconds(5f);
                operation.allowSceneActivation = true;
            }
            yield return null;
        }
    }

    IEnumerator DotsAnimation()
    {
        while (true)
        {
            switch (dotCount % 4)
            {
                case 0:
                    loadingText.text = "Loading";
                    break;
                case 1:
                    loadingText.text = "Loading.";
                    break;
                case 2:
                    loadingText.text = "Loading..";
                    break;
                case 3:
                    loadingText.text = "Loading...";
                    break;
            }
            dotCount++;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
    
