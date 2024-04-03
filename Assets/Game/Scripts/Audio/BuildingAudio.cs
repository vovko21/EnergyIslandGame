using UnityEngine;

public class BuildingAudio : MonoBehaviour
{
    [Header("Main settings")]
    [SerializeField] private ProductionBuilding _building;
    [SerializeField] private AudioSource _audioSource;

    [Header("Audio setup")]
    [SerializeField] private AudioClip _productionClip;

    private void OnEnable()
    {
        _building.OnStatusChanged += OnStatusChanged;
    }
    
    private void OnDisable()
    {
        _building.OnStatusChanged -= OnStatusChanged;
    }

    private void OnStatusChanged(BuildingStatus status)
    {
        if(status == BuildingStatus.Producing)
        {
            PlayClipSound(_productionClip);
        }
        else
        {
            StopCurrentClip();
        }
    }

    private void PlayClipSound(AudioClip clip)
    {
        if(_audioSource.isPlaying) _audioSource.Stop();
        _audioSource.clip = clip;
        _audioSource.Play();
    }


    private void StopCurrentClip()
    {
        if (_audioSource.isPlaying) _audioSource.Stop();
    }
}