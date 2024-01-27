using NUnit.Framework;

namespace Robots.Tests
{
    using System;

    public class RobotsTests
    {
        private Robot robot;
        private RobotManager manager;

        [SetUp]
        public void Setup()
        {
            robot = new Robot("WALLE", 50);
            manager = new RobotManager(10);
        }

        [Test]

        public void Robot_Constructor_WorksProperly()
        {
            Assert.NotNull(robot);
            Assert.AreEqual("WALLE", robot.Name);
            Assert.AreEqual(50, robot.MaximumBattery);
            Assert.AreEqual(50, robot.Battery);
            Assert.AreEqual(robot.Battery, robot.MaximumBattery);
        }

        [Test]

        public void RobotManager_Constructor_WorksProperly()
        {
            Assert.NotNull(manager);
            Assert.AreEqual(10, manager.Capacity);
            Assert.AreEqual(0, manager.Count);
        }

        [Test]

        public void Capacity_ThrowsException_WhenBelowZero()
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(()
                => manager = new RobotManager(-1));

            Assert.AreEqual("Invalid capacity!", ex.Message);
        }

        [Test]

        public void AddRobot_WorksProperlu()
        {
            Assert.AreEqual(0, manager.Count);
            manager.Add(robot);
            Assert.AreEqual(1, manager.Count);
        }

        [Test]

        public void AddRobot_ThrowsException_WhenRobotAlreadyAdded()
        {
            Robot robot2 = new Robot("WALLE", 100);
            manager.Add(robot);

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()
                => manager.Add(robot2));

            Assert.AreEqual("There is already a robot with name WALLE!", ex.Message);
        }

        [Test]

        public void AddRobot_ThrowsException_WhenCapacityEqualstRobotCount()
        {
            manager = new RobotManager(1);
            Robot robot2 = new Robot("R2D2", 100);
            manager.Add(robot);

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()
                => manager.Add(robot2));

            Assert.AreEqual("Not enough capacity!", ex.Message);
        }


        [Test]


        public void RemoveRobot_WorksProperly()
        {
            Robot robot2 = new Robot("R2D2", 100);
            manager.Add(robot);
            manager.Add(robot2);

            Assert.AreEqual(2, manager.Count);
            manager.Remove("R2D2");
            Assert.AreEqual(1, manager.Count);
        }

        [Test]

        public void RemoveRobot_ThrowsException_WhenRobotNotFound()
        {
            Robot robot2 = new Robot("R2D2", 100);
            manager.Add(robot);
            manager.Add(robot2);
            Assert.AreEqual(2, manager.Count);

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(
                () => manager.Remove("Dante"));

            Assert.AreEqual("Robot with the name Dante doesn't exist!", ex.Message);
        }

        [Test]

        public void Work_WorksProperly()
        {
            Robot robot2 = new Robot("R2D2", 100);
            manager.Add(robot);
            manager.Add(robot2);

            Assert.AreEqual(100, robot2.Battery);
            manager.Work("R2D2", "Fix", 50);
            Assert.AreEqual(50, robot2.Battery);
        }

        [Test]

        public void Work_ThrowsException_WhenRobotNotFound()
        {
            Robot robot2 = new Robot("R2D2", 100);
            manager.Add(robot);
            manager.Add(robot2);
            Assert.AreEqual(2, manager.Count);

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(
                () => manager.Work("Dante", "Build", 30));

            Assert.AreEqual("Robot with the name Dante doesn't exist!", ex.Message);
        }

        [Test]

        public void Work_ThrowsException_WhenBatteryLeesThanUsage()
        {
            Robot robot2 = new Robot("R2D2", 100);
            manager.Add(robot);
            manager.Add(robot2);
            Assert.AreEqual(2, manager.Count);

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(
                () => manager.Work("R2D2", "Build", 101));

            Assert.AreEqual("R2D2 doesn't have enough battery!", ex.Message);
        }

        [Test]

        public void Charge_WorksProperly()
        {
            Robot robot2 = new Robot("R2D2", 100);
            manager.Add(robot);
            manager.Add(robot2);
            manager.Work("R2D2", "Fix", 50);
            Assert.AreEqual(50, robot2.Battery);
            manager.Charge("R2D2");
            Assert.AreEqual(100, robot2.Battery);
        }

        [Test]

        public void Charge_ThrowsException_WhenRobotNotFound()
        {
            Robot robot2 = new Robot("R2D2", 100);
            manager.Add(robot);
            manager.Add(robot2);
            Assert.AreEqual(2, manager.Count);

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(
                () => manager.Charge("Dante"));

            Assert.AreEqual("Robot with the name Dante doesn't exist!", ex.Message);
        }
    }
}
