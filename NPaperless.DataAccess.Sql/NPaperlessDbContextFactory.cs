using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace NPaperless.DataAccess.Sql;

public class NPaperlessDbContextFactory : IDesignTimeDbContextFactory<NPaperlessDbContext>
{
    public NPaperlessDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<NPaperlessDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Database=npaperless;Username=npaperless;Password=npaperless;");

        return new NPaperlessDbContext(optionsBuilder.Options);
    }
}