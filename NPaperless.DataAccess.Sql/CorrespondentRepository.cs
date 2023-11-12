using System.Data.SqlClient;
using NPaperless.DataAccess.Entities;
using Dapper;
namespace NPaperless.DataAccess.Sql;

public class CorrespondentRepository : IRepository<Correspondent>
{
    private readonly string _connectionString;

    public CorrespondentRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void Add(Correspondent item)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Execute(
            "INSERT INTO Correspondents (Name, Address, City, State, ZipCode) VALUES (@Name, @Address, @City, @State, @ZipCode)",
            item);
    }

    public Correspondent FindById(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        return connection.QuerySingleOrDefault<Correspondent>("SELECT * FROM Correspondents WHERE Id = @Id", new { Id = id }) ?? throw new Exception("Correspondent not found");
    }

    public IEnumerable<Correspondent> GetAll()
    {
        using var connection = new SqlConnection(_connectionString);
        return connection.Query<Correspondent>("SELECT * FROM Correspondents");
    }

    public void Update(Correspondent item)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Execute(
            "UPDATE Correspondents SET Name = @Name, Address = @Address, City = @City, State = @State, ZipCode = @ZipCode WHERE Id = @Id",
            item);
    }

    public void Delete(Correspondent item)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Execute("DELETE FROM Correspondents WHERE Id = @Id", item);
    }
}