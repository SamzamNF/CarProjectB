namespace CarProjektBeta
{
    public interface ICarRepository
    {
        Car GetByLicensePlate(string licensePlate);
        List<Car> GetAll();
        void Add(Car car);
        void Update(Car car);
        void Delete(string licensePlate);
    }
    public interface ITripRepository
    {
        List<Trip> GetByLicensePlate(string licensePlate);
        List<Trip> GetAll();  // Returner en List i stedet for IEnumerable
        void Add(Trip trip);
        void Update(Trip trip);
        void Delete(string licensePlate);
    }

}