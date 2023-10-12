using Microsoft.EntityFrameworkCore;

namespace fast_currencies_be;

public class FastCurrenciesContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<Subscription> Subscription { get; set; }

    public FastCurrenciesContext(DbContextOptions<FastCurrenciesContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new CurrencyConfiguration());
        modelBuilder.ApplyConfiguration(new SubscriptionConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}
