using UnityEngine;

public class RecordManager : MonoSingleton<RecordManager>
{
    [SerializeField]
    private StageSO recordSO;

    public StageSO Record => recordSO;

    private void Awake()
    {
        base.Awake();
    }

    public override void Init()
    {
        base.Init();
        if (recordSO == null)
        {
            Debug.LogError("RecordSO is not assigned in RecordManager.");
        }
    }

    public bool TryUpdateBestTime(float playerTime)
    {
        if (recordSO == null) 
            return false;

        return recordSO.UpdateBestTime(playerTime);
    }

    public int GetPlayerRank(float playerTime)
    {
        return recordSO != null ? recordSO.GetPlayerRank(playerTime) : 4;
    }
}
