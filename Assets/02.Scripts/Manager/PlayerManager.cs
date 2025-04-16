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
            Debug.LogError("Player 오브젝트를 찾을 수 없습니다. 'Player' 태그를 확인하세요.");
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
