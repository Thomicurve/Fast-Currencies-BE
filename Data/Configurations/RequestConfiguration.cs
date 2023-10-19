using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace fast_currencies_be;

public class RequestConfiguration : IEntityTypeConfiguration<Request>
{
    public void Configure(EntityTypeBuilder<Request> builder)
    {
        builder.ToTable("Requests");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.CurrentRequests)
            .IsRequired();
        builder.Property(r => r.UserId)
            .IsRequired();
        builder.Property(r => r.FirstRequestFromMonthDate)
            .IsRequired(false);
        
        builder.HasOne<User>()
            .WithOne(u => u.Request)
            .HasForeignKey<Request>(r => r.UserId);
    }
}
