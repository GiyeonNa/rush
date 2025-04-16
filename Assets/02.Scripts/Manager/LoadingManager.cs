using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoSingleton<LoadingManager>
{
    public static float PlayerArrivalTime { get; set; }
    public static int PlayerRank { get; set; }

    void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }

    IEnumerator LoadSceneProcess()
    {
        SceneManager.LoadScene("Loading", LoadSceneMode.Additive);
        AsyncOperation op = SceneManager.LoadSceneAsync("Result");
        op.allowSceneActivation = false;

        while (!op.isDone)
        {
            yield return null;

            if (op.progress >= 0.9f)
            {
                // Wait for 0.5 seconds before activating the scene
                yield return new WaitForSeconds(0.5f);
                op.allowSceneActivation = true;
            }
        }
    }

    public void SetResultData(float arrivalTime, bool isBestTime, int rank)
    {
        PlayerArrivalTime = arrivalTime;
        PlayerRank = rank;
    }
}
