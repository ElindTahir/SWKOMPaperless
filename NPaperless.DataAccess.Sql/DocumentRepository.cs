using System.Data.SqlClient;
using Dapper;
using NPaperless.DataAccess.Entities;

namespace NPaperless.DataAccess.Sql;

public class DocumentRepository : IRepository<Document>
{
    private readonly string _connectionString;

    public DocumentRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public void Add(Document item)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Execute(
            "INSERT INTO Documents (Name, Description, FileName, FileSize, FileExtension, FileContent) VALUES (@Name, @Description, @FileName, @FileSize, @FileExtension, @FileContent)",
            item);
    }
    
    public Document FindById(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        return connection.QuerySingleOrDefault<Document>("SELECT * FROM Documents WHERE Id = @Id", new { Id = id }) ?? throw new Exception("Document not found");
    }
    
    public IEnumerable<Document> GetAll()
    {
        using var connection = new SqlConnection(_connectionString);
        return connection.Query<Document>("SELECT * FROM Documents");
    }
    
    public void Update(Document item)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Execute(
            "UPDATE Documents SET Name = @Name, Description = @Description, FileName = @FileName, FileSize = @FileSize, FileExtension = @FileExtension, FileContent = @FileContent WHERE Id = @Id",
            item);
    }
    
    public void Delete(Document item)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Execute("DELETE FROM Documents WHERE Id = @Id", item);
    }
    
}