using Combat.Domain.Common;
using Combat.Domain.Events;
using Combat.Domain.Exceptions;
using Combat.Domain.ValueObjects;

namespace Combat.Domain.Entities
{
    public sealed class CombatEncounter : Entity<CombatId>
    {
        private readonly List<CombatTurn> _turns = [];

        public Combatant Hero { get; private set; }
        public Combatant Enemy { get; private set; }
        public CombatStatus Status { get; private set; }
        public DateTime StartedAt { get; private set; }
        public DateTime? EndedAt { get; private set; }
        public IReadOnlyList<CombatTurn> Turns => _turns.AsReadOnly();

        public CombatEncounter(CombatId id, Combatant hero, Combatant enemy) : base(id)
        {
            Hero = hero;
            Enemy = enemy;
            Status = CombatStatus.InProgress;
            StartedAt = DateTime.UtcNow;
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        // Need for Entity Framework Core
        private CombatEncounter() : base()
        {
        }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        public static CombatEncounter Create(
            Guid heroId,
            string heroName,
            CombatantStats heroStats,
            Guid enemyId,
            string enemyName,
            CombatantStats enemyStats)
        {
            var combatId = CombatId.CreateUnique();
            var hero = new Combatant(heroId, heroName, CombatantType.Hero, heroStats);
            var enemy = new Combatant(enemyId, enemyName, CombatantType.Enemy, enemyStats);
            var combat = new CombatEncounter(combatId, hero, enemy);
            combat.AddDomainEvent(new CombatInitiatedEvent(combatId.Value, heroId, enemyId));

            return combat;
        }

        public void ExecuteCombat()
        {
            if (Status != CombatStatus.InProgress)
                throw new DomainException("Combat has already ended.");

            var turnNumber = 1;
            var random = new Random();

            var heroGoesFirst = Hero.Stats.Speed >= Enemy.Stats.Speed;

            while (Hero.Stats.IsAlive() && Enemy.Stats.IsAlive())
            {
                if (heroGoesFirst)
                {
                    ExecuteTurn(Hero, Enemy, turnNumber++, random);
                    if (!Enemy.Stats.IsAlive()) break;

                    ExecuteTurn(Enemy, Hero, turnNumber++, random);
                }
                else
                {
                    ExecuteTurn(Enemy, Hero, turnNumber++, random);
                    if (!Hero.Stats.IsAlive()) break;

                    ExecuteTurn(Hero, Enemy, turnNumber++, random);
                }
            }

            EndCombat();
        }

        private void ExecuteTurn(Combatant attacker, Combatant defender, int turnNumber, Random random)
        {
            var baseDamage = attacker.Stats.CalculateDamage(defender.Stats);

            var isCritical = random.Next(100) < 15; // 15% chance for critical hit
            var damage = isCritical ? baseDamage * 2 : baseDamage;

            var turn = new CombatTurn(turnNumber, attacker.Name, defender.Name, damage, isCritical);
            _turns.Add(turn);

            var newStats = defender.Stats.TakeDamage(damage);

            if (defender.Type == CombatantType.Hero)
                Hero = Hero.WithStats(newStats);
            else
                Enemy = Enemy.WithStats(newStats);
        }

        private void EndCombat()
        {
            EndedAt = DateTime.UtcNow;
            Status = Hero.Stats.IsAlive() ? CombatStatus.HeroWon : CombatStatus.EnemyWon;

            var winner = Hero.Stats.IsAlive() ? Hero : Enemy;
            var experienceGained = CalculateExperience();

            AddDomainEvent(new CombatCompletedEvent(
                Id.Value,
                Hero.EntityId,
                Enemy.EntityId,
                Status,
                winner.EntityId,
                experienceGained,
                _turns.Count));
        }

        private int CalculateExperience()
        {
            return Status == CombatStatus.HeroWon ? 50 + (_turns.Count * 10) : 0;
        }
    }
}