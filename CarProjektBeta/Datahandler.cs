using System;
using System.Reflection.PortableExecutable;
namespace CarProjektBeta
{
    public class Datahandler
    {
        public string FilePath { get; set; }
        public Datahandler(string filepath)
        {
            FilePath = filepath;
        }

        //Metoder til Trip klassen
        public void AddCarsAndTrips(List<Car> cars)
        {
            using (StreamWriter sw = new StreamWriter(FilePath, false))
            {
                foreach (Car car in cars)
                {
                    sw.WriteLine(car.ToString());
                    foreach (Trip trip in car.Trips)
                    {
                        sw.WriteLine(trip.ToString());
                    }
                }
            }
        }
        public List<Car> LoadCarsAndTrips()
        {
            if (!File.Exists(FilePath)) return new List<Car>();

            List<Car> cars = new List<Car>();
            Car currentCar = null;

            using (StreamReader sr = new StreamReader(FilePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    // Tjek om linjen repræsenterer en bil (6 felter)
                    if (line.Split(';').Length == 6)
                    {
                        // Hvis der allerede er en currentCar, tilføj den til listen først
                        if (currentCar != null)
                        {
                            cars.Add(currentCar);
                        }
                        // Opret ny bil
                        currentCar = Car.FromString(line);
                    }
                    else
                    {
                        // Hvis det er en tur, og der er en currentCar, tilføj turen
                        Trip trip = Trip.FromString(line);
                        if (trip != null && currentCar != null)
                        {
                            currentCar.Trips.Add(trip);
                        }
                    }
                }
                // Tilføj den sidste bil, hvis der er en
                if (currentCar != null)
                {
                    cars.Add(currentCar);
                }
            }
            return cars;
        }

        public void DeleteCarByModel(string modelName)
        {
            try
            {
                List<Car> cars = LoadCarsAndTrips();

                var carsToKeep = cars.Where(car => car.Model != modelName).ToList();

                if (carsToKeep.Count == cars.Count)
                {
                    Console.WriteLine("Ingen bil den den model blev fundet");
                }
                else
                {
                    AddCarsAndTrips(carsToKeep);
                    Console.WriteLine($"Bilen med model {modelName} blev slettet fra databasen");
                }               
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Der opstod en fejl: {ex.Message}");
            }
        }
        
    }

}

