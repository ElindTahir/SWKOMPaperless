using Dapper;

namespace NPaperless.DataAccess.Sql;
using NPaperless.DataAccess.Entities;

public class UserInfoRepository : IRepository<UserInfo>
{
    private readonly NPaperlessDbContext _dbContext;
    
    public UserInfoRepository(NPaperlessDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public UserInfo Add(UserInfo item)
    {
        _dbContext.UserInfos.Add(item);
        _dbContext.SaveChanges();
        return item;
    }
    
    public UserInfo FindById(long id)
    {
        return _dbContext.UserInfos.Find(id) ?? throw new KeyNotFoundException();
    }
    
    public IEnumerable<UserInfo> GetAll()
    {
        return _dbContext.UserInfos.ToList();
    }
    
    public UserInfo Update(UserInfo item)
    {
        _dbContext.UserInfos.Update(item);
        _dbContext.SaveChanges();
        return item;
    }
    
    public UserInfo UpdateContentByFileName(string fileName, string content)
    {
        // Your implementation logic to update UserInfo by fileName
        // This is just a placeholder; you'll need to adapt it to your actual logic
        throw new NotImplementedException();
    }
    
    public void Delete(UserInfo item)
    {
        _dbContext.UserInfos.Remove(item);
        _dbContext.SaveChanges();
    }
    
}