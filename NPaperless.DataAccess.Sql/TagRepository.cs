using Dapper;

namespace NPaperless.DataAccess.Sql;
using NPaperless.DataAccess.Entities;

public class TagRepository : IRepository<Tag>
{
    private readonly NPaperlessDbContext _dbContext;
    
    public TagRepository(NPaperlessDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public void Add(Tag item)
    {
        _dbContext.Tags.Add(item);
    }
    
    public Tag FindById(int id)
    {
        return _dbContext.Tags.Find(id) ?? throw new KeyNotFoundException();
    }
    
    public IEnumerable<Tag> GetAll()
    {
        return _dbContext.Tags.ToList();
    }
    
    public void Update(Tag item)
    {
        _dbContext.Tags.Update(item);
    }
    
    public void Delete(Tag item)
    {
        _dbContext.Tags.Remove(item);
    }
    
}