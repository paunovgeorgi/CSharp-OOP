using NUnit.Framework;
using System;

namespace Skeleton.Tests
{
    [TestFixture]
    public class DummyTests
    {
        private Dummy dummy;


        [SetUp]

        public void SetUp()
        {
            dummy = new(10, 10);
        }

        [Test]

        public void Dummy_Constructor_WorksAsIntended()
        {
            Assert.AreEqual(10, dummy.Health);
            dummy.TakeAttack(10);
            Assert.AreEqual(10, dummy.GiveExperience());
        }


        [Test]
        public void DummyLosesHealthWhenAttacked()
        {
            int expectedDummyHealth = 5;
            dummy.TakeAttack(5);

            Assert.AreEqual(expectedDummyHealth, dummy.Health);
        }

        [TestCase(1)]
        [TestCase(100)]

        public void ThrowExceptionWhenDummyHealthIsZeroOrBelow(int secondAttack)
        {
            dummy.TakeAttack(10);

           Assert.Throws<InvalidOperationException>(()
                => dummy.TakeAttack(secondAttack));

        }

        [Test]

        public void DummyCanGiveXpIfDead()
        {
            dummy.TakeAttack(10);

            int expectedXP = 10;

            Assert.AreEqual(expectedXP, dummy.GiveExperience());
        }

        [Test]

        public void DummyCantGiveXpIfAlive()
        {

            Assert.Throws<InvalidOperationException>(()
                => dummy.GiveExperience());
        }

        [Test]

        public void Dummy_IsDead_MethodWorksProperly()
        {
            dummy = new Dummy(0, 10);
            
            Assert.AreEqual(true, dummy.IsDead());
        }
    }
}