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
    
    public void Delete(UserInfo item)
    {
        _dbContext.UserInfos.Remove(item);
        _dbContext.SaveChanges();
    }
    
}