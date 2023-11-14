namespace NPaperless.DataAccess.Sql;

public interface IRepository<T>
{
    T Add(T item);
    T FindById(int id);
    IEnumerable<T> GetAll();
    T Update(T item);
    void Delete(T item);
}
