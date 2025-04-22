using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadingManager : MonoSingleton<LoadingManager>
{
    public static float PlayerArrivalTime { get; set; }
    public static int PlayerRank { get; set; }

    [SerializeField]
    private Slider loadingSlider; // Reference to the slider UI
    [SerializeField]
    private TextMeshProUGUI loadingText; // Reference to the text UI

    void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }

    IEnumerator LoadSceneProcess()
    {
        string nextScene = PlayerPrefs.GetString("NextScene"); // Get the next scene name
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        while (!op.isDone)
        {
            // Update slider and text with progress
            float progress = Mathf.Clamp01(op.progress / 0.9f); // Normalize progress to 0-1
            if (loadingSlider != null)
                loadingSlider.value = progress;
            if (loadingText != null)
                loadingText.text = $"{(progress * 100):F0}%";

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
