using System.Linq;

namespace SmartDevice.Tests
{
    using NUnit.Framework;
    using System;
    using System.Text;

    public class Tests
    {
        private Device device;


        [SetUp]
        public void Setup()
        {
            device = new Device(20);
        }

        [Test]
        public void DeviceConstructor_WorksProperly()
        {
            Assert.IsNotNull(device);
            Assert.AreEqual(20, device.MemoryCapacity);
            Assert.AreEqual(20, device.AvailableMemory);
            Assert.AreEqual(0, device.Photos);
            Assert.IsNotNull(device.Applications);
        }

        [Test]

        public void TakePhotoMethod_ReturnsTrue_WhenPhotoSizeIsSmallerThanAvailableMemory()
        {
            Assert.AreEqual(true, device.TakePhoto(1));
        }

        [Test]

        public void TakePhotoMethod_ReturnsFalse_WhenPhotoSizeIsLargerThanAvailableMemory()
        {
            Assert.AreEqual(false, device.TakePhoto(21));
        }

        [Test]

        public void TakePhotoMethod_DecreasesMemory_WhenWorkingProperly()
        {
            device.TakePhoto(2);
            Assert.AreEqual(18, device.AvailableMemory);
        }

        [Test]

        public void TakePhotoMethod_IncreasesPhotosAmount_WhenWorkintProperly()
        {
            device.TakePhoto(2);
            Assert.AreEqual(1, device.Photos);
        }

        [Test]

        public void InstallApp_WorksProperly_WhenAppSizeIsLessThanAvailableMemory()
        {
            device.InstallApp("Winamp", 19);
            Assert.AreEqual(1, device.AvailableMemory);
            Assert.AreEqual(1, device.Applications.Count);
            Assert.AreEqual("Winamp", device.Applications.Single());
        }

        [TestCase(19)]
        [TestCase(20)]

        public void InstallApp_ReturnsCorrectString_WhenWorkingProperly(int appSize)
        {
           string result = device.InstallApp("Winamp", appSize);

            Assert.AreEqual("Winamp is installed successfully. Run application?", result);
        }

        [Test]
        public void InstallApp_ThrowsException_WhenAppSizeIsLargerThanAvailableMemory()
        {
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()
                => device.InstallApp("Winamp", 21));

            Assert.AreEqual("Not enough available memory to install the app.", ex.Message);

        }

        [Test]

        public void FormatDeviceMethodIsWorkingProperly()
        {
            device.InstallApp("Skype", 10);

            device.FormatDevice();

            Assert.AreEqual(0, device.Photos);
            Assert.IsEmpty(device.Applications);
            Assert.AreEqual(device.MemoryCapacity, device.AvailableMemory);
        }

        [Test]

        public void GetDeviceStatusIsWorkingProperly()
        {
            device.InstallApp("Skype", 5);
            device.InstallApp("Discord", 5);
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Memory Capacity: 20 MB, Available Memory: 10 MB");
            sb.AppendLine("Photos Count: 0");
            sb.AppendLine("Applications Installed: Skype, Discord");

            string expected = sb.ToString().TrimEnd();

            Assert.AreEqual(expected, device.GetDeviceStatus());
        }
    }
}