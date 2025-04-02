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
            string infoHeader = String.Format("| {0,-10} | {1,-19} | {2,-19} | {3,-12} | {4,-15} | {5,-10} |", "Distance", "Starttid", "Sluttid", "Varighed", "Brændstof", "Pris");
            string line = String.Format("|------------|---------------------|---------------------|--------------|-----------------|------------|");

            if (first)
            {
                Console.WriteLine(line);
                Console.WriteLine(infoHeader);
                Console.WriteLine(line);
            }

            string tripDetails = String.Format("| {0,-10} | {1,-19} | {2,-19} | {3,-12} | {4,-15:F2} | {5,-10:F2} | ", Distance, StartTime, EndTime, CalculateDuration().ToString(@"hh\:mm\:ss"), FuelConsumed(car), CalculateTripPrice(car));
            Console.WriteLine(tripDetails);
            Console.WriteLine(line);
        }

    }
    

}
