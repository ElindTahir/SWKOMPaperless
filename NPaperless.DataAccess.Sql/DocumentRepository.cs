using Dapper;
using NPaperless.DataAccess.Entities;

namespace NPaperless.DataAccess.Sql;

public class DocumentRepository : IRepository<Document>
{
    private readonly NPaperlessDbContext _dbContext;

    public DocumentRepository(NPaperlessDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public void Add(Document item)
    {
        _dbContext.Documents.Add(item);
    }
    
    public Document FindById(int id)
    {
        return _dbContext.Documents.Find(id) ?? throw new KeyNotFoundException();
    }
    
    public IEnumerable<Document> GetAll()
    {
        return _dbContext.Documents.ToList();
    }
    
    public void Update(Document item)
    {
        _dbContext.Documents.Update(item);
    }
    
    public void Delete(Document item)
    {
        _dbContext.Documents.Remove(item);
    }
    
}