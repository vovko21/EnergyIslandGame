using System.Linq;

public class GameTasksRepository : Repository<GameTaskData>
{
    public GameTasksRepository(DataContext context) : base(context)
    {
    }

    public override GameTaskData Add(GameTaskData entity)
    {
        _context.Data.ActiveTasks.Add(entity);
        return entity;
    }

    public override void Modify(GameTaskData entity)
    {
        var entityToModify = _context.Data.ActiveTasks.FirstOrDefault(e => e.id == entity.id);

        if (entityToModify == null) return;

        entityToModify.id = entity.id;
        entityToModify.name = entity.name;
        entityToModify.description = entity.description;
        entityToModify.isCompleted = entity.isCompleted;
        entityToModify.rewardValue = entity.rewardValue;
        entityToModify.resourceType = entity.resourceType;
        entityToModify.progressCurrent = entity.progressCurrent;
        entityToModify.progressTarget = entity.progressTarget;
        entityToModify.isTook = entity.isTook;
    }

    public override bool Delete(string id)
    {
        var entity = _context.Data.ActiveTasks.FirstOrDefault(e => e.id == id);

        if (entity == null)
        {
            return false;
        }

        _context.Data.ActiveTasks.Remove(entity);

        return true;
    }
}
