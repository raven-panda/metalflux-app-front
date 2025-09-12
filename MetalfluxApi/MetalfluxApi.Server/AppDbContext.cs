using MetalfluxApi.Server.Modules.User;
using Microsoft.EntityFrameworkCore;

namespace MetalfluxApi.Server;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<UserModel> Users => Set<UserModel>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserModel>().HasIndex(u => u.Email).IsUnique();
    }
}
