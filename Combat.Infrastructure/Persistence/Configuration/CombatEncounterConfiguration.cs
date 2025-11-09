using Combat.Domain.Entities;
using Combat.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace Combat.Infrastructure.Persistence.Configuration
{
    public class CombatEncounterConfiguration : IEntityTypeConfiguration<CombatEncounter>
    {
        public void Configure(EntityTypeBuilder<CombatEncounter> builder)
        {
            builder.ToTable("Combats");

            builder.HasKey(c => c.Id);

            // Configure CombatId as Value Object
            builder.Property(c => c.Id)
                   .HasConversion(
                       id => id.Value,
                       value => CombatId.Create(value))
                   .HasColumnName("Id");

            // Configure Hero (combatant) as Owned Entity
            builder.OwnsOne(c => c.Hero, hero =>
            {
                hero.Property(h => h.EntityId).HasColumnName("HeroId");
                hero.Property(h => h.Name).HasColumnName("HeroName").HasMaxLength(50);
                hero.Property(h => h.Type)
                .HasConversion<string>()
                .HasColumnName("HeroType");
                hero.OwnsOne(h => h.Stats, stats =>
                {
                    stats.Property(s => s.Health).HasColumnName("HeroHealth");
                    stats.Property(s => s.Attack).HasColumnName("HeroAttack");
                    stats.Property(s => s.Defense).HasColumnName("HeroDefense");
                    stats.Property(s => s.Speed).HasColumnName("HeroSpeed");
                });
            });

            // Configure Enemy (combatant) as Owned Entity
            builder.OwnsOne(c => c.Enemy, enemy =>
            {
                enemy.Property(e => e.EntityId).HasColumnName("EnemyId");
                enemy.Property(e => e.Name).HasColumnName("EnemyName").HasMaxLength(50);
                enemy.Property(e => e.Type)
                .HasConversion<string>()
                .HasColumnName("EnemyType");
                enemy.OwnsOne(e => e.Stats, stats =>
                {
                    stats.Property(s => s.Health).HasColumnName("EnemyHealth");
                    stats.Property(s => s.Attack).HasColumnName("EnemyAttack");
                    stats.Property(s => s.Defense).HasColumnName("EnemyDefense");
                    stats.Property(s => s.Speed).HasColumnName("EnemySpeed");
                });
            });

            // Configure Status enum as string
            builder.Property(c => c.Status)
                   .HasConversion<string>()
                   .HasColumnName("Status");

            // Configure timestamps
            builder.Property(c => c.StartedAt)
                 .HasColumnName("StartedAt")
                .IsRequired();

            builder.Property(c => c.EndedAt)
                   .HasColumnName("EndedAt");

            // Configure Turns as Json
            builder.Property(c => c.Turns)
                   .HasConversion(
                       turns => JsonSerializer.Serialize(turns.Select(t => new
                       {
                           t.TurnNumber,
                           t.AttackerName,
                           t.DefenderName,
                           t.DamageDealt,
                           t.WasCritical
                       }),
                       (JsonSerializerOptions?)null),
                json => JsonSerializer.Deserialize<List<TurnData>>(json, (JsonSerializerOptions?)null)!
                    .Select(t => new CombatTurn(
                        t.TurnNumber,
                        t.AttackerName,
                        t.DefenderName,
                        t.DamageDealt,
                        t.WasCritical))
                    .ToList())
            .HasColumnName("Turns")
            .HasColumnType("jsonb");

            // Ignore domain events
            builder.Ignore(c => c.DomainEvents);

            // Indexes
            builder.HasIndex(c => c.StartedAt);
        }

        private class TurnData
        {
            public int TurnNumber { get; set; }
            public string AttackerName { get; set; } = string.Empty;
            public string DefenderName { get; set; } = string.Empty;
            public int DamageDealt { get; set; }
            public bool WasCritical { get; set; }
        }
    }
}