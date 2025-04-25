using UnityEngine;
using UnityEngine.Playables;
using Unity.Cinemachine; // Ensure Cinemachine is imported
using UnityEngine.UI; // Ensure UI namespace is imported
using System.Collections;
using ArcadeVP; // Required for Coroutine

public class TimelineManager : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector timelineDirector;
    [SerializeField]
    private ArcadeVehicleController Contoroller;
    [SerializeField]
    private CinemachineCamera playerCamera; // Reference to the timeline camera
    [SerializeField]
    private GameObject uiDrive;
    [SerializeField]
    private GameObject uiHud;
    [SerializeField]
    private Image Fade;


    private int originalPriority;

    private void Start()
    {
        playerCamera.Priority = 0;
        TimerManager.Instance.SetTimelinePlaying(true);

        // Disable the Contoroller GameObject
        Contoroller.gameObject.SetActive(false);

        uiDrive.SetActive(false);
        uiHud.SetActive(false);
        Fade.color = new Color(Fade.color.r, Fade.color.g, Fade.color.b, 1f);

        timelineDirector.stopped += OnTimelineStopped;
        timelineDirector.Play();
    }

    private void OnTimelineStopped(PlayableDirector director)
    {
        playerCamera.Priority = 80;

        Fade.color = new Color(Fade.color.r, Fade.color.g, Fade.color.b, 0.5f);

        // Activate UI and fade out
        uiDrive.SetActive(true);
        uiHud.SetActive(true);
        StartCoroutine(DelayedStart());
    }

    private IEnumerator DelayedStart()
    {
        // Wait for 3 seconds
        yield return new WaitForSeconds(3f);

        Fade.color = new Color(Fade.color.r, Fade.color.g, Fade.color.b, 0f);
        Contoroller.gameObject.SetActive(true);
        TimerManager.Instance.SetTimelinePlaying(false);
        CheckPointManager.Instance.ActivateNextCheckpointOnly();
    }
}
