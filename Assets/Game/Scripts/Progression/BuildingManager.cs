using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public struct BuildInteration
{
    public List<GameObject> buildAreas;
}

public class BuildingManager : SingletonMonobehaviour<BuildingManager>, IEventListener<BuildingUpdatedEvent>, IEventListener<GameEvent>
{
    [SerializeField] private List<BuildInteration> _buildInterations;

    private List<ProductionBuilding> _activeBuildings;

    private int _currentIterationIndex = 0;
    private int _buildedOnIteration = 0;

    public BuildInteration CurrentIteration => _buildInterations[_currentIterationIndex];
    public List<ProductionBuilding> ActiveBuildings => _activeBuildings;

    protected override void Awake()
    {
        base.Awake();
        _activeBuildings = new List<ProductionBuilding>();
    }

    private void OnEnable()
    {
        this.StartListeningEvent<BuildingUpdatedEvent>();
        this.StartListeningEvent<GameEvent>();
    }

    private void OnDisable()
    {
        this.StopListeningEvent<BuildingUpdatedEvent>();
        this.StopListeningEvent<GameEvent>();
    }

    private void Start()
    {
        DeactivateIterations();

        ActivateCurrentIteration();
    }

    private void NextIteration()
    {
        if (_currentIterationIndex + 1 > _buildInterations.Count - 1)
        {
            this.StopListeningEvent<BuildingUpdatedEvent>();
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

            _activeBuildings.Add(eventType.productionBuilding);
        }

        Debug.Log("TotalProductionRate: " + TotalProductionRate());
    }

    public void OnEvent(GameEvent eventType)
    {
        if (eventType.type != GameEventType.BuildingBroken) return;

        if (_activeBuildings.Count == 0) return;

        var random = Random.Range(0, _activeBuildings.Count);

        _activeBuildings[random].Brake();

        var list = new List<Transform>
        {
            _activeBuildings[random].transform
        };

        CameraController.Instance.FollowTransforms(list);
    }

    public int TotalProductionRate()
    {
        int totalProduction = 0;
        foreach (var building in _activeBuildings)
        {
            totalProduction += building.CurrentStats.ProductionPerGameHour;
        }
        return totalProduction;
    }
}
