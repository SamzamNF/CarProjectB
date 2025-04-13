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
        private string _licenseplate;

        // Konstruktør
        public Car(string brand, string model, int year, double odometer, FuelType fuelSource, double kmPerLiter, string licenseplate)
        {
            _brand = brand;
            _model = model;
            _year = year;
            Odometer = odometer;
            FuelSource = fuelSource;
            _kmPerLiter = kmPerLiter;
            _isEngineOn = false;
            _trips = new List<Trip>();
            _licenseplate = licenseplate;
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
                    throw new ArgumentException("Kilometerstand kan ikke være negativ");
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
                    throw new ArgumentException("Km/l skal være et positivt tal");
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
        public string LicensePlate
        {
            get { return _licenseplate; }
            set
            {
                if (value.Length == 7)
                {
                    _licenseplate = value;
                }
                else
                {
                    throw new ArgumentException("Nummerpladen skal have præcis 7 tegn");
                }
            }
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
                throw new ArgumentException("Motoren skal være tændt");
            }

        }      
        public void PrintCar(bool First = false)
        {
            string infoHeader = String.Format("{0,-13} {1,-13} {2,-13} {3,-10} {4,-13} {5,-13}", "Bilmærke", "Model", "Odometer", "Km/l", "Årgang", "Nummerplade");
            string line = new string('-', 81);

            if (First)
            {

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(infoHeader);
                Console.ResetColor();
                Console.WriteLine(line);
                
            }
            if (Odometer > 200000)
                Console.ForegroundColor = ConsoleColor.Red;
            else if (Odometer > 100000)
                Console.ForegroundColor = ConsoleColor.Yellow;
            else if (Odometer > 0 && Odometer <= 15000)
                Console.ForegroundColor = ConsoleColor.Green;
            else
                Console.ForegroundColor = ConsoleColor.White;

            string carDetails = string.Format("{0,-13} {1,-13} {2,-13} {3,-10} {4,-13} {5,-13}", Brand, Model, Odometer, KmPerLiter, Year, LicensePlate);
            Console.WriteLine(carDetails);

            Console.ResetColor();


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


        //Metoder til at gemme filer
        public override string ToString()
        {
            return $"{Brand};{Model};{Odometer};{_fuelSource};{KmPerLiter};{Year};{LicensePlate}";
        }
        public static Car FromString(string data)
        {
            string[] parts = data.Split(';');
            if (parts.Length < 7) return null;

            string brand = parts[0];
            string model = parts[1];
            double odometer = double.Parse(parts[2]);
            FuelType fuelSource = Enum.Parse<FuelType>(parts[3]);
            double kmPerLiter = double.Parse(parts[4]);
            int year = int.Parse(parts[5]);
            string licensePlate = parts[6];

            Car car = new Car(brand, model, year, odometer, fuelSource, kmPerLiter, licensePlate);

            return car;

        }
    }



}
