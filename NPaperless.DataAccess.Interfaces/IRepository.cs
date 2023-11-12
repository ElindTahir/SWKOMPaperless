namespace NPaperless.DataAccess.Sql;

public interface IRepository<T>
{
    void Add(T item);
    T FindById(int id);
    IEnumerable<T> GetAll();
    void Update(T item);
    void Delete(T item);
}
