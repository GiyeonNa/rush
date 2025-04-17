using UnityEngine;

public class SpawnManager : MonoSingleton<SpawnManager>
{
    [SerializeField]
    private Vector3 spawnPosition = Vector3.zero;

    [SerializeField]
    private float penaltyTime = 5f; // �г�Ƽ �ð� (��)

    private Vector3 lastSpawnPosition;

    private Transform playerTransform;


    private void Awake()
    {
        base.Awake();
        playerTransform = GameObject.FindWithTag("Player").transform;

#if UNITY_EDITOR
        if (playerTransform == null)
            Debug.LogError("Player ������Ʈ�� ã�� �� �����ϴ�. 'Player' �±׸� Ȯ���ϼ���.");
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
        //RŰ�� ������ �÷��̾� ������
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
