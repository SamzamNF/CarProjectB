using CarProjektBeta;

namespace TestUnitBilProjektBeta
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestDriveMethodIfEngineOn()
        {
            // Arrange
            Car carTest = new Car("Toyota", "Corolla", 2020, 0, FuelType.Benzin, 18);
            carTest.ToggleEngine(true);

            Trip tripTest = new Trip(0, DateTime.Now, DateTime.Now, DateTime.Now.AddHours(1));
            tripTest.Distance = 100;

            // Act
            carTest.Drive(tripTest);

            // Assert
            Assert.AreEqual(100, carTest.Odometer);
            Console.WriteLine($"Du har kørt: {carTest.Odometer}");
        }

        [TestMethod]
        public void TestDriveMethodIfEngineOff()
        {
            // Arrange
            Car carTest = new Car("Toyota", "Corolla", 2020, 0, FuelType.Benzin, 18);
            carTest.ToggleEngine(false);

            Trip tripTest = new Trip(0, DateTime.Now, DateTime.Now, DateTime.Now.AddHours(1));
            tripTest.Distance = 100;

            // Act
            carTest.Drive(tripTest);

            // Assert
            Assert.AreEqual(0, carTest.Odometer);
            Console.WriteLine($"Du har kørt: {carTest.Odometer}");
        }
        [TestMethod]
        public void TestTurnEngineOnMethod()
        {
            // Arrange
            Car carTest = new Car("Toyota", "Corolla", 2020, 0, FuelType.Benzin, 18);

            // Act
            carTest.ToggleEngine(true);

            // Assert
            Assert.AreEqual(true, carTest.IsEngineOn);

        }
        [TestMethod]
        public void TestTurnEngineOffMethod()
        {
            // Arrange
            Car carTest = new Car("Toyota", "Corolla", 2020, 0, FuelType.Benzin, 18);

            // Act
            carTest.ToggleEngine(false);

            // Assert
            Assert.AreEqual(false, carTest.IsEngineOn);

        }
        [TestMethod]
        public void TestPriceDetails()
        {
            // Arrange
            Car carTest = new Car("Toyota", "Corolla", 2020, 0, FuelType.Benzin, 18);

            Trip tripTest = new Trip(0, DateTime.Now, DateTime.Now, DateTime.Now.AddHours(1));
            tripTest.Distance = 100;
            carTest.KmPerLiter = 18;
            carTest.FuelSource = FuelType.Benzin;
            
            // Act
            double priceCheck = tripTest.CalculateTripPrice(carTest);
            double expectedPrice = (100 / 18.0) * 13.49;

            // Assert
            Assert.AreEqual(expectedPrice, priceCheck);
            Console.WriteLine($"Metodetjek: {priceCheck:F2}");
            Console.WriteLine($"Manueltudregnet: {expectedPrice:F2}");
        }
        [TestMethod]
        public void TestCatchTripPrice()
        {
            // Arrange
            Car carTest = new Car("Toyota", "Corolla", 2020, 0, FuelType.Benzin, 0);

            Trip tripTest = new Trip(0, DateTime.Now, DateTime.Now, DateTime.Now.AddHours(1));
            tripTest.Distance = 10;

            // Act
            double expectedResult = tripTest.CalculateTripPrice(carTest);

            //Assert
            Assert.AreEqual(0, expectedResult);
        }

        [TestMethod]
        public void TestDriveMethodException()
        {
            // Arrange
            Car carTest = new Car("Toyota", "Corolla", 2020, 500, FuelType.Benzin, 18);
            carTest.ToggleEngine(true);

            Trip tripTest = new Trip(0, DateTime.Now, DateTime.Now, DateTime.Now.AddHours(1));
            tripTest.Distance = -250;

            // Act
            carTest.Drive(tripTest);

            // Assert
            Assert.AreEqual(500, carTest.Odometer);            
        }

    }
}
