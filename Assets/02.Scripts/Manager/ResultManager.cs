using UnityEngine;
using TMPro;

public class ResultManager : MonoSingleton<ResultManager>
{
    [SerializeField]
    private TextMeshProUGUI playerTimeText;
    [SerializeField]
    private TextMeshProUGUI rankText;

    private void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        base.Init();

        float playerTime = LoadingManager.PlayerArrivalTime;
        int rank = LoadingManager.PlayerRank;

        SetResultData(playerTime, rank);
    }

    public override void Init()
    {
        base.Init();
    }

    public void SetResultData(float playerTime, int rank)
    {
        playerTimeText.text = $"Time: {playerTime:F2} seconds";
        rankText.text = $"Rank: {rank}";
    }
}
