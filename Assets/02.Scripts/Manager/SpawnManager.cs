using UnityEngine;

public class SpawnManager : MonoSingleton<SpawnManager>
{
    [SerializeField]
    private Vector3 spawnPosition = Vector3.zero;

    public Vector3 GetSpawnPosition()
    {
        return spawnPosition;
    }

    public void SetSpawnPosition(Vector3 newSpawnPosition)
    {
        spawnPosition = newSpawnPosition;
    }
}
