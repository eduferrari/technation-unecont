using Microsoft.EntityFrameworkCore;
using UneContFinancial.Models;

namespace UneContFinancial.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }

    public DbSet<Nota> Notas { get; set; }
}
