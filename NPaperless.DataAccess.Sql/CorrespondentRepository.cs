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

    public Correspondent Add(Correspondent item)
    {
        // add the new correspondent to the database and return the new correspondent with the id
        _dbContext.Correspondents.Add(item);
        _dbContext.SaveChanges();
        return item;
    }

    public Correspondent FindById(long id)
    {
        return _dbContext.Correspondents.Find(id) ?? throw new KeyNotFoundException();
    }

    public IEnumerable<Correspondent> GetAll()
    {
        return _dbContext.Correspondents.ToList();
    }

    public Correspondent Update(Correspondent item)
    {
        _dbContext.Correspondents.Update(item);
        _dbContext.SaveChanges();
        return item;
    }
    
    public Correspondent UpdateContentByFileName(string fileName, string content)
    {
        // Your implementation logic to update UserInfo by fileName
        // This is just a placeholder; you'll need to adapt it to your actual logic
        throw new NotImplementedException();
    }

    public void Delete(Correspondent item)
    {
        _dbContext.Correspondents.Remove(item);
        _dbContext.SaveChanges();
    }
}