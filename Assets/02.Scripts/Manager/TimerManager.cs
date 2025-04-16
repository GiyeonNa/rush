using UnityEngine;
using TMPro;

public class TimerManager : MonoSingleton<TimerManager>
{
    [SerializeField]
    private TextMeshProUGUI timerText;

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
        base.UpdateLogic();

        //마지막 체크포인트면 타이머 멈추기
        if (CheckPointManager.Instance.IsLastCheckpointReached())
        {
            return;
        }

        currentTime += Time.deltaTime;

        if (timerText != null)
            timerText.text = FormatTime(currentTime);
    }

    
    public void ResetTimer()
    {
        currentTime = 0f;
    }

    public float GetCurrentTime()
    {
        return currentTime;
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int milliseconds = Mathf.FloorToInt((time * 1000) % 1000);

        return $"{minutes:00}.{seconds:00}.{milliseconds:000}";
    }

    public void AddPenalty(float penaltyTime)
    {
        currentTime += penaltyTime;
    }

}
