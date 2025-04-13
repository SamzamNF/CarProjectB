using System;

namespace CarProjektBeta
{
    public class Trip
    {
        private double _distance;
        private DateTime _tripDate;
        private DateTime _startTime;
        private DateTime _endTime;

        public Trip(double distance, DateTime tripDate, DateTime startTime, DateTime endTime)
        {
            _distance = distance;
            _tripDate = tripDate;
            _startTime = startTime;
            _endTime = endTime;
        }
        public DateTime TripDate
        {
            get { return _tripDate; }
            set { _tripDate = value; }
        }
        public DateTime StartTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }
        public DateTime EndTime
        {
            get { return _endTime; }
            set { _endTime = value; }
        }
        public double Distance
        {
            get { return _distance; }
            set
            {
                if (value >= 0)
                {
                    _distance = value;
                }
                else
                {
                    throw new InvalidDistanceException("Distance skal være størrere end 0..");
                }
            }
        }
        public double CalculateTripPrice(Car car)
        {
            try
            {
                if (car.KmPerLiter == 0)
                {
                    throw new DivideByZeroException();
                }

                return (Distance / car.KmPerLiter) * car.FuelPrice;
            }
            catch (DivideByZeroException DBZE)
            {
                Console.WriteLine("Km/l kan ikke være 0, og prisen af din tur kan derfor ikke udregnes..");
                return 0;
            }
        }
        public TimeSpan CalculateDuration()
        {
            return _endTime - _startTime;
        }
        public double FuelConsumed(Car car)
        {
            return Distance / car.KmPerLiter;
        }


        public void PrintTripDetails(Car car, bool first = false)
        {
            string infoHeader = String.Format("{0,-13} {1,-10} {2,-25} {3,-25} {4,-12} {5,-15} {6,-10}", "Nummerplade", "Distance", "Starttid", "Sluttid", "Varighed", "Brændstof", "Pris");
            string line = new string('-', 120);

            if (first)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(infoHeader);
                Console.ResetColor();
                Console.WriteLine(line);

            }

            if (CalculateDuration() > TimeSpan.FromHours(7))
                Console.ForegroundColor = ConsoleColor.Red;
            else if (CalculateDuration() > TimeSpan.FromHours(5))
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            else if (CalculateDuration() > TimeSpan.FromHours(3))
                Console.ForegroundColor = ConsoleColor.Yellow;
            else
                Console.ForegroundColor = ConsoleColor.White;
                            
            string tripDetails = String.Format("{0,-13} {1,-10} {2,-25} {3,-25} {4,-12} {5,-15:F2} {6,-10:F2}", car.LicensePlate, Distance, StartTime, EndTime, CalculateDuration().ToString(@"hh\:mm\:ss"), FuelConsumed(car), CalculateTripPrice(car));
            Console.WriteLine(tripDetails);

            Console.ResetColor();
        }

        //Metoder til filer
        public override string ToString()
        {
            return $"{Distance};{TripDate:dd/MM/yyyy};{StartTime:HH:mm};{EndTime:HH:mm}";
        }
        public static Trip FromString(string data)
        {
            string[] parts = data.Split(';');
            if (parts.Length != 4) return null;

            double distance = double.Parse(parts[0]);
            
            DateTime tripDate = DateTime.ParseExact(parts[1], "dd-MM-yyyy", null);

            // Kombiner datoen fra tripDate med tidspunkterne
            DateTime startTime = DateTime.ParseExact(parts[2], "HH:mm", null);
            startTime = tripDate.Date + startTime.TimeOfDay; // Kombiner dato og tid

            DateTime endTime = DateTime.ParseExact(parts[3], "HH:mm", null);
            endTime = tripDate.Date + endTime.TimeOfDay; // Kombiner dato og tid


            return new Trip(distance, tripDate, startTime, endTime);
        }

    }
    

}
