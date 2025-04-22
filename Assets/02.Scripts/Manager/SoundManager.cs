using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField]
    private AudioSource backgroundMusicSource;
    [SerializeField]
    private AudioSource soundEffectSource;

    private Dictionary<string, AudioClip> soundClips = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        base.Awake();
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

    public void PlayBackgroundMusic(string clipName, bool loop = true)
    {
        if (soundClips.TryGetValue(clipName, out AudioClip clip))
        {
            backgroundMusicSource.clip = clip;
            backgroundMusicSource.loop = loop;
            backgroundMusicSource.Play();
        }
        else
        {
            Debug.LogWarning($"Background music clip '{clipName}' not found.");
        }
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
}
