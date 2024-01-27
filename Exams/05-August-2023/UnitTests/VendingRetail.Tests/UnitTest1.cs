using NUnit.Framework;

namespace VendingRetail.Tests
{
    public class Tests
    {
        private CoffeeMat coffeemat;
        [SetUp]
        public void Setup()
        {
            coffeemat = new CoffeeMat(20, 10);
        }

        [Test]
        public void CoffeeMatConstructorCreatestAnInstance()
        {
            Assert.IsNotNull(coffeemat);
        }

        [Test]

        public void CoffeeMatCreatesAllPropertiesCorrectly()
        {
            Assert.AreEqual(20,coffeemat.WaterCapacity);
            Assert.AreEqual(10, coffeemat.ButtonsCount);
            Assert.AreEqual(0, coffeemat.Income);
        }

        [Test]

        public void FillWaterTankMethod_WhenWaterTankIsEqualToWaterCapacity()
        {
            //coffeemat = new CoffeeMat(0, 10);
            coffeemat.FillWaterTank();
            Assert.AreEqual("Water tank is already full!", coffeemat.FillWaterTank());
        }

        [Test]

        public void FillWaterTankMethod_WhenWaterTankLevelIsLessThanWaterCapacity()
        {
            Assert.AreEqual("Water tank is filled with 20ml", coffeemat.FillWaterTank());
            Assert.AreEqual("Water tank is already full!", coffeemat.FillWaterTank());
        }

        [Test]

        public void AddDrinkMethodReturnsTrue()
        {
            Assert.AreEqual(true, coffeemat.AddDrink("Whiskey", 2));
        }

        [Test]

        public void AddDrinkMethod_ReturnsFalse_WhenDrinkAlreadyAdded()
        {
            coffeemat.AddDrink("Whiskey", 2);
            Assert.AreEqual(false, coffeemat.AddDrink("Whiskey", 2));
        }

        [Test]

        public void AddDrinkMethod_ReturnsFalse_WhenDrinksCountIsNotLessThanButtonsCount()
        {
            coffeemat = new CoffeeMat(20, 0);

            Assert.AreEqual(false, coffeemat.AddDrink("Vodka", 2));
        }

        [Test]

        public void BuyDrinkMethod_WhenWaterTankIsBelow_80()
        {
            Assert.AreEqual("CoffeeMat is out of water!", coffeemat.BuyDrink("Whiskey"));
        }

        [Test]

        public void BuyDrinkMehotd_WhenDrinkIsAvailable()
        {
            coffeemat = new CoffeeMat(100, 20);
            coffeemat.FillWaterTank();
            coffeemat.AddDrink("Whiskey", 5);

            Assert.AreEqual("Your bill is 5.00$", coffeemat.BuyDrink("Whiskey"));
            Assert.AreEqual(5, coffeemat.Income);
            Assert.AreEqual("CoffeeMat is out of water!", coffeemat.BuyDrink("Whiskey"));
        }

        [Test]

        public void BuyDrinkMehotd_WhenDrinkIsNotAvailable()
        {
            coffeemat = new CoffeeMat(100, 20);
            coffeemat.FillWaterTank();
            coffeemat.AddDrink("Whiskey", 5);

            Assert.AreEqual("Vodka is not available!", coffeemat.BuyDrink("Vodka"));
        }

        [Test]

        public void CollectIncomeMethod_ShouldWorkProperly()
        {
            coffeemat = new CoffeeMat(100, 20);
            coffeemat.FillWaterTank();
            coffeemat.AddDrink("Whiskey", 5);
            coffeemat.BuyDrink("Whiskey");

            Assert.AreEqual(5, coffeemat.CollectIncome());
            Assert.AreEqual(0, coffeemat.Income);
        }
    }
}