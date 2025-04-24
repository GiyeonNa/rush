using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Stage", menuName = "Scriptable Objects/Stage")]
public class StageSO : ScriptableObject
{
    public string stageName;
    public Sprite stageImage;
    public float bestTime;
    public float firstPlaceTime;
    public float secondPlaceTime;
    public float thirdPlaceTime;

    private void OnEnable()
    {
        //if(bestTime == 0)
        //    bestTime = firstPlaceTime;
    }

    public int GetPlayerRank(float playerTime)
    {
        if (playerTime < firstPlaceTime)
            return 1;
        else if (playerTime < secondPlaceTime)
            return 2;
        else
            return 3;
    }

    public bool UpdateBestTime(float playerTime)
    {
        if (playerTime < bestTime || bestTime == 0)
        {
            bestTime = playerTime;
            return true; 
        }
        return false; 
    }
}
