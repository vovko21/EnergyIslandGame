using System;
using TMPro;
using UnityEngine;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private SwitchToggle _musicToggle;
    [SerializeField] private SwitchToggle _sfxToggle;
    [SerializeField] private TextMeshProUGUI _serverStatusText;

    private void OnEnable()
    {
        _musicToggle.OnToggleChanged += Music_OnToggleChanged;
        _sfxToggle.OnToggleChanged += SFX_OnToggleChanged;
    }

    private void OnDisable()
    {
        _musicToggle.OnToggleChanged -= Music_OnToggleChanged;
        _sfxToggle.OnToggleChanged -= SFX_OnToggleChanged;
    }
   
    public void Initialize()
    {
        _serverStatusText.text = TimeManager.Instance.IsServerTimeSuccess ? "ONLINE" : "PROBLEM";

        _serverStatusText.color = TimeManager.Instance.IsServerTimeSuccess ? Color.green : Color.red;
    }

    private void Music_OnToggleChanged(bool isOn)
    {
        float volume = isOn ? 1f : 0f;
        AudioManager.Instance.SetMusicVolume(volume);
    }

    private void SFX_OnToggleChanged(bool isOn)
    {
        float volume = isOn ? 1f : 0f;
        AudioManager.Instance.SetSFXVolume(volume);
    }

}
