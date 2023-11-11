namespace NPaperless.DataAccess.Sql;
using Microsoft.Extensions.Logging;

public class SqlRepository<T> : IRepository<T> where T : class
{
    private readonly ILogger<SqlRepository<T>> _logger;

    //Implement the IRepository interface
    public void Add(T item)
    {
        _logger.LogInformation("Adding a new Correspondent");
        throw new NotImplementedException();
    }
    
    public T FindById(int id)
    {
        throw new NotImplementedException();
    }
    
    public IEnumerable<T> GetAll()
    {
        throw new NotImplementedException();
    }
    
    public void Update(T item)
    {
        throw new NotImplementedException();
    }
    
    public void Delete(T item)
    {
        throw new NotImplementedException();
    }
}
