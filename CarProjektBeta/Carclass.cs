using System.Reflection;

namespace CarProjektBeta
{
    public class Car
    {
        // Private felter (attributter)
        private string _brand;
        private string _model;
        private int _year;
        private double _kilometer;
        private string _fuelSource;
        private double _kmPerLiter;
        private double _fuelPrice;
        private bool _isEngineOn;

        // Konstruktør
        public Car(string brand, string model, int year, double odometer, string fuelSource, double kmPerLiter)
        {
            _brand = brand;
            _model = model;
            _year = year;
            Odometer = odometer;
            this.FuelSource = fuelSource;
            KmPerLiter = kmPerLiter;
            _isEngineOn = false;
        }

        // Manuelt implementeret property

        public string Brand
        {
            get { return _brand; }
            set { _brand = value; }
        }
        public string Model
        {
            get { return _model; }
            set { _model = value; }
        }
        public int Year
        {
            get { return _year; }
            set { _year = value; }
        }
        public string FuelSource
        {
            get { return _fuelSource; }
            set
            {
                _fuelSource = value.ToLower();

                if (_fuelSource == "benzin")
                {
                    _fuelPrice = 13.49;
                }
                else if (_fuelSource == "diesel")
                {
                    _fuelPrice = 12.29;
                }
                else
                {
                    Console.WriteLine("Ugyldigt brændstof! Vælg mellem benzin eller diesel.");
                    _fuelPrice = 0;
                }
            }
        }

        public double FuelPrice
        {
            get { return _fuelPrice; }
        }
        public Double Odometer
        {
            get { return _kilometer; }
            set
            {
                if (value >= 0)
                {
                    _kilometer = value;
                }
                else
                {
                    Console.WriteLine("Kilometer kan ikke være negativt i din bil..");
                }
            }
        }
        public double KmPerLiter
        {
            get { return _kmPerLiter; }
            set
            {
                if (value > 0)
                {
                    _kmPerLiter = value;
                }
                else
                {
                    Console.WriteLine("kmPerLiter skal være positivt");
                }
            }
        }
        public bool IsEngineOn
        {
            get { return _isEngineOn; }
        }
        // Metoder

        public void ToggleEngine(bool value)
        {
            if (value)
            {
                Console.WriteLine("Motoren er tændt.");
            }
            else
            {
                Console.WriteLine("Motoren er slukket.");
            }
            _isEngineOn = value;
        }
        public void Drive(double distance)
        {
            if (_isEngineOn)
            {
                _kilometer += distance;
                Console.WriteLine($"Du har kørt {distance}km. Nyt kilometertal: {_kilometer}km");
            }
            else
            {
                Console.WriteLine("Du skal starte motoren først..");
            }
        }
        public double CalculateTripPrice(double distance)
        {
            return (distance / _kmPerLiter) * _fuelPrice;
        }
        public void PrintCar(bool First = false)
        {
            string infoHeader = String.Format("| {0,-13} | {1,-10} | {2,-10} | {3,-10} | {4,-10} |", "Bilmærke", "Model", "Odometer", "Km/l", "Årgang");
            string line = String.Format("|---------------|------------|------------|------------|------------|");

            if (First)
            {

                Console.WriteLine(line);
                Console.WriteLine(infoHeader);
                Console.WriteLine(line);
            }
            string carDetails = string.Format("| {0,-13} | {1,-10} | {2,-10} | {3,-10} | {4,-10} |", Brand, Model, Odometer, KmPerLiter, Year);
            Console.WriteLine(carDetails);
            Console.WriteLine(line);
        }
    }


}
