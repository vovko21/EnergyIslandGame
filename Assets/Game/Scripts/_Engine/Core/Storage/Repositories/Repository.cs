using System.Collections.Generic;
using System.Linq;

public abstract class Repository<T> where T : Model
{
    protected DataContext _context;

    private List<T> Enteties => _context.Set<T>();

    public Repository(DataContext context)
    {
        _context = context;
    }

    public T GetById(string id)
    {
        return Enteties.FirstOrDefault(e => e.id == id);
    }

    public abstract T Add(T entity);

    public abstract void Modify(T entity);

    public abstract bool Delete(string id);

    public List<T> GetAll()
    {
        return Enteties;
    }

    public void Save()
    {
        _context.SaveAsync();
    }
}
