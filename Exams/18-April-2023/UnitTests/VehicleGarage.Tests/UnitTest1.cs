using NUnit.Framework;

namespace VehicleGarage.Tests
{
    public class Tests
    {
        private Vehicle vehicle;
        private Garage garage;

        [SetUp]
        public void Setup()
        {
            vehicle = new Vehicle("Lamborghini", "Aventador", "XX444XX");
            garage = new Garage(16);
        }

        [Test]
        public void Vehicle_Constructor_IsWorkingProperly()
        {
            Assert.NotNull(vehicle);
            Assert.AreEqual("Lamborghini", vehicle.Brand);
            Assert.AreEqual("Aventador", vehicle.Model);
            Assert.AreEqual("XX444XX", vehicle.LicensePlateNumber);
            Assert.AreEqual(100,vehicle.BatteryLevel);
            Assert.IsFalse(vehicle.IsDamaged);
        }

        [Test]

        public void Garage_Constructor_IsWorkingProperly()
        {
            Assert.NotNull(garage);
            Assert.NotNull(garage.Vehicles);
            Assert.AreEqual(16, garage.Capacity);
            Assert.AreEqual(0, garage.Vehicles.Count);
            Assert.IsEmpty(garage.Vehicles);
        }

        [Test]

        public void Garage_AddVehicle_WorksProperly()
        {
            Assert.IsTrue(garage.AddVehicle(vehicle));
            Assert.IsNotEmpty(garage.Vehicles);
            Assert.AreEqual(1, garage.Vehicles.Count);
        }

        [Test]

        public void Garage_AddVehicle_ReturnsFalse_WhenCapacityEqualToVehiclesCount()
        {
            Garage current = new Garage(1);
            current.AddVehicle(vehicle);
            Vehicle vehicle2 = new Vehicle("Lamborghini", "Aventador", "SS444XX");
            Assert.IsFalse(current.AddVehicle(vehicle2));
            Assert.AreEqual(1, current.Vehicles.Count);
        }

        [Test]

        public void Garage_AddVehicle_ReturnsFalse_WhenPlateNumberAlreadyAdded()
        {
            garage.AddVehicle(vehicle);
            Vehicle vehicle2 = new Vehicle("Lamborghini", "Gallardo", "XX444XX");
            Assert.IsFalse(garage.AddVehicle(vehicle2));
            Assert.AreEqual(1, garage.Vehicles.Count);
        }


        [Test]

        public void Garage_ChargeVehicle_WorksProperly()
        {
            Vehicle vehicle2 = new Vehicle("Porsche", "911", "SS444XX");
            garage.AddVehicle(vehicle);
            garage.AddVehicle(vehicle2);
            garage.ChargeVehicles(100);

            Assert.AreEqual(0, garage.ChargeVehicles(60));

            foreach (Vehicle gVehicle in garage.Vehicles)
            {
                Assert.AreEqual(100, gVehicle.BatteryLevel);
            }
        }

        [Test]

        public void Garage_DriveVehicle_IsWorkingProperly()
        {

            Vehicle vehicle2 = new Vehicle("Porsche", "911", "SS444XX");
            garage.AddVehicle(vehicle);
            garage.AddVehicle(vehicle2);

            garage.DriveVehicle("SS444XX", 50, true);

            Assert.AreEqual(50, vehicle2.BatteryLevel);
            Assert.IsTrue(vehicle2.IsDamaged);
        }

        [Test]

        public void Garage_DriveVehicle_StopsMethod_WhenVehicleIsDamaged()
        {
            Vehicle vehicle2 = new Vehicle("Porsche", "911", "SS444XX");
            vehicle2.IsDamaged = true;
            garage.AddVehicle(vehicle);
            garage.AddVehicle(vehicle2);
            garage.DriveVehicle("SS444XX", 50, true);

            Assert.AreEqual(100, vehicle2.BatteryLevel);
            Assert.IsTrue(vehicle2.IsDamaged);
        }

        [Test]
        public void Garage_DriveVehicle_StopsMethod_WhenBatteryDrainageIsMoreThan100()
        {
            Vehicle vehicle2 = new Vehicle("Porsche", "911", "SS444XX");
            garage.AddVehicle(vehicle);
            garage.AddVehicle(vehicle2);
            garage.DriveVehicle("SS444XX", 101, true);

            Assert.AreEqual(100, vehicle2.BatteryLevel);
            Assert.IsFalse(vehicle2.IsDamaged);
        }

        [Test]

        public void Garage_DriveVehicle_StopsMethod_WhenBatteryDrainageIsMoreThanBatteryLevel()
        {

            Vehicle vehicle2 = new Vehicle("Porsche", "911", "SS444XX");
            garage.AddVehicle(vehicle);
            garage.AddVehicle(vehicle2);

            garage.DriveVehicle("SS444XX", 50, false);
            garage.DriveVehicle("SS444XX", 60, false);


            Assert.AreEqual(50, vehicle2.BatteryLevel);
            Assert.IsFalse(vehicle2.IsDamaged);
        }

        [Test]

        public void Garage_RepairVehicles_WorksProperly()
        {
            Vehicle vehicle2 = new Vehicle("Porsche", "911", "SS444XX");
            garage.AddVehicle(vehicle);
            garage.AddVehicle(vehicle2);
            garage.DriveVehicle("SS444XX", 50, true);

            Assert.AreEqual($"Vehicles repaired: 1", garage.RepairVehicles());
            Assert.IsFalse(vehicle2.IsDamaged);
        }
    }
}