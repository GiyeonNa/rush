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
    private Button optionButton;
    [SerializeField]
    private Button startButton;
    [SerializeField]
    private Button leftButton; // New button for navigating left
    [SerializeField]
    private Button rightButton; // New button for navigating right
    [SerializeField]
    private RectTransform stageContainer; // Container to hold dynamically created UI_Stage objects
    [SerializeField]
    private GameObject uiStagePrefab; // Prefab for UI_Stage

    private List<StageSO> stages; // List to hold StageInfo data
    private List<UI_Stage> uiStages = new List<UI_Stage>(); // List to hold created UI_Stage instances
    private int currentStageIndex = 0;

    private void Awake()
    {
        base.Awake();
        SetBtn(backButton, OnClickBack);
        SetBtn(optionButton, OnClickOption);
        SetBtn(startButton, OnClickStart);
        SetBtn(leftButton, OnClickLeft); // Set left button functionality
        SetBtn(rightButton, OnClickRight); // Set right button functionality
    }

    private void Init()
    {
        // Load StageInfo data from AddressableData
        Addressables.LoadAssetsAsync<StageSO>("StageInfo", null).Completed += OnStagesLoaded;
    }

    private void OnStagesLoaded(AsyncOperationHandle<IList<StageSO>> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            stages = new List<StageSO>(handle.Result);
            if (stages.Count > 0)
            {
                CreateStageUI();
                UpdateStageUI();
            }
        }
        else
        {
            Debug.LogError("Failed to load StageInfo from Addressables.");
        }
    }

    private void CreateStageUI()
    {
        // Clear existing UI_Stage objects
        foreach (var uiStage in uiStages)
        {
            Destroy(uiStage.gameObject);
        }
        uiStages.Clear();

        // Create a UI_Stage object for each StageSO
        foreach (var stage in stages)
        {
            var uiStageInstance = Instantiate(uiStagePrefab, stageContainer).GetComponent<UI_Stage>();
            uiStageInstance.SetStageData(stage);
            uiStages.Add(uiStageInstance);
        }
    }

    private void Start()
    {
        Init();
        base.Open();
    }

    private void UpdateStageUI()
    {
        // Ensure only the current stage is visible
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

    private void OnClickOption()
    {
        SoundManager.Instance.PlayButtonPopupSound();
        Debug.Log("Option button clicked");
    }

    private void OnClickStart()
    {
        SoundManager.Instance.PlayButtonPopupSound();

        if (stages != null && currentStageIndex >= 0 && currentStageIndex < stages.Count)
        {
            string targetSceneName = stages[currentStageIndex].stageName; // Get the target stage name
            PlayerPrefs.SetString("NextScene", targetSceneName); // Store the target scene name in PlayerPrefs
            SceneManager.LoadScene("Loading"); // Load the Loading scene
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
            currentStageIndex = (currentStageIndex - 1 + stages.Count) % stages.Count; // Wrap around to the last stage if at the first stage
            UpdateStageUI();
        }
    }

    private void OnClickRight()
    {
        if (stages != null && stages.Count > 0)
        {
            currentStageIndex = (currentStageIndex + 1) % stages.Count; // Wrap around to the first stage if at the last stage
            UpdateStageUI();
        }
    }
    #endregion
}
