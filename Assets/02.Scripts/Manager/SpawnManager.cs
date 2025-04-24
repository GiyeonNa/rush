using UnityEngine;
using System.Threading.Tasks;
using ArcadeVP;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.ResourceLocations;

public class SpawnManager : MonoSingleton<SpawnManager>
{
    [SerializeField]
    private Vector3 spawnPosition;
    [SerializeField]
    private float penaltyTime = 5f; // 패널티 시간 (초)
    [SerializeField]
    private MeshFilter playerMesh;
    [SerializeField]
    private MeshRenderer playerMaterial;

    private Vector3 lastSpawnPosition;

    private Transform playerTransform;

    private async void Awake()
    {
        base.Awake();

        // Load saved model and color from PlayerPrefs
        string savedModelName = PlayerPrefs.GetString("SelectedCarModel", null);
        string savedColorName = PlayerPrefs.GetString("SelectedCarColor", null);

        if (!string.IsNullOrEmpty(savedModelName) && !string.IsNullOrEmpty(savedColorName))
        {
            // Load the saved model and color using Addressables
            AsyncOperationHandle<Mesh> modelHandle = Addressables.LoadAssetAsync<Mesh>(savedModelName);
            await modelHandle.Task;
            if (modelHandle.Status == AsyncOperationStatus.Succeeded)
            {
                playerMesh.mesh = modelHandle.Result;
            }
            else
            {
                Debug.LogError($"Failed to load saved car model: {savedModelName}");
            }

            AsyncOperationHandle<Material> colorHandle = Addressables.LoadAssetAsync<Material>(savedColorName);
            await colorHandle.Task;
            if (colorHandle.Status == AsyncOperationStatus.Succeeded)
            {
                playerMaterial.material = colorHandle.Result;
            }
            else
            {
                Debug.LogError($"Failed to load saved car color: {savedColorName}");
            }
        }
        else
        {
            Debug.LogWarning("No saved car model or color found. Using default values.");
        }
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

        if (isPenalty)
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(spawnPosition, 0.5f);
    }
}
