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

        // Reinitialize checkpoints if they are null or empty
        if (checkpoints == null || checkpoints.Count == 0)
        {
            checkpoints = new List<Checkpoint>(FindObjectsByType<Checkpoint>(FindObjectsSortMode.None));
            checkpoints.Sort((a, b) => a.checkpointID.CompareTo(b.checkpointID)); // Sort by checkpointID
        }

        for (int i = 0; i < checkpoints.Count; i++)
            checkpoints[i].gameObject.SetActive(i == nextCheckpointIndex);
    }

    private void OnDestroy()
    {
        Checkpoint.OnCheckpointPassed -= RecordCheckpointTime;
    }

    private void Start()
    {
        Init();
    }

    private void RecordCheckpointTime(int checkpointID)
    {
        if (!checkpointTimes.ContainsKey(checkpointID))
        {
            checkpoints[checkpointID].gameObject.SetActive(false);
            nextCheckpointIndex = checkpointID + 1;

            if (nextCheckpointIndex < checkpoints.Count)
                checkpoints[nextCheckpointIndex].gameObject.SetActive(true);
        }
    }

    public bool IsLastCheckpointReached()
    {
        return nextCheckpointIndex >= checkpoints.Count;
    }

    public int GetLastCheckpointID()
    {
        if (checkpoints.Count > 0)
            return checkpoints[checkpoints.Count - 1].checkpointID;

        return -1;
    }
}


