using UnityEngine;
using UnityEngine.Playables;
using Unity.Cinemachine; // Ensure Cinemachine is imported

public class TimelineManager : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector timelineDirector;

    [SerializeField]
    private CinemachineVirtualCamera timelineCamera; // Reference to the timeline camera

    private int originalPriority;

    private void Start()
    {
        // Save the original priority of the camera
        originalPriority = timelineCamera.Priority;

        // Increase camera priority and disable player input
        timelineCamera.Priority = int.MaxValue;
        TimerManager.Instance.SetTimelinePlaying(true);

        timelineDirector.stopped += OnTimelineStopped;
        timelineDirector.Play();
    }

    private void OnTimelineStopped(PlayableDirector director)
    {
        // Restore the original camera priority
        timelineCamera.Priority = originalPriority;

        // Re-enable player input and start gameplay
        TimerManager.Instance.SetTimelinePlaying(false);
    }
}
