using UnityEngine;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    private Transform playerTransform;

    private void Awake()
    {
        base.Awake();
        playerTransform = GameObject.FindWithTag("Player").transform;
        if (playerTransform == null)
        {
            Debug.LogError("Player ������Ʈ�� ã�� �� �����ϴ�. 'Player' �±׸� Ȯ���ϼ���.");
        }
    }

    public void MoveToSpawn(Vector3 spawnPosition)
    {
        if (playerTransform != null)
        {
            playerTransform.position = spawnPosition;
        }
    }
}
