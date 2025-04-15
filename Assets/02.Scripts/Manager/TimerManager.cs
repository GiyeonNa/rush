using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerManager : MonoSingleton<TimerManager>
{
    [SerializeField] 
    private TextMeshProUGUI timerText;

    private float timer;

    private void Awake()
    {
        base.Awake();
    }

    public override void Init()
    {
        base.Init();
        timer = 0f;
    }

    private void Start()
    {
        Init();
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

    private void Update()
    {
        base.UpdateLogic();
        timer += Time.deltaTime;

        // Update the timerText with the formatted time
        if (timerText != null)
        {
            timerText.text = FormatTime(timer);
        }
    }
}
