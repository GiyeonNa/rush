using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float penaltyTime = 5f; // 패널티 시간 (초)

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
}
