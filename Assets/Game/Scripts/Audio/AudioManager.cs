using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : SingletonMonobehaviour<AudioManager>
{
    [Header("Main settings")]
    [SerializeField] private AudioMixer _audioMixer;

    private const string MASTER_VOLUME = "MasterVolume";
    private const string MUSIC_VOLUME = "MusicVolume";
    private const string SFX_VOLUME = "SFXVolume";

    public void SetMusicVolume(float volume01)
    {
        SetVolume(MUSIC_VOLUME, volume01);
    }

    public void SetSFXVolume(float volume01)
    {
        SetVolume(SFX_VOLUME, volume01);
    }

    private void SetVolume(string mixer, float volume01)
    {
        volume01 = Mathf.Clamp01(volume01);
        if (volume01 == 0) volume01 = 0.0001f;
        _audioMixer.SetFloat(mixer, Mathf.Log10(volume01) * 20);
    }
}