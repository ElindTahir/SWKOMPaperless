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
    
    public Document Add(Document item)
    {
        item.Created = DateTime.UtcNow;
        item.CreatedDate = DateTime.UtcNow;
        item.Modified = DateTime.UtcNow;
        item.Added = DateTime.UtcNow;
        _dbContext.Documents.Add(item);
        _dbContext.SaveChanges();
        return item;
    }
    
    public Document FindById(long id)
    {
        return _dbContext.Documents.Find(id) ?? throw new KeyNotFoundException();
    }
    
    public IEnumerable<Document> GetAll()
    {
        return _dbContext.Documents.ToList();
    }
    
    public Document Update(Document item)
    {
        _dbContext.Documents.Update(item);
        _dbContext.SaveChanges();
        return item;
    }
    
    public void Delete(Document item)
    {
        _dbContext.Documents.Remove(item);
        _dbContext.SaveChanges();
    }
    
}