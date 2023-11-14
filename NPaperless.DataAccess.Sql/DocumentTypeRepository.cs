using Dapper;

namespace NPaperless.DataAccess.Sql;
using NPaperless.DataAccess.Entities;

public class DocumentTypeRepository: IRepository<DocumentType>
{
    private readonly NPaperlessDbContext _dbContext;

    
    public DocumentTypeRepository(NPaperlessDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public DocumentType Add(DocumentType item)
    {
        _dbContext.DocumentTypes.Add(item);
        _dbContext.SaveChanges();
        return item;
    }
    
    public DocumentType FindById(long id)
    {
        return _dbContext.DocumentTypes.Find(id) ?? throw new KeyNotFoundException();
    }
    
    public IEnumerable<DocumentType> GetAll()
    {
        return _dbContext.DocumentTypes.ToList();
    }
    
    public DocumentType Update(DocumentType item)
    {
        _dbContext.DocumentTypes.Update(item);
        _dbContext.SaveChanges();
        return item;
    }
    
    public void Delete(DocumentType item)
    {
        _dbContext.DocumentTypes.Remove(item);
        _dbContext.SaveChanges();
    }
    
}