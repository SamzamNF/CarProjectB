using CarProjektBeta;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class FileTripRepository : ITripRepository
{
    private readonly string _filePath = "trips.txt";

    public FileTripRepository(string filePath)
    {
        _filePath = filePath;
        // Sikrer at filen eksisterer
        if (!File.Exists(_filePath))
        {
            File.Create(_filePath).Close();
        }
    }

    public List<Trip> GetByLicensePlate(string licensePlate)
    {
        var trips = GetAll();
        return trips.Where(t => t.LicensePlate == licensePlate).ToList();
    }

    public List<Trip> GetAll()
    {
        var trips = new List<Trip>();

        try
        {
            using (var sr = new StreamReader(_filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var trip = Trip.FromString(line);
                    if (trip != null)
                    {
                        trips.Add(trip);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved læsning af fil: {ex.Message}");
        }

        return trips;
    }
    public void Add(Trip trip)
    {
        try
        {
            using (var sw = new StreamWriter(_filePath, append: true))
            {
                sw.WriteLine(trip.ToString());
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved skrivning til fil: {ex.Message}");
        }
    }
    public void Update(Trip trip)
    {
        var trips = GetAll();
        var existingTrip = trips.FirstOrDefault(t => t.LicensePlate == trip.LicensePlate && t.TripDate == trip.TripDate);

        if (existingTrip != null)
        {
            trips.Remove(existingTrip);
            trips.Add(trip);
            SaveAll(trips);
        }
        else
        {
            Console.WriteLine("Trip not found for update.");
        }
    }

    public void Delete(string licensePlate)
    {
        var trips = GetAll();
        //Bemærk, den sletter kun én trip. Brug Where i stedet for hvis alle med denne licensePlate skal slettes på én gang
        var tripToDelete = trips.FirstOrDefault(t => t.LicensePlate == licensePlate);

        if (tripToDelete != null)
        {
            trips.Remove(tripToDelete);
            SaveAll(trips);
        }
        else
        {
            Console.WriteLine("Tur ikke fundet.");
        }
    }

    private void SaveAll(List<Trip> trips)
    {
        try
        {
            using (var sw = new StreamWriter(_filePath, append: false))
            {
                foreach (var trip in trips)
                {
                    sw.WriteLine(trip.ToString());
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved skrivning til fil: {ex.Message}");
        }
    }
}
