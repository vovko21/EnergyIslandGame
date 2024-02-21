using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public struct BuildInteration
{
    public List<GameObject> buildAreas;
}

public class BuildingManager : MonoBehaviour, IEventListener<BuildingUpdatedEvent>
{
    [SerializeField] private List<BuildInteration> _buildInterations;

    private List<ProductionBuilding> _productionBuildings;

    private int _currentIterationIndex = 0;
    private int _buildedOnIteration = 0;

    public BuildInteration CurrentIteration => _buildInterations[_currentIterationIndex];

    private void Awake()
    {
        _productionBuildings = new List<ProductionBuilding>();
    }

    private void OnEnable()
    {
        this.StartListeningEvent();
    }

    private void OnDisable()
    {
        this.StopListeningEvent();
    }

    private void Start()
    {
        DeactivateIterations();

        ActivateCurrentIteration();
    }

    public void NextIteration()
    {
        if (_currentIterationIndex + 1 > _buildInterations.Count - 1)
        {
            this.StopListeningEvent();
            return;
        }

        _currentIterationIndex++;

        ActivateCurrentIteration();

        CameraController.Instance.FollowTransforms(CurrentIteration.buildAreas.Select(x => x.transform).ToList());
    }

    private void ActivateCurrentIteration()
    {
        var areas = CurrentIteration.buildAreas;

        foreach (var area in areas)
        {
            area.SetActive(true);
        }
    }

    private void DeactivateIterations()
    {
        for (int i = 0; i < _buildInterations.Count; i++)
        {
            if (_currentIterationIndex == i)
            {
                continue;
            }

            foreach (var area in _buildInterations[i].buildAreas)
            {
                area.SetActive(false);
            }
        }
    }

    public void OnEvent(BuildingUpdatedEvent eventType)
    {
        if (eventType.upgraded == false)
        {
            if (_buildedOnIteration + 1 == CurrentIteration.buildAreas.Count)
            {
                NextIteration();
                _buildedOnIteration = 0;
            }
            else
            {
                _buildedOnIteration++;
            }

            _productionBuildings.Add(eventType.productionBuilding);
        }

        Debug.Log("TotalProductionRate: " + TotalProductionRate());
    }

    public int TotalProductionRate()
    {
        int totalProduction = 0;
        foreach (var building in _productionBuildings)
        {
            totalProduction += building.CurrentStats.ProductionPerGameHour;
        }
        return totalProduction;
    }
}
