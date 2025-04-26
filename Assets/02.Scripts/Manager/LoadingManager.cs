using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LoadingManager : MonoSingleton<LoadingManager>
{
    [SerializeField]
    private Slider loadingSlider; // Reference to the slider UI
    [SerializeField]
    private TextMeshProUGUI loadingText; // Reference to the text UI


    [Header("InGame")]
    [SerializeField]
    private GameObject inGameBG;
    [SerializeField]
    private List<TextMeshProUGUI> recordTextList;
    [SerializeField]
    private Image loaadingImage;

    [Header("MainMenu")]
    [SerializeField]
    private GameObject mainMenuBG;

    private void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        DetermineBackground(); // Determine which background to show
        StartCoroutine(LoadSceneProcess());
    }

    private void DetermineBackground()
    {
        string loadingType = PlayerPrefs.GetString("LoadingType", "MainMenu"); // Default to MainMenu

        if (loadingType == "InGame")
        {
            inGameBG.SetActive(true);
            mainMenuBG.SetActive(false);
        }
        else
        {
            inGameBG.SetActive(false);
            mainMenuBG.SetActive(true);
        }
    }

    IEnumerator LoadSceneProcess()
    {
        string nextScene = PlayerPrefs.GetString("NextScene"); 
        string loadingType = PlayerPrefs.GetString("LoadingType", "MainMenu"); // Default to MainMenu

        if (loadingType == "InGame")
        {
            string stageDataJson = PlayerPrefs.GetString("CurrentStageData");
            StageSO currentStageData = ScriptableObject.CreateInstance<StageSO>();
            JsonUtility.FromJsonOverwrite(stageDataJson, currentStageData);

            recordTextList[0].text = FormatTime(currentStageData.firstPlaceTime);
            recordTextList[1].text = FormatTime(currentStageData.secondPlaceTime);
            recordTextList[2].text = FormatTime(currentStageData.thirdPlaceTime);
            loaadingImage.sprite = currentStageData.stageImage;
        }

        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false; 

        while (!op.isDone)
        {
            // Update slider and text with progress
            float progress = Mathf.Clamp01(op.progress / 0.9f); // Normalize progress to 0-1
            if (loadingSlider != null)
                loadingSlider.value = progress;
            if (loadingText != null)
                loadingText.text = $"{(progress * 100):F0}%";

            yield return null;

            if (op.progress >= 0.9f)
            {
                // Wait for 0.5 seconds before activating the scene
                yield return new WaitForSeconds(0.5f);
                op.allowSceneActivation = true;
            }
        }
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int milliseconds = Mathf.FloorToInt((time * 1000) % 1000);
        return $"{minutes:D2}:{seconds:D2}.{milliseconds:D3}";
    }
}
