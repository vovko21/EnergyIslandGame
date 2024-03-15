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
        switch (status)
        {
            case BuildingStatus.None:
                break;
            case BuildingStatus.Producing:
                PlayClipSound(_productionClip);
                break;
            case BuildingStatus.NotProducing:
                break;
            case BuildingStatus.Maintenance:
                break;
            case BuildingStatus.MaxedOut:
                break;
            case BuildingStatus.Broken:
                break;
        }
    }

    private void PlayClipSound(AudioClip clip)
    {
        if(_audioSource.isPlaying) _audioSource.Stop();
        _audioSource.clip = clip;
        _audioSource.Play();
    }
}
