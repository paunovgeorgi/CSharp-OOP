using System;

namespace FightingArena.Tests
{
    using Microsoft.VisualBasic;
    using Newtonsoft.Json.Linq;
    using NUnit.Framework;
    using static System.Net.Mime.MediaTypeNames;

    [TestFixture]
    public class WarriorTests
    {


        [Test]

        public void WarriorConstructorIsWorking()
        {
            string expectedName = "Gosho";
            int expectedDamage = 32;
            int expectedHP = 100;

            Warrior warrior = new(expectedName, expectedDamage, expectedHP);

            Assert.AreEqual(expectedName, warrior.Name);
            Assert.AreEqual(expectedDamage, warrior.Damage);
            Assert.AreEqual(expectedHP, warrior.HP);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("      ")]

        public void WarriorNameShouldNotBeNullOrWhiteSpace(string name)
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(()
                => new Warrior(name, 12, 12));

            Assert.AreEqual("Name should not be empty or whitespace!", ex.Message);
        }

        [TestCase(0)]
        [TestCase(-1)]

        public void WarriorDamageShouldBePositive(int damage)
        {
            Assert.Throws<ArgumentException>(()
                => new Warrior("Leo", damage, 100), "Damage value should be positive!");
        }

        [Test]

        public void WarriorHPShouldNotBeNegative()
        {
            Assert.Throws<ArgumentException>(()
                => new Warrior("aaa", 20, -1), "Damage value should be positive!");
        }

        [Test]

        public void AttackMethodSHouldWorkCorrectly()
        {
            Warrior attacker = new("Rashi", 10, 100);
            Warrior defender = new("Shaw", 5, 90);

            int expectedAttackerHP = 95;
            int expectedDeffenderHP = 80;
            attacker.Attack(defender);

            Assert.AreEqual(expectedAttackerHP, attacker.HP);
            Assert.AreEqual(expectedDeffenderHP, defender.HP);
        }

        [TestCase(30)]
        [TestCase(25)]

        public void WarriorHPShouldBeMoreThanMinAttackHP(int hp)
        {
            Warrior attacker = new("Rashi", 10, hp);
            Warrior defender = new("Shaw", 5, 90);

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()
                => attacker.Attack(defender));

            Assert.AreEqual("Your HP is too low in order to attack other warriors!", ex.Message);
        }


        [TestCase(30)]
        [TestCase(25)]

        public void EnemyHPShouldBeMoreThanMinAttackHP(int hp)
        {
            Warrior attacker = new("Rashi", 10, 50);
            Warrior defender = new("Shaw", 5, hp);

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()
                => attacker.Attack(defender));

            Assert.AreEqual("Enemy HP must be greater than 30 in order to attack him!", ex.Message);
        }

        [TestCase(51)]
        [TestCase(200)]

        public void WarriorHPShouldBeMoreThanEnemyAttack(int attack)
        {
            Warrior attacker = new("Rashi", 10, 50);
            Warrior defender = new("Shaw", attack,60);

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()
                => attacker.Attack(defender));

            Assert.AreEqual("You are trying to attack too strong enemy", ex.Message);
        }

        [Test]

        public void WarriorDamageIsMoreThanEnemeyHP()
        {

            Warrior attacker = new("Rashi", 50, 100);
            Warrior enemy = new("Shaw", 5, 40);

            int expectedEnemyHP = 0;

            attacker.Attack(enemy);

            Assert.AreEqual(expectedEnemyHP, enemy.HP);
        }

        [Test]

        public void WarriorDamageIsLessThanEnemeyHP()
        {

            Warrior attacker = new("Rashi",30, 100);
            Warrior enemy = new("Shaw", 5, 40);

            int expectedEnemyHP = 10;

            attacker.Attack(enemy);

            Assert.AreEqual(expectedEnemyHP, enemy.HP);
        }
    }
}