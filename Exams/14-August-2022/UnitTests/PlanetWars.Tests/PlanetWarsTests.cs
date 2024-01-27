using NUnit.Framework;
using System;
using System.ComponentModel;
using System.Linq;

namespace PlanetWars.Tests
{
    public class Tests
    {
        public class PlanetWarsTests
        {
            private Weapon weapon;
            private Planet planet;

            [SetUp]

            public void Setup()
            {
                weapon = new Weapon("Pistol", 20, 5);
                planet = new Planet("Mars", 300);
            }

            [Test]

            public void Weapon_Constructor_WorksProperly()
            {
                Assert.NotNull(weapon);
                Assert.AreEqual("Pistol", weapon.Name);
                Assert.AreEqual(20, weapon.Price);
                Assert.AreEqual(5, weapon.DestructionLevel);
            }

            [Test]

            public void Weapon_PricePropertyThrowsException_WhenValueLessThanZero()
            {
                ArgumentException ex = Assert.Throws<ArgumentException>(()
                    => weapon = new Weapon("Pistol", -1, 20));

                Assert.AreEqual("Price cannot be negative.", ex.Message);
            }

            [Test]

            public void Weapon_IncreaseDestructionLevel_WorksProperly()
            {
                Assert.AreEqual(5, weapon.DestructionLevel);
                weapon.IncreaseDestructionLevel();
                Assert.AreEqual(6, weapon.DestructionLevel);
            }

            [Test]

            public void Weapon_IsNuclear_WorksProperly()
            {
                Assert.IsFalse(weapon.IsNuclear);
                weapon = new Weapon("AAA", 20, 30);
                Assert.IsTrue(weapon.IsNuclear);
            }

            [Test]

            public void Planet_Constructor_WorksProperly()
            {
                Assert.NotNull(planet);
                Assert.AreEqual("Mars", planet.Name);
                Assert.AreEqual(300, planet.Budget);
                Assert.NotNull(planet.Weapons);
                Assert.IsEmpty(planet.Weapons);
            }

            [TestCase("")]
            [TestCase(null)]

            public void Planet_NameSetter_WorksProperly(string name)
            {
                ArgumentException ex = Assert.Throws<ArgumentException>(()
                    => planet = new Planet(name, 300));

                Assert.AreEqual("Invalid planet Name", ex.Message);
            }


            [Test]

            public void Planet_BudgetThrowsException_WhenValueLessThanZero()
            {
                ArgumentException ex = Assert.Throws<ArgumentException>(()
                    => planet = new Planet("Mars", -1));

                Assert.AreEqual("Budget cannot drop below Zero!", ex.Message);
            }

            [Test]

            public void Planet_MilitaryPowerRatio_WorksProperly()
            {
                Assert.AreEqual(0, planet.MilitaryPowerRatio);
            }

            [Test]

            public void Planet_ProfitMethod_WorksProperly()
            {
                Assert.AreEqual(300, planet.Budget);
                planet.Profit(20);
                Assert.AreEqual(320, planet.Budget);
            }

            [Test]

            public void Planet_SpendMethod_WorksProperly()
            {
                Assert.AreEqual(300, planet.Budget);
                planet.SpendFunds(50);
                Assert.AreEqual(250, planet.Budget);
            }

            [Test]

            public void Plant_SpendMethod_ThrowsInvalidOperationException_WhenAmountMoreThanBudget()
            {
                InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()
                    => planet.SpendFunds(301));

                Assert.AreEqual("Not enough funds to finalize the deal.", ex.Message);
            }

            [Test]

            public void Planet_AddWeapon_WorksProperly()
            {
                Assert.IsEmpty(planet.Weapons);
                planet.AddWeapon(weapon);
                Assert.IsNotEmpty(planet.Weapons);
                Assert.AreEqual(1, planet.Weapons.Count);
            }

            [Test]

            public void Planet_AddWeapon_ThrowsInvalidOperationException_WhenWeaponWithSameNameAlreadyAdded()
            {
                Weapon weapon2 = new Weapon("Pistol", 4, 16);
                planet.AddWeapon(weapon);

                InvalidOperationException ex = Assert.Throws<InvalidOperationException>(
                    () => planet.AddWeapon(weapon2));

                Assert.AreEqual($"There is already a {weapon.Name} weapon.", ex.Message);
            }

            [Test]

            public void Planet_RemoveWeapon_WorksProperly()
            {
                Weapon weapon2 = new Weapon("MGK", 4, 16);
                planet.AddWeapon(weapon);
                planet.AddWeapon(weapon2);

                Assert.AreEqual(2, planet.Weapons.Count);
                Assert.NotNull(planet.Weapons.FirstOrDefault(w=>w.Name == weapon2.Name));
                planet.RemoveWeapon("MGK");
                Assert.AreEqual(1, planet.Weapons.Count);
                Assert.IsNull(planet.Weapons.FirstOrDefault(w => w.Name == weapon2.Name));

            }


            [Test]

            public void Planet_UpgradeWeapon_WorksProperly()
            {
                Weapon weapon2 = new Weapon("MGK", 4, 16);
                planet.AddWeapon(weapon);
                planet.AddWeapon(weapon2);

                Assert.AreEqual(16, weapon2.DestructionLevel);

                planet.UpgradeWeapon("MGK");

                Assert.AreEqual(17, weapon2.DestructionLevel);
            }

            [Test]

            public void Planet_UpgradeMethod_ThrowsException_WhenNameNotValid()
            {
                Weapon weapon2 = new Weapon("MGK", 4, 16);
                planet.AddWeapon(weapon);
                planet.AddWeapon(weapon2);

                InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()
                    => planet.UpgradeWeapon("Sniper"));

                Assert.AreEqual($"{"Sniper"} does not exist in the weapon repository of {planet.Name}", ex.Message);
            }

            [Test]

            public void Planet_DestructMethod_WorksProperly()
            {
                Weapon weapon2 = new Weapon("MGK", 4, 16);
                planet.AddWeapon(weapon);
                planet.AddWeapon(weapon2);
                Planet opponent = new Planet("Saturn", 100);

                Assert.AreEqual($"{opponent.Name} is destructed!", planet.DestructOpponent(opponent));

            }

            [Test]

            public void Planet_Destruct_ThrowsException_WhenOpponentTooStrong()
            {
                Weapon weapon2 = new Weapon("MGK", 4, 16);
                planet.AddWeapon(weapon);
                planet.AddWeapon(weapon2);
                Planet attacker = new Planet("Saturn", 100);

                InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()
                    => attacker.DestructOpponent(planet));

                Assert.AreEqual($"{planet.Name} is too strong to declare war to!", ex.Message);
            }
        }
    }
}
