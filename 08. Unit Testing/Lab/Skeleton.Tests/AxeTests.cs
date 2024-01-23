using NUnit.Framework;
using System;

namespace Skeleton.Tests
{
    [TestFixture]
    public class AxeTests
    {
        [TestCase(20,20, 30,30)]
        public void AttackLosesDurabilityAfterHit(int health, int xp, int attack, int durability)
        {
            //Arrange
            Dummy dummy = new Dummy(health, xp);
            Axe axe = new Axe(attack, durability);

            axe.Attack(dummy);
            Assert.AreEqual(29, axe.DurabilityPoints);
        }

        [Test]

        public void Axe_Constructor_Works_Properly()
        {
            Axe axe = new Axe(10, 10);

            Assert.AreEqual(10, axe.AttackPoints);
            Assert.AreEqual(10, axe.DurabilityPoints);
        }

        [Test]

        public void AttackWithBrokenSword()
        {
            Dummy dummy = new Dummy(20, 20);
            Axe axe = new Axe(5, 0);

            Assert.Throws<InvalidOperationException>(()
                => axe.Attack(dummy));
        }
    }
}