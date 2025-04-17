using UnityEngine;

public class SpawnManager : MonoSingleton<SpawnManager>
{
    [SerializeField]
    private Vector3 spawnPosition = Vector3.zero;

    [SerializeField]
    private float penaltyTime = 5f; // 패널티 시간 (초)

    private Vector3 lastSpawnPosition;

    private Transform playerTransform;


    private void Awake()
    {
        base.Awake();
        playerTransform = GameObject.FindWithTag("Player").transform;

#if UNITY_EDITOR
        if (playerTransform == null)
            Debug.LogError("Player 오브젝트를 찾을 수 없습니다. 'Player' 태그를 확인하세요.");
#endif
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
        //R키를 누르면 플레이어 리스폰
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerRespawn();
        }
    }

    public void PlayerRespawn(bool isPenalty = false)
    {
        playerTransform.position = GetSpawnPosition();
        playerTransform.rotation = Quaternion.identity;
        TimerManager.Instance.AddPenalty(penaltyTime);
    }

    public Vector3 GetSpawnPosition()
    {
        return spawnPosition;
    }

    public void SetSpawnPosition(Vector3 newSpawnPosition)
    {
        spawnPosition = newSpawnPosition;
    }
}
