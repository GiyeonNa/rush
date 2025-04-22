using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultManager : MonoSingleton<ResultManager>
{
    [SerializeField]
    private TextMeshProUGUI playerTimeText;
    [SerializeField]
    private TextMeshProUGUI rankText;
    [SerializeField]
    private Button backButton;
    [SerializeField]
    private Button retryButton;

    private void Awake()
    {
        base.Awake();
        backButton.onClick.AddListener(OnClickBack);
        retryButton.onClick.AddListener(OnClickRetry);
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

    private void OnClickBack()
    {
        // �ڷΰ��� ����
        SceneManager.LoadScene("Select");
    }
    private void OnClickRetry()
    {
        // ����� ����
        SceneManager.LoadScene("City");
    }
}
