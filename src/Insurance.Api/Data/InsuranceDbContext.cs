using Insurance.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Insurance.Api.Data;
public class InsuranceDbContext : DbContext
{
    public InsuranceDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Surcharge> Surcharges { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Surcharge>().Property(e => e.Title).HasMaxLength(30).IsRequired();
        base.OnModelCreating(modelBuilder);
    }
}