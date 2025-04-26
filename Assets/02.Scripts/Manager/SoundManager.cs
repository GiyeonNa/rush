using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField]
    private AudioSource backgroundMusicSource;
    [SerializeField]
    private AudioSource soundEffectSource;

    [Header("UI Sound Effects")]
    [SerializeField]
    private AudioClip buttonPopupSoundClip;

    [Header("Background Music")]
    [SerializeField]
    private AudioClip mainMenuMusicClip;
    [SerializeField]
    private AudioClip raceTrackMusicClip;

    [Header("Car Sounds")]
    [SerializeField]
    private AudioClip checkPointSoundClip;

    private Dictionary<string, AudioClip> soundClips = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject); // Ensure SoundManager persists across scenes
    }

    public override void Init()
    {
        base.Init();
    }

    public void PlaySoundEffect(string clipName)
    {
        if (soundClips.TryGetValue(clipName, out AudioClip clip))
            soundEffectSource.PlayOneShot(clip);
        else
            Debug.LogWarning($"Sound clip '{clipName}' not found.");
    }


    public void PlayBackgroundMusic(bool isInGame = false)
    {
        if(isInGame)
            backgroundMusicSource.clip = raceTrackMusicClip;
        else
            backgroundMusicSource.clip = mainMenuMusicClip;

        backgroundMusicSource.loop = true;
        backgroundMusicSource.Play();
    }

    public void StopBackgroundMusic()
    {
        backgroundMusicSource.Stop();
    }

    public void SetVolume(float volume)
    {
        backgroundMusicSource.volume = volume;
        soundEffectSource.volume = volume;
    }

    public void AddSoundClip(string clipName, AudioClip clip)
    {
        if (!soundClips.ContainsKey(clipName))
            soundClips.Add(clipName, clip);
    }

    public void PlayButtonPopupSound()
    {
        soundEffectSource.PlayOneShot(buttonPopupSoundClip);
    }

    public void PlayCheckPointSound()
    {
        soundEffectSource.PlayOneShot(checkPointSoundClip);
    }
}
