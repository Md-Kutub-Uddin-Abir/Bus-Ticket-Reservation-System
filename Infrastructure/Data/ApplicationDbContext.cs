using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Bus> Buses => Set<Bus>();
    public DbSet<BusSchedule> BusSchedules => Set<BusSchedule>();
    public DbSet<Ticket> Tickets => Set<Ticket>();

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(DateTime))
                {
                    property.SetValueConverter(new ValueConverter<DateTime, DateTime>(
                        v => v.Kind == DateTimeKind.Unspecified ? DateTime.SpecifyKind(v, DateTimeKind.Utc) : v.ToUniversalTime(),
                        v => DateTime.SpecifyKind(v, DateTimeKind.Utc)));
                }
                else if (property.ClrType == typeof(DateTime?))
                {
                    property.SetValueConverter(new ValueConverter<DateTime?, DateTime?>(
                        v => v.HasValue
                            ? (v.Value.Kind == DateTimeKind.Unspecified
                                ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc)
                                : v.Value.ToUniversalTime())
                            : v,
                        v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : v));
                }
            }
        }

        base.OnModelCreating(modelBuilder);
    }
}
