using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    public int checkpointID; 
    public bool isPassed = false; 

    public delegate void CheckpointPassedHandler(int checkpointID);
    public static event CheckpointPassedHandler OnCheckpointPassed;

    private void Awake()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPassed)
        {
            isPassed = true;
            SpawnManager.Instance.SetSpawnPosition(transform.position);
            OnCheckpointPassed?.Invoke(checkpointID);

            //마지막 체크포인트인지 확인
            if (checkpointID == CheckPointManager.Instance.GetLastCheckpointID())
            {
                float currentTime = TimerManager.Instance.GetCurrentTime();
                RecordManager.Instance.TryUpdateBestTime(currentTime);
                int rank = RecordManager.Instance.GetPlayerRank(currentTime);

                // LoadingManager에 데이터 저장
                LoadingManager.PlayerArrivalTime = currentTime;
                LoadingManager.PlayerRank = rank;

                //TOOD :: 연출?
                //RecordManager.Instance.TryUpdateBestTime(TimerManager.Instance.GetCurrentTime());
                //RecordManager.Instance.GetPlayerRank(TimerManager.Instance.GetCurrentTime());
                SceneManager.LoadScene("Result");
            }
         
        }
    }
}
