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
    
    public Document UpdateContentByFileName(string fileName, string content)
    {
        // Finden des Dokuments anhand des Dateinamens
        var document = _dbContext.Documents.FirstOrDefault(doc => doc.OriginalFileName == fileName);

        if (document == null)
        {
            throw new KeyNotFoundException("Dokument mit dem gegebenen Dateinamen wurde nicht gefunden.");
        }

        // Aktualisieren des Inhalts
        document.Content = content;
        document.Modified = DateTime.UtcNow; // Setzen des Änderungsdatums

        _dbContext.Documents.Update(document);
        _dbContext.SaveChanges();

        return document;
    }
    
    public void Delete(Document item)
    {
        _dbContext.Documents.Remove(item);
        _dbContext.SaveChanges();
    }
    
}