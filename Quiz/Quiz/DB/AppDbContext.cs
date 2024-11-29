using Microsoft.EntityFrameworkCore;
using Quiz.Connections;
using Quiz.Entity;
using Quiz.InferaStructure.Configurations;

namespace Quiz.DB;


public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer(ConnectionsString.Connection1);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new TransacrionConfiguration());
        modelBuilder.ApplyConfiguration(new CardConfiguration());
    }
    public DbSet<Card> Cards { get; set; }
    public DbSet<TransAction> Transactions { get; set; }
}
