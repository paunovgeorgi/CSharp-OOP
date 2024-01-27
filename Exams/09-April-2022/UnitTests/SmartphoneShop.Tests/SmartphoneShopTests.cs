using System;
using NUnit.Framework;

namespace SmartphoneShop.Tests
{
    [TestFixture]
    public class SmartphoneShopTests
    {

        private Smartphone phone;
        private Shop shop;

        [SetUp]

        public void Setup()
        {
            phone = new Smartphone("Galaxy", 500);
            shop = new Shop(10);
        }

        [Test]

        public void Smartphone_Constructor_WorksProperly()
        {
            Assert.NotNull(phone);
            Assert.AreEqual("Galaxy", phone.ModelName);
            Assert.AreEqual(500, phone.MaximumBatteryCharge);
            Assert.AreEqual(500, phone.CurrentBateryCharge);
            Assert.AreEqual(phone.MaximumBatteryCharge, phone.CurrentBateryCharge);
        }

        [Test]


        public void Shop_Constructor_WorksProperly()
        {
            Assert.NotNull(shop);
            Assert.AreEqual(10, shop.Capacity);
            Assert.AreEqual(0, shop.Count);
        }

        [Test]

        public void Capacity_ThrowsException_WhenLessThanZero()
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(()
                => shop = new Shop(-1));

            Assert.AreEqual("Invalid capacity.", ex.Message);
        }

        [Test]

        public void AddMethod_WorksProperly()
        {
            Assert.AreEqual(0, shop.Count);
            shop.Add(phone);
            Assert.AreEqual(1, shop.Count);
        }

        [Test]

        public void AddMethod_ThrowsException_WhenModelExists()
        {
           Smartphone phone2 = new Smartphone("Galaxy", 400);
           shop.Add(phone);

           InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()
               => shop.Add(phone2));


           Assert.AreEqual($"The phone model {phone2.ModelName} already exist.", ex.Message);
        }

        [Test]

        public void AddMethod_ThrowsException_WhenCountEquasCapacity()
        {
            shop = new Shop(1);
            Smartphone phone2 = new Smartphone("Iphone", 400);
            shop.Add(phone);

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()
                => shop.Add(phone2));

            Assert.AreEqual("The shop is full.", ex.Message);
        }

        [Test]

        public void Remove_WorksProperly()
        {
            Smartphone phone2 = new Smartphone("Iphone", 400);
            shop.Add(phone);
            shop.Add(phone2);

            Assert.AreEqual(2, shop.Count);
            shop.Remove("Galaxy");

            Assert.AreEqual(1, shop.Count);
        }

        [Test]

        public void Remove_ThrowsException_WhenPhoneNotAdded()
        {
            Smartphone phone2 = new Smartphone("Iphone", 400);
            shop.Add(phone);
            shop.Add(phone2);

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()
                => shop.Remove("Pixel"));

            Assert.AreEqual("The phone model Pixel doesn't exist.", ex.Message);

        }

        [Test]

        public void TestPhone_WorksProperly()
        {
            shop.Add(phone);
            Assert.AreEqual(500, phone.CurrentBateryCharge);

            shop.TestPhone("Galaxy", 50);
            Assert.AreEqual(450, phone.CurrentBateryCharge);
        }

        [Test]

        public void TestPhone_ThrowsException_WhenPhoneNotAdded()
        {
            Smartphone phone2 = new Smartphone("Iphone", 400);
            shop.Add(phone);
            shop.Add(phone2);

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()
                => shop.TestPhone("Pixel", 100));

            Assert.AreEqual("The phone model Pixel doesn't exist.", ex.Message);

        }

        [Test]

        public void TestPhone_ThrowsException_WhenBatteryChargeLessThanUsage()
        {
            Smartphone phone2 = new Smartphone("Iphone", 400);
            shop.Add(phone);
            shop.Add(phone2);

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()
                => shop.TestPhone("Galaxy", 501));

            Assert.AreEqual("The phone model Galaxy is low on batery.", ex.Message);
        }

        [Test]

        public void Charge_WorksProperly()
        {
            shop.Add(phone);
            Assert.AreEqual(500, phone.CurrentBateryCharge);
            shop.TestPhone("Galaxy", 50);
            Assert.AreEqual(450, phone.CurrentBateryCharge);
            shop.ChargePhone("Galaxy");
            Assert.AreEqual(phone.CurrentBateryCharge, phone.MaximumBatteryCharge);
        }

        [Test]

        public void Charge_ThrowsException_WhenPhoneNotFound()
        {
            Smartphone phone2 = new Smartphone("Iphone", 400);
            shop.Add(phone);
            shop.Add(phone2);

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()
                => shop.ChargePhone("Pixel"));

            Assert.AreEqual("The phone model Pixel doesn't exist.", ex.Message);
        }
    }
}