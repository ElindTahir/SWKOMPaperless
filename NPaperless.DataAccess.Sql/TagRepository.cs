﻿using Dapper;

namespace NPaperless.DataAccess.Sql;
using NPaperless.DataAccess.Entities;

public class TagRepository : IRepository<Tag>
{
    private readonly NPaperlessDbContext _dbContext;
    
    public TagRepository(NPaperlessDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Tag Add(Tag item)
    {
        _dbContext.Tags.Add(item);
        _dbContext.SaveChanges();
        return item;
    }
    
    public Tag FindById(long id)
    {
        return _dbContext.Tags.Find(id) ?? throw new KeyNotFoundException();
    }
    
    public IEnumerable<Tag> GetAll()
    {
        return _dbContext.Tags.ToList();
    }
    
    public Tag Update(Tag item)
    {
        _dbContext.Tags.Update(item);
        _dbContext.SaveChanges();
        return item;
    }
    
    public Tag UpdateContentByFileName(string fileName, string content)
    {
        // Your implementation logic to update UserInfo by fileName
        // This is just a placeholder; you'll need to adapt it to your actual logic
        throw new NotImplementedException();
    }
    
    public void Delete(Tag item)
    {
        _dbContext.Tags.Remove(item);
        _dbContext.SaveChanges();
    }
    
}