using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultManager : MonoSingleton<ResultManager>
{
    private float playTime;
    private int rank;

    private void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        base.Init();
        playTime = LoadingManager.PlayerArrivalTime;
        rank = LoadingManager.PlayerRank;
    }

    public override void Init()
    {
        base.Init();
    }

    public float GetTime()
    {
        return playTime;
    }

    public int GetRank()
    {
        return rank;
    }

}
