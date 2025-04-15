using UnityEngine;
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

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int milliseconds = Mathf.FloorToInt((time * 1000) % 1000);

        return $"{minutes:00}:{seconds:00}:{milliseconds:000}";
    }
}
