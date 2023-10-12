using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace fast_currencies_be;

public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.CurrentRequests)
            .IsRequired();
        builder.Property(s => s.MaxRequests)
            .IsRequired();
        builder.Property(s => s.UserId)
            .IsRequired();

        builder.HasOne(s => s.User)
            .WithOne()
            .HasForeignKey<Subscription>(s => s.UserId);
    }
}
