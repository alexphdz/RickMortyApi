using Microsoft.EntityFrameworkCore;

public class RickMortyDbContext : DbContext
{
    public DbSet<Character> Characters { get; set; }

    public RickMortyDbContext(DbContextOptions<RickMortyDbContext> options)
        : base(options)
    {
     
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Episode>()
            .HasKey(e => e.Id);

        modelBuilder.Entity<Character>()
            .HasMany(c => c.Episodes)
            .WithOne(e => e.Character)
            .HasForeignKey(e => e.CharacterId);

    }
}
