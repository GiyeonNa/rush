using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int checkpointID; 
    public bool isPassed = false; 
    [SerializeField]
    private Material goldMaterial; 

    private Renderer checkpointRenderer;
    private Material originalMaterial; 

    public delegate void CheckpointPassedHandler(int checkpointID);
    public static event CheckpointPassedHandler OnCheckpointPassed;

    private void Awake()
    {
        checkpointRenderer = GetComponent<Renderer>();

        if (checkpointRenderer != null)
            originalMaterial = checkpointRenderer.material;
    }

    public void ReplaceMaterialWithGold()
    {
        if (goldMaterial != null && checkpointRenderer != null)
            checkpointRenderer.material = goldMaterial;
        else
            Debug.LogWarning($"Gold material or renderer is missing on checkpoint {checkpointID}");
    }

    public void ResetMaterialToOriginal()
    {
        if (originalMaterial != null && checkpointRenderer != null)
            checkpointRenderer.material = originalMaterial;
        else
            Debug.LogWarning($"Original material or renderer is missing on checkpoint {checkpointID}");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPassed)
        {
            isPassed = true;
            SpawnManager.Instance.SetSpawnPosition(transform.position);
            OnCheckpointPassed?.Invoke(checkpointID); 
        }
    }
}
