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
    
    public void Add(DocumentType item)
    {
        _dbContext.DocumentTypes.Add(item);
    }
    
    public DocumentType FindById(int id)
    {
        return _dbContext.DocumentTypes.Find(id) ?? throw new KeyNotFoundException();
    }
    
    public IEnumerable<DocumentType> GetAll()
    {
        return _dbContext.DocumentTypes.ToList();
    }
    
    public void Update(DocumentType item)
    {
        _dbContext.DocumentTypes.Update(item);
    }
    
    public void Delete(DocumentType item)
    {
        _dbContext.DocumentTypes.Remove(item);
    }
    
}