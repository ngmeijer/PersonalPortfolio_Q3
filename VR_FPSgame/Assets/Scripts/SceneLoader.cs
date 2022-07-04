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
    [SerializeField] private bool EnableTestMode;
    [SerializeField] private bool EnableAsync;
    private float timer;

    private void Start()
    {
        if(EnableTestMode && EnableAsync) StartCoroutine(LoadYourAsyncScene());
    }

    private void Update()
    {
        if (EnableTestMode && !EnableAsync)
        {
            timer += Time.deltaTime;

            if (timer > 2)
            {
                EnableTestMode = false;
                LoadNewScene();
            }
        }
    }

    public void LoadNewSceneAsync()
    {
        if (asyncLoad.progress >= 0.9f) asyncLoad.allowSceneActivation = true;
    }

    public void LoadNewScene()
    {
        SceneManager.LoadScene(1);
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
        if(EnableTestMode) LoadNewSceneAsync();
    }
}