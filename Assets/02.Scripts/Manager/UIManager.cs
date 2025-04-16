using UnityEngine;
using TMPro;
using ArcadeVP;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField]
    private TextMeshProUGUI timerText;

    [SerializeField]
    private TextMeshProUGUI speedText;

    private TimerManager timerManager;
    private ArcadeVehicleController playerVehicle;

    private void Awake()
    {
        base.Awake();
    }

    public override void Init()
    {
        base.Init();
        timerManager = TimerManager.Instance;
        playerVehicle = FindObjectOfType<ArcadeVehicleController>();
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        UpdateTimerUI();
        UpdateSpeedUI();
    }

    private void UpdateTimerUI()
    {
        if (timerManager != null && timerText != null)
        {
            timerText.text = FormatTime(timerManager.GetCurrentTime());
        }
    }

    private void UpdateSpeedUI()
    {
        if (playerVehicle != null && speedText != null)
        {
            float speed = playerVehicle.carVelocity.magnitude;
            speedText.text = $"{speed:F1} km/h";
        }
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int milliseconds = Mathf.FloorToInt((time * 1000) % 1000);

        return $"{minutes:00}.{seconds:00}.{milliseconds:000}";
    }

    public void DisplayPlayerRank(int rank)
    {
        string rankMessage = rank <= 3 ? $"You are in {rank} place!" : "You are outside the top 3.";
        Debug.Log(rankMessage);
        // Add UI logic here to display the rank on the screen
    }
}
