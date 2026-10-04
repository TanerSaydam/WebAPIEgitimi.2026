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
        modelBuilder.Entity<Muayene>(entity =>
        {
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
}