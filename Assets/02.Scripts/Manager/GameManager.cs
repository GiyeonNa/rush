using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField]
    private float penaltyTime = 5f; // 패널티 시간 (초)

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
        // 스폰 위치 가져오기
        Vector3 spawnPosition = SpawnManager.Instance.GetSpawnPosition();

        // 플레이어를 스폰 위치로 이동
        PlayerManager.Instance.MoveToSpawn(spawnPosition);

        // 타이머에 패널티 추가
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
