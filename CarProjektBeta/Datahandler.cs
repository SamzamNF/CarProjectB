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
            StreamWriter sw = null;
            try
            {
                using (sw = new StreamWriter(FilePath, false))
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
            catch (IOException ex)
            {
                Console.WriteLine($"Fejl under skrivning: {ex.Message}");
            }
            /* Denne del er ikke nødvendig, da "using" allerede lukker filen ordenligt.
            finally
            {
                sw?.Close();
            }*/
        }

        public List<Car> LoadCarsAndTrips()
        {
            List<Car> cars = new List<Car>();
            Car currentCar = null;

            try
            {
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
                
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Filen blev ikke fundet, opretter en tom liste..");
            }
            catch (FormatException)
            {
                Console.WriteLine("Der opstod en formatfejl i læsningen af filerne, sørg for at dataen er i den rigtige format");
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

