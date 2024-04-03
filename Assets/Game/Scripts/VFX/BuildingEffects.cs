using UnityEngine;

public class BuildingEffects : MonoBehaviour, IEventListener<BuildingUpdatedEvent>
{
    [SerializeField] private ParticleSystem _buildedParticles;

    public void StartListen()
    {
        this.StartListeningEvent();
    }

    private void OnDisable()
    {
        this.StopListeningEvent();
    }

    public void OnEvent(BuildingUpdatedEvent eventType)
    {
        _buildedParticles.transform.position = eventType.productionBuilding.transform.position;
        _buildedParticles.Play();
    }
}