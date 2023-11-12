using System.Data.SqlClient;
using Dapper;

namespace NPaperless.DataAccess.Sql;
using NPaperless.DataAccess.Entities;

public class TagRepository : IRepository<Tag>
{
    private readonly string _connectionString;
    
    public TagRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public void Add(Tag item)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Execute(
            "INSERT INTO Tags (Name) VALUES (@Name)",
            item);
    }
    
    public Tag FindById(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        return connection.QuerySingleOrDefault<Tag>("SELECT * FROM Tags WHERE Id = @Id", new { Id = id }) ?? throw new Exception("Tag not found");
    }
    
    public IEnumerable<Tag> GetAll()
    {
        using var connection = new SqlConnection(_connectionString);
        return connection.Query<Tag>("SELECT * FROM Tags");
    }
    
    public void Update(Tag item)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Execute(
            "UPDATE Tags SET Name = @Name WHERE Id = @Id",
            item);
    }
    
    public void Delete(Tag item)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Execute("DELETE FROM Tags WHERE Id = @Id", item);
    }
    
}