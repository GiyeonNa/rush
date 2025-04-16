using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float penaltyTime = 5f; // �г�Ƽ �ð� (��)

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
}
