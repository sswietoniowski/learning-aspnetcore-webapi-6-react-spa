using Microsoft.EntityFrameworkCore;

public class HouseDbContext : DbContext
{ 
    public DbSet<HouseEntity> Houses => Set<HouseEntity>();

    public HouseDbContext(DbContextOptions<HouseDbContext> options) : base(options) {}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            optionsBuilder.UseSqlite($"Data Source={Path.Join(path, "houses.db")}");
        }
    }    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        SeedData.Seed(modelBuilder);
    }
}