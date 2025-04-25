using UnityEngine;
using TMPro;

public class TimerManager : MonoSingleton<TimerManager>
{
    private float currentTime;
    private StageSO record;
    private bool isTimelinePlaying = false;

    private void Awake()
    {
        base.Awake();
    }

    public override void Init()
    {
        base.Init();
        RecordManager.Instance.Init();
        currentTime = 0f;
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        // If the timeline is playing, block timer updates
        if (isTimelinePlaying || CheckPointManager.Instance.IsLastCheckpointReached())
            return;

        base.UpdateLogic();
        currentTime += Time.deltaTime;
    }

    public void ResetTimer()
    {
        currentTime = 0f;
    }

    public float GetCurrentTime()
    {
        return currentTime;
    }

    public void AddPenalty(float penaltyTime)
    {
        currentTime += penaltyTime;
    }

    public void SetTimelinePlaying(bool isPlaying)
    {
        isTimelinePlaying = isPlaying;
    }
}
