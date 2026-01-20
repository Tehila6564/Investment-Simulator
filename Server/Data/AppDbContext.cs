using Microsoft.EntityFrameworkCore;
using Server.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Server.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<DbActiveInvestment> ActiveInvestments => Set<DbActiveInvestment>();
    public DbSet<DbInvestmentOption> InvestmentOptions => Set<DbInvestmentOption>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<DbInvestmentOption>().HasData(
            new DbInvestmentOption
            {
                Id = "3",
                Name = "short-term investment",
                RequiredAmount = 10m,
                ExpectedReturn = 20m,
                DurationSeconds = 10
            },
            new DbInvestmentOption
            {
                Id = "2",
                Name = "mid-term investment",
                RequiredAmount = 100m,
                ExpectedReturn = 250m, 
                DurationSeconds = 30
            },
            new DbInvestmentOption
            {
                Id = "1",
                Name = "long-term investment",
                RequiredAmount = 1000m,
                ExpectedReturn = 3000m,
                DurationSeconds = 60
            }
        );
    }

}