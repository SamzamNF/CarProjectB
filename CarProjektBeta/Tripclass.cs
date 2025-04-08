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
                    Console.WriteLine("Indtast en distance der er mere end 0..");
                }
            }
        }
        public double CalculateTripPrice(Car car)
        {
            return (Distance / car.KmPerLiter) * car.FuelPrice;
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
            string infoHeader = String.Format("{0,-10} {1,-25} {2,-25} {3,-12} {4,-15} {5,-10}", "Distance", "Starttid", "Sluttid", "Varighed", "Brændstof", "Pris");
            string line = new string('-', 100);

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
                Console.ForegroundColor = ConsoleColor.Gray;
                            
            string tripDetails = String.Format("{0,-10} {1,-25} {2,-25} {3,-12} {4,-15:F2} {5,-10:F2}", Distance, StartTime, EndTime, CalculateDuration().ToString(@"hh\:mm\:ss"), FuelConsumed(car), CalculateTripPrice(car));
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
            DateTime tripDate = DateTime.ParseExact(parts[1], "dd/MM/yyyy", null);
            DateTime startTime = DateTime.ParseExact(parts[2], "HH:mm", null);
            DateTime endTime = DateTime.ParseExact(parts[3], "HH:mm", null);

            return new Trip(distance, tripDate, startTime, endTime);
        }

    }
    

}
