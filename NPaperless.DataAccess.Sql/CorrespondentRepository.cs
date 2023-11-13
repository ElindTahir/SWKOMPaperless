using NPaperless.DataAccess.Entities;
using Dapper;
namespace NPaperless.DataAccess.Sql;

public class CorrespondentRepository : IRepository<Correspondent>
{
    private readonly NPaperlessDbContext _dbContext;

    public CorrespondentRepository(NPaperlessDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(Correspondent item)
    {
        _dbContext.Correspondents.Add(item);
        _dbContext.SaveChanges();
    }

    public Correspondent FindById(int id)
    {
        return _dbContext.Correspondents.Find(id) ?? throw new KeyNotFoundException();
    }

    public IEnumerable<Correspondent> GetAll()
    {
        return _dbContext.Correspondents.ToList();
    }

    public void Update(Correspondent item)
    {
        _dbContext.Correspondents.Update(item);
    }

    public void Delete(Correspondent item)
    {
        _dbContext.Correspondents.Remove(item);
    }
}