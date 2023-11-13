using NPaperless.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
// Entity Framework Core

namespace NPaperless.DataAccess.Sql;
public class NPaperlessDbContext : DbContext
{
    public DbSet<Correspondent> Correspondents { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<DocumentType> DocumentTypes { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<UserInfo> UserInfos { get; set; }
    
    public NPaperlessDbContext(DbContextOptions<NPaperlessDbContext> options) : base (options)
    {
    }
    
}