using System.Linq;

public class ResourcesRepository : Repository<ResourceData>
{
    public ResourcesRepository(DataContext context) : base(context)
    {
    }

    public override ResourceData Add(ResourceData entity)
    {
        entity.id = entity.type.ToString();
        _context.Data.Resources.Add(entity);
        return entity;
    }

    public override void Modify(ResourceData entity)
    {
        var resourceToModify = _context.Data.Resources.FirstOrDefault(e => e.id == entity.id);

        if (resourceToModify == null) return;

        resourceToModify.value = entity.value;
    }

    public override bool Delete(string id)
    {
        var entity = _context.Data.Resources.FirstOrDefault(e => e.id == id);

        if (entity == null)
        {
            return false;
        }

        _context.Data.Resources.Remove(entity);

        return true;
    }
}
