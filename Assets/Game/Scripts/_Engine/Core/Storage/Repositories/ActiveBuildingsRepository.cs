using System.Linq;

public class ActiveBuildingsRepository : Repository<BuildingData>
{
    public ActiveBuildingsRepository(DataContext context) : base(context)
    {
    }

    public override BuildingData Add(BuildingData entity)
    {
        _context.Data.ActiveBuildings.Add(entity);
        return entity;
    }

    public override void Modify(BuildingData entity)
    {
        var buildingToModify = _context.Data.ActiveBuildings.FirstOrDefault(e => e.id == entity.id);

        if (buildingToModify == null) return;

        buildingToModify.id = entity.id;
        buildingToModify.produced = entity.produced;
        buildingToModify.productionLevelIndex = entity.productionLevelIndex;
        buildingToModify.maxSupplyLevelIndex = entity.maxSupplyLevelIndex;
        buildingToModify.status = entity.status;
    }

    public override bool Delete(string id)
    {
        var entity = _context.Data.ActiveBuildings.FirstOrDefault(e => e.id == id);

        if (entity == null)
        {
            return false;
        }

        _context.Data.ActiveBuildings.Remove(entity);

        return true;
    }
}
