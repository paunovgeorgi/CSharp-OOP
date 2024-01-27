using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;
using System.Text;
using NuGet.Frameworks;
using System.Diagnostics;
using System.Reflection;

namespace RobotFactory.Tests
{
    public class Tests
    {
        private Supplement supplement;
        private Robot robot;
        private Factory factory;

        [SetUp]
        public void Setup()
        {
            supplement = new Supplement("Name", 123);
            robot = new Robot("Model", 20, 123);
            factory = new Factory("Name", 20);
        }

        [Test]
        public void SupplementConstructorAndPropertiesWorkCorrectly()
        {
            Assert.IsNotNull(supplement);
            Assert.AreEqual("Name", supplement.Name);
            Assert.AreEqual(123, supplement.InterfaceStandard);
        }

        [Test]

        public void SupplementToStringMethodWorksCorrectly()
        {
            string expected = "Supplement: Name IS: 123";
            Assert.AreEqual(expected, supplement.ToString());
        }

        [Test]

        public void RobotConstructorAndPropertiesWorkCorrectly()
        {
            Assert.NotNull(robot);
            Assert.NotNull(robot.Supplements);
            Assert.AreEqual("Model", robot.Model);
            Assert.AreEqual(20, robot.Price);
            Assert.AreEqual(123, robot.InterfaceStandard);
        }

        [Test]

        public void RobotToStringMethodIsWorkingProperly()
        {
            string expected = "Robot model: Model IS: 123, Price: 20.00";
            Assert.AreEqual(expected, robot.ToString());
        }

        [Test]

        public void FactoryConstructorAndPropertiesWorkProperly()
        {
            Assert.IsNotNull(factory);
            Assert.AreEqual("Name", factory.Name);
            Assert.AreEqual(20, factory.Capacity);
            Assert.IsNotNull(factory.Robots);
            Assert.IsNotNull(factory.Supplements);
        }

        [Test]

        public void NameSetterWorksProperly()
        {
            string expected = "Harry";
            factory.Name = "Harry";

            Assert.AreEqual(expected, factory.Name);
        }

        [Test]

        public void CapacitySetterWorksProperly()
        {
            int expected = 16;
            factory.Capacity = 16;

            Assert.AreEqual(expected, factory.Capacity);
        }

        [Test]

        public void RobotsSetterWorksProperly()
        {

            List<Robot> robots = new List<Robot>() { robot };
            factory.Robots = robots;

            Assert.AreSame(robots, factory.Robots);

        }
        [Test]
        public void SupplementsSetterWorksProperly()
        {

            List<Supplement> supp = new List<Supplement>() { supplement };
            factory.Supplements = supp;

            Assert.AreSame(supp, factory.Supplements);

        }

        [Test]

        public void FactoryProduceRobotMethodIsWorkingProperly()
        {

            string expected = "Produced --> Robot model: Model IS: 123, Price: 20.00";
            Assert.AreEqual(expected,factory.ProduceRobot("Model", 20, 123));
            Assert.IsNotEmpty(factory.Robots);
            Assert.AreEqual(1, factory.Robots.Count);
            Assert.IsTrue(factory.Robots.Any(r => r.Model == "Model" && r.Price == 20.00 && r.InterfaceStandard == 123));
            //Assert.AreSame(robot, factory.Robots.FirstOrDefault(r=>r.Model == robot.Model));

        }

        [Test]

        public void FactoryProduceMethodDoesNotWork_WhenCapacityIsLessOrEqualToRobotsCount()
        {
            Robot robot2 = new Robot("Model2", 40, 456);
            factory = new Factory("Name", 2);
            factory.ProduceRobot("Model12", 40, 456);
            factory.ProduceRobot("Model1111", 40, 458);
            Assert.AreEqual("The factory is unable to produce more robots for this production day!", factory.ProduceRobot("Model3", 60, 789));
            Assert.AreEqual(2, factory.Robots.Count);
        }

        [Test]

        public void FactoryProduceSupplementMethotIsWorkingProperly()
        {
            Assert.AreEqual("Supplement: Name IS: 123", factory.ProduceSupplement("Name", 123));
            Assert.IsNotEmpty(factory.Supplements);
            Assert.AreEqual(1, factory.Supplements.Count);
        }

        [Test]


        public void FactoryUpgradeMethodIsWorkingProperly()
        {
            Assert.IsTrue(factory.UpgradeRobot(robot, supplement));

            Assert.AreEqual(1, robot.Supplements.Count);

            Assert.AreSame(supplement, robot.Supplements.Single());
        }


        [Test]

        public void FactoryUpgradeMethodReturnsFalse_WhenSupplementIsAlreadyAdded()
        {
           factory.UpgradeRobot(robot, supplement);
           Robot robot2 = new Robot("Model2", 40, 456);

            Assert.IsFalse(factory.UpgradeRobot(robot, supplement));
        }


        [Test]

        public void FactoryUpgradeMethodReturnsFalse_WhenRobotIFStandardIsDifferentToSupplements()
        {
            //factory.UpgradeRobot(robot, supplement);
            Robot robot2 = new Robot("Model2", 40, 456);

            Assert.IsFalse(factory.UpgradeRobot(robot2, supplement));
        }

        [Test]
        public void FactoryUpgradeReturnsFalseWhenBothParamsAreInvalid()
        {
            Robot robot2 = new Robot("Model2", 40, 456);
            factory.UpgradeRobot(robot, supplement);
            Assert.IsFalse(factory.UpgradeRobot(robot2, supplement));

        }

        [Test]

        public void FactorySellMethodIsWorkingProperly()
        {
            factory.ProduceRobot("CCC", 60.4, 789);
            factory.ProduceRobot("BBB", 40.2, 456);
            factory.ProduceRobot("AAA", 20.1, 123);
            Robot expected = factory.Robots.FirstOrDefault(r => r.Price == 60.4);

            Assert.AreSame(expected, factory.SellRobot(60.4));
            Assert.AreEqual(3, factory.Robots.Count);

        }


        [Test]
        public void SellRobot_ReturnsNull()
        {
            Factory factory = new Factory("Imperial", 1);
            Robot robot = new Robot("C3PO", 15.50, 10001);

            Assert.IsNull(factory.SellRobot(12.5));
        }
    }
}