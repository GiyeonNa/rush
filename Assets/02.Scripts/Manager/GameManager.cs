using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField]
    private float penaltyTime = 5f; // �г�Ƽ �ð� (��)

    private void Awake()
    {
        base.Awake();
    }

    public override void Init()
    {
        base.Init();
    }

    private void Start()
    {
        Init();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            HandleRespawn();
        }
    }

    private void HandleRespawn()
    {
        // ���� ��ġ ��������
        Vector3 spawnPosition = SpawnManager.Instance.GetSpawnPosition();

        // �÷��̾ ���� ��ġ�� �̵�
        PlayerManager.Instance.MoveToSpawn(spawnPosition);

        // Ÿ�̸ӿ� �г�Ƽ �߰�
        TimerManager.Instance.AddPenalty(penaltyTime);
    }

    public void OnPlayerFinish(float finalTime)
    {
        bool isBestTime = RecordManager.Instance.TryUpdateBestTime(finalTime);
        int rank = RecordManager.Instance.GetPlayerRank(finalTime);

        LoadingManager.Instance.SetResultData(finalTime, isBestTime, rank);
        //LoadingManager.Instance.LoadResultScene();
    }
}
