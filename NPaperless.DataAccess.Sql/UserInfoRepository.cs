using System.Data.SqlClient;
using Dapper;

namespace NPaperless.DataAccess.Sql;
using NPaperless.DataAccess.Entities;

public class UserInfoRepository : IRepository<UserInfo>
{
    private readonly string _connectionString;
    
    public UserInfoRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public void Add(UserInfo item)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Execute(
            "INSERT INTO UserInfo (FirstName, LastName, Email, Password, Salt) VALUES (@FirstName, @LastName, @Email, @Password, @Salt)",
            item);
    }
    
    public UserInfo FindById(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        return connection.QuerySingleOrDefault<UserInfo>("SELECT * FROM UserInfo WHERE Id = @Id", new { Id = id }) ?? throw new Exception("UserInfo not found");
    }
    
    public IEnumerable<UserInfo> GetAll()
    {
        using var connection = new SqlConnection(_connectionString);
        return connection.Query<UserInfo>("SELECT * FROM UserInfo");
    }
    
    public void Update(UserInfo item)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Execute(
            "UPDATE UserInfo SET FirstName = @FirstName, LastName = @LastName, Email = @Email, Password = @Password, Salt = @Salt WHERE Id = @Id",
            item);
    }
    
    public void Delete(UserInfo item)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Execute("DELETE FROM UserInfo WHERE Id = @Id", item);
    }
    
}