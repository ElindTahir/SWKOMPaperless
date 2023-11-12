using System.Data.SqlClient;
using Dapper;

namespace NPaperless.DataAccess.Sql;
using NPaperless.DataAccess.Entities;

public class DocumentTypeRepository: IRepository<DocumentType>
{
    private readonly string _connectionString;
    
    public DocumentTypeRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public void Add(DocumentType item)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Execute(
            "INSERT INTO DocumentTypes (Name, Description) VALUES (@Name, @Description)",
            item);
    }
    
    public DocumentType FindById(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        return connection.QuerySingleOrDefault<DocumentType>("SELECT * FROM DocumentTypes WHERE Id = @Id", new { Id = id }) ?? throw new Exception("DocumentType not found");
    }
    
    public IEnumerable<DocumentType> GetAll()
    {
        using var connection = new SqlConnection(_connectionString);
        return connection.Query<DocumentType>("SELECT * FROM DocumentTypes");
    }
    
    public void Update(DocumentType item)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Execute(
            "UPDATE DocumentTypes SET Name = @Name, Description = @Description WHERE Id = @Id",
            item);
    }
    
    public void Delete(DocumentType item)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Execute("DELETE FROM DocumentTypes WHERE Id = @Id", item);
    }
    
}