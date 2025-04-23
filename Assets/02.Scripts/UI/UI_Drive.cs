using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using ArcadeVP;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class UI_Drive : UIPopup
{
    [SerializeField]
    private TextMeshProUGUI speedText;
    [SerializeField]
    private List<TextMeshProUGUI> lapTimeTexts;
    [SerializeField]
    private Button pauseButton;

    private ArcadeVehicleController playerVehicle;
    private int currentLapIndex = 0; // Tracks the current lap being updated

    void Awake()
    {
        base.Awake();
        playerVehicle = Object.FindFirstObjectByType<ArcadeVehicleController>();
        SetBtn(pauseButton, OnClickPause);
        Checkpoint.OnCheckpointPassed += CheckpointPassed;
    }

    private void Init()
    {
        for (int i = 0; i < lapTimeTexts.Count; i++)
            SetText(lapTimeTexts[i], "--:--:---");
        currentLapIndex = 0;
    }

    private void Start()
    {
        base.Open();
        Init();
    }

    private void Update()
    {
        UpdateSpeedUI();
        UpdateTimerUI();
    }

    private void UpdateSpeedUI()
    {
        if (playerVehicle != null && speedText != null)
        {
            float speed = playerVehicle.carVelocity.magnitude;
            SetText(speedText, $"{speed:F1} km/h");
        }
    }

    private void UpdateTimerUI()
    {
        if (currentLapIndex < lapTimeTexts.Count)
        {
            float currentTime = TimerManager.Instance.GetCurrentTime();
            SetText(lapTimeTexts[currentLapIndex], FormatTime(currentTime));

        }
    }

    private void CheckpointPassed(int checkpointID)
    {
        if (checkpointID < lapTimeTexts.Count)
        {
            // Record the final time for the current lap
            float passTime = TimerManager.Instance.GetCurrentTime();
            SetText(lapTimeTexts[checkpointID], FormatTime(passTime));

            // Move to the next lap
            currentLapIndex = checkpointID + 1;
        }
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int milliseconds = Mathf.FloorToInt((time * 1000) % 1000);
        return $"{minutes:D2}:{seconds:D2}:{milliseconds:D3}";
    }

    private void OnClickPause()
    {
        // Pause the game
        Time.timeScale = 0f;
        UIManager.Instance.ShowUI("UI_Pause");
    }



    private void OnDestroy()
    {
        Checkpoint.OnCheckpointPassed -= CheckpointPassed;
    }
}

