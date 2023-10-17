using Microsoft.EntityFrameworkCore;

namespace fast_currencies_be;

public class FastCurrenciesContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<Request> Requests { get; set; }
    public DbSet<ConvertionHistory> ConvertionHistories { get; set; }
    public DbSet<CurrencyRate> CurrencyRates { get; set; }

    public FastCurrenciesContext(DbContextOptions<FastCurrenciesContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new RequestConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new CurrencyConfiguration());
        modelBuilder.ApplyConfiguration(new SubscriptionConfiguration());
        modelBuilder.ApplyConfiguration(new ConvertionHistoryConfiguration());
        modelBuilder.ApplyConfiguration(new CurrencyRateConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}
