using Common.Messaging.Outbox.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.Messaging.Outbox.EntityFrameworkCore
{
    public class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
    {
        public void Configure(EntityTypeBuilder<OutboxMessage> builder)
        {
            builder.ToTable("OutboxMessages");

            builder.HasKey(message => message.Id);

            builder.Property(message => message.OccurredAtUtc).IsRequired();

            builder.Property(message => message.Type).HasMaxLength(1000).IsRequired();

            builder.Property(message => message.Payload).IsRequired();

            builder.Property(message => message.AttemptCount).IsRequired();

            builder.Property(message => message.LastError).HasMaxLength(4000);

            builder.Property(message => message.NextAttemptAtUtc);

            builder.Property(message => message.DeadLetteredAtUtc);

            builder.HasIndex(message => new { message.PublishedAtUtc, message.DeadLetteredAtUtc, message.NextAttemptAtUtc });
        }
    }
}
