using Heroes.Domain.Entities;
using Heroes.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Heroes.Infrastructure.Persistence.Configuration
{
    public class HeroConfiguration : IEntityTypeConfiguration<Hero>
    {
        public void Configure(EntityTypeBuilder<Hero> builder)
        {
            builder.ToTable("Heroes");

            builder.HasKey(h => h.Id);

            // Configure HeroId as Value Object
            builder.Property(h => h.Id)
                   .HasConversion(
                       id => id.Value,
                       value => HeroId.Create(value))
                   .HasColumnName("Id");

            // Configure HeroName as Value Object
            builder.Property(h => h.Name)
                   .HasConversion(
                       name => name.Value,
                       value => HeroName.Create(value))
                   .HasColumnName("Name")
                   .HasMaxLength(50)
                   .IsRequired();

            // Configure HeroClass as Value Object
            builder.Property(h => h.Class)
                   .HasConversion(
                       heroClass => heroClass.Name,
                       value => HeroClass.FromString(value))
                   .HasColumnName("Class")
                   .HasMaxLength(20)
                   .IsRequired();

            // Configure Level as Value Object
            builder.Property(h => h.Level)
                   .HasConversion(
                       level => level.Value,
                       value => Level.Create(value))
                   .HasColumnName("Level")
                   .IsRequired();

            // Configure Experience as Value Object
            builder.Property(h => h.Experience)
                   .HasConversion(
                       experience => experience.Value,
                       value => Experience.Create(value))
                   .HasColumnName("Experience")
                   .IsRequired();

            // Configure Stats as Owned Entity
            builder.OwnsOne(h => h.CurrentStats, stats =>
            {
                stats.Property(s => s.Health).HasColumnName("Health").IsRequired();
                stats.Property(s => s.Attack).HasColumnName("Attack").IsRequired();
                stats.Property(s => s.Defense).HasColumnName("Defense").IsRequired();
                stats.Property(s => s.Speed).HasColumnName("Speed").IsRequired();
            });

            builder.Property(h => h.CreatedAt)
                   .HasColumnName("CreatedAt")
                   .IsRequired();

            builder.Property(h => h.LastBattleAt)
                .HasColumnName("LastBattleAt");

            // Ignore domain events
            builder.Ignore(h => h.DomainEvents);

            // Indexes
            builder.HasIndex(h => h.Name);
            builder.HasIndex(h => h.Level);
        }
    }
}