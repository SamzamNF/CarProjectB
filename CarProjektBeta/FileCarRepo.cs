using CarProjektBeta;

public class FileCarRepository : ICarRepository
{
    private readonly string _filePath = "cars.txt";

    public FileCarRepository(string filePath)
    {
        _filePath = filePath;
        // Sikrer at filen eksisterer
        if (!File.Exists(_filePath))
        {
            File.Create(_filePath).Close();
        }
    }
    public Car GetByLicensePlate(string licensePlate)
    {
        var cars = GetAll();
        return cars.FirstOrDefault(c => c.LicensePlate == licensePlate);
    }

    
    public List<Car> GetAll()
    {
        var cars = new List<Car>();

        try
        {
            using (StreamReader sw = new StreamReader(_filePath))
            {
                string line;
                while ((line = sw.ReadLine()) != null)
                {
                    var car = Car.FromString(line);
                    if (car != null)
                    {
                        cars.Add(car);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved læsning af fil: {ex.Message}");
        }
        return cars;
    }

    public void Add(Car car)
    {
        try
        {
            using (StreamWriter sr = new StreamWriter(_filePath, append: true))
            {
                sr.WriteLine(car.ToString());
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved skrivning til fil: {ex.Message}");
        }
    }
    public void Update(Car car)
    {
        // Henter alle biler fra filen og gemmer dem i en liste
        var cars = GetAll();

        // Finder den eksisterende bil i listen baseret på nummerpladen
        var existingCar = cars.FirstOrDefault(c => c.LicensePlate == car.LicensePlate);

        // Hvis bilen findes, fjernes den gamle version og tilføjes den opdaterede version
        if (existingCar != null)
        {
            cars.Remove(existingCar); // Fjerner den gamle bil
            cars.Add(car);            // Tilføjer den opdaterede bil der sendes i parameteret
        }

        SaveAll(cars);
    }

    public void Delete(string licensePlate)
    {
        var cars = GetAll();
        var carToDelete = cars.FirstOrDefault(c => c.LicensePlate == licensePlate);

        if (carToDelete != null)
        {
            cars.Remove(carToDelete);
        }

        SaveAll(cars);
    }

    private void SaveAll(List<Car> cars)
    {
        try
        {
            using (StreamWriter sw = new StreamWriter(_filePath, append: false))
            {
                foreach (Car car in cars)
                {
                    sw.WriteLine(car.ToString());
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved skrivning til fil: {ex.Message}");
        }
    }
}
