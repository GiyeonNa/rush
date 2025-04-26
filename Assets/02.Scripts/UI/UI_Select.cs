using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class UI_Select : UIPopup
{
    [SerializeField]
    private Button backButton;
    [SerializeField]
    private Button startButton;
    [SerializeField]
    private Button leftButton; 
    [SerializeField]
    private Button rightButton; 
    [SerializeField]
    private RectTransform stageContainer; 
    [SerializeField]
    private GameObject uiStagePrefab; 

    private List<StageSO> stages; 
    private List<UI_Stage> uiStages = new List<UI_Stage>(); 
    private int currentStageIndex = 0;

    private void Awake()
    {
        base.Awake();
        SetBtn(backButton, OnClickBack);
        SetBtn(startButton, OnClickStart);
        SetBtn(leftButton, OnClickLeft); 
        SetBtn(rightButton, OnClickRight); 
    }

    private void Init()
    {
        Addressables.LoadAssetsAsync<StageSO>("StageInfo", null).Completed += OnStagesLoaded;
    }

    private void Start()
    {
        Init();
        base.Open();
    }

    private void OnStagesLoaded(AsyncOperationHandle<IList<StageSO>> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            stages = new List<StageSO>(handle.Result);
            if (stages.Count > 0)
            {
                CreateStageUI();
            }
        }
        else
        {
            Debug.LogError("Failed to load StageInfo from Addressables.");
        }
    }

    private void CreateStageUI()
    {
        foreach (var uiStage in uiStages)
        {
            Destroy(uiStage.gameObject);
        }
        uiStages.Clear();

        foreach (var stage in stages)
        {
            var uiStageInstance = Instantiate(uiStagePrefab, stageContainer).GetComponent<UI_Stage>();
            uiStageInstance.SetStageData(stage);
            uiStages.Add(uiStageInstance);
        }

        UpdateStageUI();
    }



    private void UpdateStageUI()
    {
        for (int i = 0; i < uiStages.Count; i++)
        {
            uiStages[i].gameObject.SetActive(i == currentStageIndex);
        }
    }

    #region Button Functions
    private void OnClickBack()
    {
        SoundManager.Instance.PlayButtonPopupSound();
        SceneManager.LoadScene("Start");
    }

    private void OnClickStart()
    {
        SoundManager.Instance.PlayButtonPopupSound();

        if (stages != null && currentStageIndex >= 0 && currentStageIndex < stages.Count)
        {
            string targetSceneName = stages[currentStageIndex].stageName; 

            string stageDataJson = JsonUtility.ToJson(stages[currentStageIndex]);
            PlayerPrefs.SetString("LoadingType", "InGame");
            PlayerPrefs.SetString("CurrentStageData", stageDataJson);
            PlayerPrefs.SetString("NextScene", targetSceneName); 
            SceneManager.LoadScene("Loading"); 
        }
        else
        {
            Debug.LogError("Invalid stage index or stages list is null.");
        }
    }

    private void OnClickLeft()
    {
        if (stages != null && stages.Count > 0)
        {
            currentStageIndex = (currentStageIndex - 1 + stages.Count) % stages.Count; 
            UpdateStageUI();
        }
    }

    private void OnClickRight()
    {
        if (stages != null && stages.Count > 0)
        {
            currentStageIndex = (currentStageIndex + 1) % stages.Count; 
            UpdateStageUI();
        }
    }
    #endregion
}
