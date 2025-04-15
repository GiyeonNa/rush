using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NUnit.Framework;
using System.Collections.Generic;

public class TimerManager : MonoSingleton<TimerManager>
{
    [SerializeField] 
    private TextMeshProUGUI timerText;
    [SerializeField]
    private List<Checkpoint> checkpoints; // 모든 체크포인트 리스트

    private Dictionary<int, float> checkpointTimes = new Dictionary<int, float>();
    private float timer;
    private int nextCheckpointIndex = 0; // Track the next checkpoint to pass

    private void Awake()
    {
        base.Awake();
        Checkpoint.OnCheckpointPassed += RecordCheckpointTime;
    }

    public override void Init()
    {
        base.Init();
        timer = 0f;
        checkpointTimes.Clear();
    }

    private void Start()
    {
        Init();

        if (checkpoints.Count > 0)
            checkpoints[nextCheckpointIndex].ReplaceMaterialWithGold();
    }

    private void Update()
    {
        base.UpdateLogic();
        timer += Time.deltaTime;

        if (timerText != null)
            timerText.text = FormatTime(timer);
    }

    public void ResetTimer()
    {
        timer = 0f;
    }

    public float GetCurrentTime()
    {
        return timer;
    }

    public void LogCheckpointTime()
    {
        float checkpointTime = GetCurrentTime();
        Debug.Log($"Checkpoint passed! Time: {checkpointTime:F2} seconds");
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int milliseconds = Mathf.FloorToInt((time * 1000) % 1000);

        return $"{minutes:00}:{seconds:00}:{milliseconds:000}";
    }

    private void RecordCheckpointTime(int checkpointID)
    {
        if (!checkpointTimes.ContainsKey(checkpointID))
        {
            checkpointTimes[checkpointID] = timer;
            Debug.Log($"Checkpoint {checkpointID} passed at {timer:F2} seconds");

            if (nextCheckpointIndex < checkpoints.Count)
                checkpoints[nextCheckpointIndex].ResetMaterialToOriginal();

            nextCheckpointIndex = checkpointID + 1;

            if (nextCheckpointIndex < checkpoints.Count)
                checkpoints[nextCheckpointIndex].ReplaceMaterialWithGold();

            if (checkpointID == checkpoints.Count - 1)
                Debug.Log($"Final checkpoint reached! Total time: {timer:F2} seconds");
        }
    }

    private void OnDestroy()
    {
        Checkpoint.OnCheckpointPassed -= RecordCheckpointTime; // 이벤트 해제
    }

}
