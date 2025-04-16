using UnityEngine;

[CreateAssetMenu(fileName = "Record", menuName = "Scriptable Objects/Record")]
public class RecordSO : ScriptableObject
{
    // ���� �ְ��� ó������ firstPlaceTime�� �ʱ�ȭ
    public float bestTime;
    // 1��
    public float firstPlaceTime;
    // 2��
    public float secondPlaceTime;
    // 3��
    public float thirdPlaceTime;

    private void OnEnable()
    {
        if(bestTime == 0)
            bestTime = firstPlaceTime;
    }

    public int GetPlayerRank(float playerTime)
    {
        if (playerTime < firstPlaceTime)
            return 1;
        else if (playerTime < secondPlaceTime)
            return 2;
        else if (playerTime < thirdPlaceTime)
            return 3;
        else
            return 4; 
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
