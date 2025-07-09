using Microsoft.EntityFrameworkCore;
using AquaPomodoro.Models;

namespace AquaPomodoro.Data;

public class AquaPomodoroDbContext : DbContext
{
    public AquaPomodoroDbContext(DbContextOptions<AquaPomodoroDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Fish> Fishes { get; set; }
    public DbSet<Aquarium> Aquariums { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Set default schema
        modelBuilder.HasDefaultSchema("AquariumPomodoro");

        // Configure enum mapping for PostgreSQL
        modelBuilder.Entity<Fish>()
            .Property(f => f.Type)
            .HasConversion<string>();

        // Configure User entity
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(u => u.Mail).IsUnique();
            entity.Property(u => u.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        // Configure Fish entity
        modelBuilder.Entity<Fish>(entity =>
        {
            entity.Property(f => f.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        // Configure Aquarium entity
        modelBuilder.Entity<Aquarium>(entity =>
        {
            entity.Property(a => a.AddedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            
            entity.HasOne(a => a.User)
                .WithMany(u => u.Aquariums)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(a => a.Fish)
                .WithMany(f => f.Aquariums)
                .HasForeignKey(a => a.FishId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
} 