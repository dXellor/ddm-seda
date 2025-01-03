using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using seda_dll.Data.Configurations;
using seda_dll.Models;

namespace seda_dll.Data;

public class DataContext: DbContext
{
    private IConfiguration _configuration;
    
    public DbSet<User> Users { get; init; }

    public DataContext(DbContextOptions options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration["DefaultConnection:ConnectionString"]);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
    }
}