using Microsoft.EntityFrameworkCore;
using WebAPIEgitimi.HastaKabulWebAPI.Models;

namespace WebAPIEgitimi.HastaKabulWebAPI.Context;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Patient> Patients { get; set; }
    public DbSet<Muayene> Muayeneler { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasQueryFilter(x => !x.IsDeleted);
        });

        modelBuilder.Entity<Muayene>(entity =>
        {
            entity.HasQueryFilter(x => !x.IsDeleted);
            entity.Property(i => i.PatientStatus)
                .HasConversion(
                    v => v.Value,
                    v => PatientStatusEnum.FromValue(v));

            entity
            .HasOne(p => p.Patient)
            .WithOne()
            .HasForeignKey<Muayene>(p => p.PatientId)
            .OnDelete(DeleteBehavior.NoAction);
        });
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is Abstractions.Entity entity)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entity.CreatedAt = DateTimeOffset.Now;
                        break;
                    case EntityState.Modified:
                        if (entity.IsDeleted)
                        {
                            entity.DeletedAt = DateTimeOffset.UtcNow;
                        }
                        else
                        {
                            entity.UpdatedAt = DateTimeOffset.Now;
                        }
                        break;
                }
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}