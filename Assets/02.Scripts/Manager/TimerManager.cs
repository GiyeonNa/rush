using UnityEngine;
using TMPro;

public class TimerManager : MonoSingleton<TimerManager>
{
    private float currentTime;
    private RecordSO record;

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
        //������ üũ����Ʈ�� Ÿ�̸� ���߱�
        if (CheckPointManager.Instance.IsLastCheckpointReached())
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

}
