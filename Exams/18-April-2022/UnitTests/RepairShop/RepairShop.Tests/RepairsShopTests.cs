using System;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace RepairShop.Tests
{
    public class Tests
    {
        public class RepairsShopTests
        {
            private Car car;
            private Garage garage;

            [SetUp]
            public void Setup()
            {
                car = new Car("Tesla", 0);
                garage = new Garage("Drift", 3);
            }

            [Test]

            public void Car_Constructor_WorksProperly()
            {
                Assert.NotNull(car);
                Assert.AreEqual("Tesla", car.CarModel);
                Assert.AreEqual(0, car.NumberOfIssues);
            }

            [Test]

            public void Car_IsFixed_WorksProperly()
            {
                Assert.IsTrue(car.IsFixed);
                car.NumberOfIssues = 1;
                Assert.IsFalse(car.IsFixed);
            }

            [Test]

            public void Garage_Constructor_WorksProperly()
            {
                Assert.NotNull(garage);
                Assert.AreEqual("Drift", garage.Name);
                Assert.AreEqual(3, garage.MechanicsAvailable);
                Assert.AreEqual(0, garage.CarsInGarage);
            }

            [TestCase("")]
            [TestCase(null)]

            public void Garage_NameProperty_ThrowsException_WhenNullOrEmpty(string name)
            {
                ArgumentNullException ex = Assert.Throws<ArgumentNullException>(()
                    => garage = new Garage(name, 3));

                Assert.AreEqual("Invalid garage name. (Parameter 'value')", ex.Message);
            }

            [TestCase(0)]
            [TestCase(-1)]

            public void Garage_MechanicsAvailable_ThrowsException_WhenZeroOrLess(int count)
            {
                ArgumentException ex = Assert.Throws<ArgumentException>(()
                    => garage = new Garage("aaa", count));

                Assert.AreEqual("At least one mechanic must work in the garage.", ex.Message);
            }

            [Test]

            public void Garage_AddCar_WorksProperly()
            {
                Assert.AreEqual(0, garage.CarsInGarage);
                garage.AddCar(car);
                Assert.AreEqual(1, garage.CarsInGarage);
            }

            [Test]

            public void Garage_AddCar_ThrowsException_WhenCarCountEqualsAvailableMechanics()
            {
                garage = new Garage("aaa", 1);
                garage.AddCar(car);

                InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()
                    => garage.AddCar(car));

                Assert.AreEqual("No mechanic available.", ex.Message);
            }


            [Test]

            public void Garage_FixCar_WorksProperly()
            {
                Car car2 = new Car("BMW", 1);
                garage.AddCar(car);
                garage.AddCar(car2);
                Assert.AreEqual(1, car2.NumberOfIssues);
                Assert.IsFalse(car2.IsFixed);
                garage.FixCar("BMW");
                Assert.IsTrue(car2.IsFixed);
                Assert.AreEqual(0, car2.NumberOfIssues);
            }

            [Test]

            public void Garage_FixCar_ThrowsException_WhenCarNotPresent()
            {
                garage.AddCar(car);

                InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()
                    => garage.FixCar("BMW"));

                Assert.AreEqual("The car BMW doesn't exist.", ex.Message);
            }

            [Test]

            public void Garage_RemoveFixedCar_WorksProperly()
            {
                Car car2 = new Car("BMW", 0);
                garage.AddCar(car);
                garage.AddCar(car2);
                Assert.AreEqual(2, garage.CarsInGarage);

                Assert.AreEqual(2,garage.RemoveFixedCar());
                Assert.AreEqual(0, garage.CarsInGarage);
            }

            [Test]

            public void Garage_RemoveFixedCars_ThrowsException_WhenGaragaIsEmpty()
            {
                car.NumberOfIssues = 1;
                garage.AddCar(car);
                InvalidOperationException ex = Assert.Throws<InvalidOperationException>(
                    () => garage.RemoveFixedCar());

                Assert.AreEqual("No fixed cars available.", ex.Message);
            }

            [Test]

            public void Garage_ReportMethod_WorksProperly()
            {
                Car car2 = new Car("BMW", 2);
                car.NumberOfIssues = 1;
                garage.AddCar(car);
                garage.AddCar(car2);

                string result = $"There are 2 which are not fixed: Tesla, BMW.";

                Assert.AreEqual(result, garage.Report());

            }
        }
    }
}