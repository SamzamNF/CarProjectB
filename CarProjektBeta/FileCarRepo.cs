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

        foreach (var line in File.ReadLines(_filePath))
        {
            var car = Car.FromString(line);
            if (car != null)
            {
                cars.Add(car);
            }
        }

        return cars;
    }

    public void Add(Car car)
    {
        var allCars = GetAll();
        allCars.Add(car);
        File.AppendAllText(_filePath, car.ToString() + Environment.NewLine);
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
        File.WriteAllLines(_filePath, cars.Select(c => c.ToString()));
    }
}
