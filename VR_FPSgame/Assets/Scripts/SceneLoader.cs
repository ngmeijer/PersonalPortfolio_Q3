using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private AsyncOperation asyncLoad;
    private bool progressToNextScene;
    [SerializeField] private TextMeshProUGUI loadingProgressText;

    private void Start()
    {
        StartCoroutine(LoadYourAsyncScene());
    }

    public void LoadNewScene()
    {
        if(asyncLoad.progress >= 0.9f) asyncLoad.allowSceneActivation = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadYourAsyncScene()
    {
        asyncLoad = SceneManager.LoadSceneAsync(1);
        asyncLoad.allowSceneActivation = false;

        while (asyncLoad.progress < 0.9f)
        {
            loadingProgressText.SetText("Loading progress: " + (asyncLoad.progress * 100) + "%");
            
            yield return null;
        }
        
        loadingProgressText.SetText("Loading done!"); 
    }
}