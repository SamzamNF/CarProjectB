using System;
using System.Collections.Generic;
using CarProjektBeta;

namespace BilProjektBeta
{
    internal class Program
    {
        static Datahandler datahandler = new Datahandler("CarsAndTrips.txt");
        static List<Car> cars = new List<Car>();

        static void Main(string[] args)
        {

            cars = datahandler.LoadCarsAndTrips();
            int choice;
            Car userCar = null;
            Trip newTrip = null;
            do
            {
                Console.WriteLine("\nVælg funktion: ");
                Console.WriteLine("1. Tilføj bil");
                Console.WriteLine("2. Slet bil");
                Console.WriteLine("3. Vælg bil");
                Console.WriteLine("4. Tænd eller sluk moteren");
                Console.WriteLine("5. Kør en tur");
                Console.WriteLine("6. Tjek om odometeret er et palindrom");
                Console.WriteLine("7. Print bilernes oplysninger");
                Console.WriteLine("8. Print tur pris");
                Console.WriteLine("9. Afslut programmet");
                
                Console.Write("Tast dit svar: ");


                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        userCar = CreateCar(datahandler);

                        MenuReturn();
                        break;
                    case 2:
                        Console.Clear();
                        if (cars.Count == 0)
                        {
                            Console.WriteLine("Der findes ingen biler i databasen");
                        }
                        else
                        {
                            Console.Clear();
                            CarList();

                            Console.Write("Vælg model du gerne vil slette: ");
                            string modelName = Console.ReadLine();
                            datahandler.DeleteCarByModel(modelName);

                            //Sætter cars listen = filens indhold.
                            cars = datahandler.LoadCarsAndTrips();
                        }
                        
                        MenuReturn();
                        break;
                    case 3:
                        Console.Clear();
                        CarList();
                        Console.Write("Vælg modelnavn for at bruge bilen: ");
                        string modelPick = Console.ReadLine();

                        userCar = ChooseCar(cars, modelPick);

                        MenuReturn();
                        break;
                    case 4:
                        Console.Clear();
                        if (userCar != null)
                        {
                            Console.Write("\nVil du tænde eller slukke moteren? ");
                            string engineChoice = Console.ReadLine().ToLower();
                            if (engineChoice == "tænd" || engineChoice == "tænde")
                            {
                                userCar.ToggleEngine(true);
                            }
                            else if (engineChoice == "sluk" || engineChoice == "slukke")
                            {
                                userCar.ToggleEngine(false);
                            }
                            else
                            {
                                Console.WriteLine("Ugyldt valg. Tænd eller sluk moteren med tænd / sluk");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Opret en bil i menu 1 først..");
                        }

                        MenuReturn();
                        break;                 
                    case 5:
                        //Tilføj tur til bil                        
                        Console.Clear();
                        if (userCar != null)
                        {
                            newTrip = CreateTrip();
                            userCar.Drive(newTrip);

                            Console.WriteLine($"Du har kørt {newTrip.Distance}km. Nyt kilometertal: {userCar.Odometer}km");

                            //Finder den aktive "userCar" der er valgt, og matcher den med den i listen.
                            for (int i = 0; i < cars.Count; i++)
                            {
                                if (cars[i].Model == userCar.Model)
                                {
                                    cars[i] = userCar;
                                    break;
                                }
                            }
                            //Tilføjer dataen til filen, så den passer til den rigtige bil
                            datahandler.AddCarsAndTrips(cars);
                        }
                        else
                        {
                            Console.WriteLine("Opret en bil i menu 1 først..");
                        }

                        MenuReturn();
                        break;
                    case 6:
                        Console.Clear();
                        if (userCar != null)
                        {
                            if (Palindrom(userCar.Odometer))
                            {
                                Console.WriteLine("\nOdometeret er et palindrom!");
                            }
                            else
                            {
                                Console.WriteLine("Odometeret er ikke et palindrom!");
                            }
                        }

                        MenuReturn();
                        break;
                    case 7:
                        Console.Clear();
                        if (cars.Count == 0)
                        {
                            Console.WriteLine("Der er ingen biler i databasen");
                        }
                        else
                        {
                            for (int i = 0; i < cars.Count; i++)
                            {
                                cars[i].PrintCar(i == 0);
                            }
                        }
                        MenuReturn();
                        break;
                    case 8:
                        
                        // userCar = objektet til min bil fra car klasse, .Trips er public propety til at get min private liste information så det kan printes. Count tæller listen.
                        
                        if (userCar != null)       
                        {
                            Console.Write("\nTast 1 for at se alle tures priser:");
                            Console.Write("\nTast 2 for at søge efter en tur: ");
                            int tripChoice = Convert.ToInt32(Console.ReadLine());
                            switch (tripChoice)
                            {
                                case 1:
                                    for (int i = 0; i < userCar.Trips.Count; i++)
                                    {
                                        userCar.Trips[i].PrintTripDetails(userCar, i == 0);
                                    }
                                    break;
                                case 2:
                                    Console.Write("Indtast datoen for at finde ture (dd-MM-yyyy): ");
                                    DateTime date = Convert.ToDateTime(Console.ReadLine());

                                    List<Trip> tripsByDate = userCar.GetTripsByDate(date);

                                    if (tripsByDate.Count == 0)
                                    {
                                        Console.WriteLine("Ingen ture blev fundet på denne dato");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"\nDer blev fundet {tripsByDate.Count} tur(e) på datoen.");

                                        for (int i = 0; i < tripsByDate.Count; i++)
                                        {
                                            tripsByDate[i].PrintTripDetails(userCar, i == 0);
                                        }
                                    }
                                    Console.WriteLine("\nTryk enter for at gå tilbage til menuen..");
                                    Console.ReadLine();
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Husk at oprette en bil og tur detaljer først..");
                        }
                        break;
                    case 9:
                        Console.WriteLine("Afslutter programmet...");
                        break;
                    default:
                        Console.WriteLine("Ugyldt valg, prøv igen");
                        break;
                }
            } while (choice != 8);

        }
        
