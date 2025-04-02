using System.Reflection;

namespace CarProjektBeta
{

    public enum FuelType
    {
        None,
        Diesel,
        Benzin,
        El,
        Hybrid
    }
    public class Car
    {
        // Private felter (attributter)
        private string _brand;
        private string _model;
        private int _year;
        private double _kilometer;
        private FuelType _fuelSource;
        private double _kmPerLiter;
        private double _fuelPrice;
        private bool _isEngineOn;
        private List<Trip> _trips;

        // Konstruktør
        public Car(string brand, string model, int year, double odometer, FuelType fuelSource, double kmPerLiter)
        {
            _brand = brand;
            _model = model;
            _year = year;
            Odometer = odometer;
            FuelSource = fuelSource;
            _kmPerLiter = kmPerLiter;
            _isEngineOn = false;
            _trips = new List<Trip>();           
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
        public FuelType FuelSource
        {
            get { return _fuelSource; }
            set
            {
                _fuelSource = value;

                switch (_fuelSource)
                {
                    case FuelType.Benzin:
                        _fuelPrice = 13.49;
                        break;
                    case FuelType.Diesel:
                        _fuelPrice = 12.29;
                        break;
                    case FuelType.El:
                        _fuelPrice = 8;
                        break;
                    case FuelType.Hybrid:
                        _fuelPrice = 9.5;
                        break;
                    default:
                        throw new ArgumentException("Ugyldigt brændstof! Vælg mellem benzin, diesel, el eller hybrid.");
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
        public List<Trip> Trips
        {
            get { return _trips; }
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
        public void Drive(Trip newTrip)
        {
            if (_isEngineOn)
            {
                _kilometer += newTrip.Distance;
                _trips.Add(newTrip);
            }
            else
            {
                Console.WriteLine("Du skal starte motoren først..");
            }
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
        public List<Trip> GetTripsByDate(DateTime date)
        {
            List<Trip> tripsByDate = new();

            for (int i = 0; i < Trips.Count; i++)
            {
                if (Trips[i].TripDate == date)
                {
                    tripsByDate.Add(Trips[i]);
                }
            }
            return tripsByDate;
        }
    }


}
