using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework.Constraints;

namespace FightingArena.Tests
{
    using NUnit.Framework;
    using System.Xml.Linq;

    [TestFixture]
    public class ArenaTests
    {
        private Arena arena;

        [SetUp]

        public void SetUp()
        {
            arena = new Arena();
        }


        [Test]

        public void ArenaConstructorIsWorkingProperly()
        {
            
           Assert.IsNotNull(arena);
           Assert.IsNotNull(arena.Warriors);
        }

        [Test]

        public void WarriorsCountPropertyIsCorrect()
        {
            int expectedResult = 1;
            Warrior current = new("Achilles", 32, 64);
            arena.Enroll(current);
            Assert.AreEqual(expectedResult, arena.Count);
        }

        [Test]

        public void ArenaEnrollShouldWorkProperly()
        {
            Warrior currentWarrior = new("Achilles", 32, 64);
            arena.Enroll(currentWarrior);
            
            Assert.IsNotEmpty(arena.Warriors);
            Assert.AreEqual(currentWarrior, arena.Warriors.Single());

        }

        [Test]

        public void ArenaEnrollShouldThrowExceptionIfWarriorIsAlreadyEnrolled()
        {
            Warrior currentWarrior = new("Achilles", 32, 64);
            arena.Enroll(currentWarrior);

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()
                =>arena.Enroll(currentWarrior));

            Assert.AreEqual("Warrior is already enrolled for the fights!", ex.Message);

        }

        [Test]

        public void ArenaFightMethodShouldWorkProperly()
        {
            Warrior attacker = new("Rashi", 10, 50);
            Warrior defender = new("Shaw", 5, 100);

            arena.Enroll(attacker);
            arena.Enroll(defender);

            arena.Fight(attacker.Name, defender.Name);

            int expectedAttackerHP = 45;
            int expectedDefenderHP = 90;

            Assert.AreEqual(expectedAttackerHP, attacker.HP);
            Assert.AreEqual(expectedDefenderHP, defender.HP);
        }

        [Test]

        public void ArenaFightShouldThrowExceptionIfAttackerNotFound()
        {
            Warrior attacker = new("Rashi", 10, 50);
            Warrior defender = new("Shaw", 5, 100);

            arena.Enroll(defender);


            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()
                => arena.Fight(attacker.Name, defender.Name));

            Assert.AreEqual($"There is no fighter with name {attacker.Name} enrolled for the fights!", ex.Message);

        }

        [Test]

        public void ArenaFightShouldThrowExceptionIfDefenderNotFound()
        {
            Warrior attacker = new("Rashi", 10, 50);
            Warrior defender = new("Shaw", 5, 100);

            arena.Enroll(attacker);


            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()
                => arena.Fight(attacker.Name, defender.Name));

            Assert.AreEqual($"There is no fighter with name {defender.Name} enrolled for the fights!", ex.Message);

        }
    }
}
