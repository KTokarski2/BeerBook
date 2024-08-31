using Microsoft.EntityFrameworkCore;

public class PgRepository : DbContext
{

    public PgRepository() {}
    public PgRepository(DbContextOptions<PgRepository> options) : base(options) {}
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>(entity => {
            entity.HasData(
                new Role {Id = 1, Name = "User"},
                new Role {Id = 2, Name = "Admin"}
            );
        });
    }

}