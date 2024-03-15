using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public struct BuildData
{
    public string id;
    public GameObject buildArea;
}

[System.Serializable]
public struct BuildInteration
{
    public List<GameObject> buildArea;
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

    public void Initialize()
    {
        var buildings = StorageService.Instance.GetActiveBuildings();

        if (buildings == null) return;

        foreach (var interation in _buildInterations)
        {
            foreach (var buildArea in interation.buildArea)
            {
                var building = buildArea.GetComponentInChildren<ProductionBuilding>(true);

                var matchedBuilding = buildings.FirstOrDefault(e => e.id == building.Id);

                if (matchedBuilding != null)
                {
                    buildArea.GetComponentInChildren<BuildArea>().Build();
                    building.Initialize(matchedBuilding.produced, matchedBuilding.productionLevelIndex, matchedBuilding.maxSupplyLevelIndex, matchedBuilding.status);
                }
            }
        }
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

        CameraController.Instance.FollowTransforms(CurrentIteration.buildArea.Select(x => x.transform).ToList());
    }

    private void ActivateCurrentIteration()
    {
        var areas = CurrentIteration.buildArea;

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

            foreach (var area in _buildInterations[i].buildArea)
            {
                area.SetActive(false);
            }
        }
    }

    public void OnEvent(BuildingUpdatedEvent eventType)
    {
        var building = _activeBuildings.FirstOrDefault(x => x.Id == eventType.productionBuilding.Id);

        if (building == null)
        {
            if (_buildedOnIteration + 1 == CurrentIteration.buildArea.Count)
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