        static Car CreateCar(Datahandler datahandler)
        {

            Car newCar = null;
            while (newCar == null)
            {
                try
                {
                    Console.WriteLine("\n***** OPRET NY BIL *****");
                    Console.Write("Indtast bilens mærke: ");
                    string brand = Console.ReadLine();
                    Console.Write("Indtast bilens model: ");
                    string model = Console.ReadLine();
                    Console.Write("Indtast bilens årgang: ");
                    int year = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Indtast bilens kilometer: ");
                    double odometer = Convert.ToDouble(Console.ReadLine());
                    Console.Write("Hvor mange km/l kører din bil? ");
                    double kmPerLiter = Convert.ToDouble(Console.ReadLine());
                    Console.Write("Indtast bilens brændstofstype: ");
                    string fuelInput = Console.ReadLine().ToLower();

                    FuelType fuelSource = FuelType.None;
                    switch (fuelInput)
                    {
                        case "diesel": fuelSource = FuelType.Diesel; break;
                        case "benzin": fuelSource = FuelType.Benzin; break;
                        case "el": fuelSource = FuelType.El; break;
                        case "hybrid": fuelSource = FuelType.Hybrid; break;
                        default: Console.WriteLine("Ugyldt valg, prøv igen!"); break;
                    }

                    newCar = new Car(brand, model, year, odometer, fuelSource, kmPerLiter);

                    cars.Add(newCar);
                    datahandler.AddCarsAndTrips(cars);
                    Console.WriteLine("Bilen blev oprettet og gemt i filen.");

                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Prøv igen");
                    Console.WriteLine();
                }
               
            }
            return newCar;
        }
        
        static Trip CreateTrip()
        {
            Console.Write("\nIndtast antal kilometer for turen: ");
            double distance = Convert.ToDouble(Console.ReadLine());

            Console.Write("Indtast dato for turen: (dd/mm/yyyy): ");
            DateTime tripDate = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);

            Console.Write("Indtast starttidspunkt (HH:mm): ");
            DateTime startTime = DateTime.ParseExact(Console.ReadLine(), "HH:mm", null);
            startTime = tripDate.Date + startTime.TimeOfDay; // Kombiner dato og tid

            Console.Write("Indtast sluttidspunkt (HH:mm): ");
            DateTime endTime = DateTime.ParseExact(Console.ReadLine(), "HH:mm", null);
            endTime = tripDate.Date + endTime.TimeOfDay; // Kombiner dato og tid

            return new Trip(distance, tripDate, startTime, endTime);
        }
        
        static bool Palindrom(double odometer)
        {
            string kilometerStr = odometer.ToString();
            string reverseStr = new string(kilometerStr.Reverse().ToArray());

            return kilometerStr == reverseStr;
        }
       static void CarList()
        {
            Console.WriteLine("\nFølgende biler findes i databasen");
            string infoHeader = string.Format("{0,-15} {1,-15} {2,-10}", "Mærke", "Model", "Årgang");
            Console.WriteLine(infoHeader);
            Console.WriteLine(new string('-', 45));

            foreach (Car car in cars)
            {
                string printInfo = string.Format("{0,-15} {1,-15} {2,-10}", car.Brand, car.Model, car.Year);
                Console.WriteLine(printInfo);
            }
        }
        static Car ChooseCar(List<Car> cars, string modelPick)
        {
            if (cars.Count == 0)
            {
                Console.WriteLine("Der er ingen biler i databasen");
                return null;
            }
            foreach (Car car in cars)
            {
                if (car.Model == modelPick)
                {
                    Console.WriteLine($"Du har valgt: {car.Brand} {car.Model}");
                    return car;
                }
            }

            Console.WriteLine("Ingen bil med den model blev fundet");
            return null;
        }
        static void MenuReturn()
        {
            Console.WriteLine("\nEnter for at se menuen..");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
