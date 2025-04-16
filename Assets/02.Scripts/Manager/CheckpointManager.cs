using UnityEngine;
using System.Collections.Generic;

public class CheckPointManager : MonoSingleton<CheckPointManager>
{
    [SerializeField]
    private List<Checkpoint> checkpoints; // 모든 체크포인트 리스트

    private Dictionary<int, float> checkpointTimes = new Dictionary<int, float>();
    private int nextCheckpointIndex = 0;

    private void Awake()
    {
        base.Awake();
        Checkpoint.OnCheckpointPassed += RecordCheckpointTime;
    }

    public override void Init()
    {
        base.Init();
        checkpointTimes.Clear();
        nextCheckpointIndex = 0;

        if (checkpoints.Count > 0)
            checkpoints[nextCheckpointIndex].ReplaceMaterialWithGold();
    }

    private void OnDestroy()
    {
        Checkpoint.OnCheckpointPassed -= RecordCheckpointTime; // 이벤트 해제
    }

    private void Start()
    {
        Init();
    }

    private void RecordCheckpointTime(int checkpointID)
    {
        if (!checkpointTimes.ContainsKey(checkpointID))
        {
            float currentTime = TimerManager.Instance.GetCurrentTime();
            checkpointTimes[checkpointID] = currentTime;
            Debug.Log($"Checkpoint {checkpointID} passed at {currentTime:F2} seconds");

            if (nextCheckpointIndex < checkpoints.Count)
                checkpoints[nextCheckpointIndex].ResetMaterialToOriginal();

            nextCheckpointIndex = checkpointID + 1;

            if (nextCheckpointIndex < checkpoints.Count)
                checkpoints[nextCheckpointIndex].ReplaceMaterialWithGold();

            if (checkpointID == checkpoints.Count - 1)
                Debug.Log($"Final checkpoint reached! Total time: {currentTime:F3} seconds");
        }
    }

    public bool IsLastCheckpointReached()
    {
        return nextCheckpointIndex >= checkpoints.Count;
    }
}


