namespace NPaperless.DataAccess.Sql;

public interface IRepository<T>
{
    T Add(T item);
    T FindById(long id);
    IEnumerable<T> GetAll();
    T Update(T item);
    void Delete(T item);
}
