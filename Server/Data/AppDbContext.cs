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
    }
}