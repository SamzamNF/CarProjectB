using System;
using CarProjektBeta;

namespace BilProjektBeta
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int choice;
            Car userCar = null;
            Trip newTrip = null;
            List<Trip> trips = new List<Trip>()   // Slet alt dette hvis du gemmer ture permanent samt .ToString i PrintTripDetails hvor den regner tiden ud. Også slet argument der tilføjer alle disse ture i case 7
            {
                new Trip(50, DateTime.Now.Date, DateTime.Now, DateTime.Now.AddHours(1)),
                new Trip(250, DateTime.Now.Date.AddDays(1), DateTime.Now.AddDays(1), DateTime.Now.AddDays(1).AddHours(2)),
                new Trip(450, DateTime.Now.Date.AddDays(3), DateTime.Now.AddDays(3), DateTime.Now.AddDays(3).AddHours(3)),
            };           

            do
            {
                Console.WriteLine("\nVælg funktion: ");
                Console.WriteLine("1. Indtast biloplysninger");
                Console.WriteLine("2. Tænd eller sluk moteren");
                Console.WriteLine("3. Kør en tur");
                Console.WriteLine("4. Tjek om odometeret er et palindrom");
                Console.WriteLine("5. Print bilernes oplysninger");
                Console.WriteLine("6. Print gruppens biler");
                Console.WriteLine("7. Print tur pris");
                Console.WriteLine("8. Afslut programmet");
                Console.Write("Tast dit svar: ");

                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        userCar = CreateCar();
                        break;
                    case 2:
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
                        break;                 
                    case 3:
                        if (userCar != null)
                        {
                            newTrip = CreateTrip();
                            userCar.Drive(newTrip);

                            Console.WriteLine($"Du har kørt {newTrip.Distance}km. Nyt kilometertal: {userCar.Odometer}km");
                        }
                        else
                        {
                            Console.WriteLine("Opret en bil i menu 1 først..");
                        }
                        break;
                    case 4:
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
                        break;
                    case 5:
                        if (userCar != null)
                            userCar.PrintCar(true);
                        break;
                    case 6:
                        PrintTeamCarTabel();
                        break;
                    case 7:
                        if (userCar != null)         // userCar = objektet til min bil fra car klasse, .Trips er public propety til at get min private liste information så det kan printes. Count tæller listen.
                        {
                            Console.Write("\nTast 1 for at se alle tures priser:");
                            Console.Write("\nTast 2 for at søge efter en tur: ");
                            int tripChoice = Convert.ToInt32(Console.ReadLine());
                            switch (tripChoice)
                            {
                                case 1:
                                    for (int i = 0; i < trips.Count; i++)      // slet denne for-loop når perm-trip data bliver implementeret
                                    {
                                        userCar.Drive(trips[i]);
                                    }

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
                    case 8:
                        Console.WriteLine("Afslutter programmet...");
                        break;
                    default:
                        Console.WriteLine("Ugyldt valg, prøv igen");
                        break;
                }
            } while (choice != 8);

        }
        
        static Car CreateCar()
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
        
        static void PrintTeamCarTabel()
        {
            Car[] teamCars = new Car[]
            {
                new Car("Audi", "A8", 2020, 15212, FuelType.Benzin, 14.3),
                new Car("BMW", "X5", 2021, 21000, FuelType.Diesel, 13.5),
                new Car("Honda", "Civic", 2016, 125020, FuelType.Benzin, 21.3),
                new Car("VW", "Golf", 2013, 95500, FuelType.Diesel, 17.4),
                new Car("Fiat", "500", 2014, 83121, FuelType.Benzin, 16.5)
            };


            for (int i = 0; i < teamCars.Length; i++)
            {
                teamCars[i].PrintCar(i == 0);
            }


        }
    }
}
