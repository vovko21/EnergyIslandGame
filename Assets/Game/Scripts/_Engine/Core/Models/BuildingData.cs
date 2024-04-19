[System.Serializable]
public class BuildingData : Model
{
    public int produced;
    public int productionLevelIndex;
    public int maxSupplyLevelIndex;
    public BuildingStatus status;

    public EnergyResource energyResource;
}
