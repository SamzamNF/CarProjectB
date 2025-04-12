using CarProjektBeta;

namespace BilProjektBeta
{
    internal class Program
    {
        static Datahandler datahandler = new Datahandler("CarsAndTrips.txt");
        static List<Car> cars = new List<Car>();
        static Car userCar = null;

        static void Main(string[] args)
        {
            cars = datahandler.LoadCarsAndTrips();
            ShowMainMenu();
        }

        static void ShowMainMenu()
        {
            ConsoleKeyInfo choice;
            do
            {
                Console.Clear();
                PrintMainMenu();
                choice = Console.ReadKey(true);
                HandleMenuChoice(choice.KeyChar);
            } while (choice.KeyChar != '6');
        }

        static void PrintMainMenu()
        {
            Console.Clear();
            PrintColoredMenu();

            PrintColoredOption(1, "Adminstrer bil");
            PrintColoredOption(2, "Kør en tur");
            PrintColoredOption(3, "Tjek om odometeret er et palindrom");
            PrintColoredOption(4, "Print bilernes oplysninger");
            PrintColoredOption(5, "Print tur pris");
            PrintColoredOption(6, "Afslut programmet");
        }

        static void HandleMenuChoice(char choice)
        {
            switch (choice)
            {
                case '1': CarOptions(); break;
                case '2': DriveCar(); break;
                case '3': CheckOdometerPalindrome(); break;
                case '4': PrintCarDetails(); break;
                case '5': PrintTripDetails(); break;
                case '6': Console.WriteLine("Afslutter programmet..."); break;
                default: Console.WriteLine("Ugyldigt valg, prøv igen"); break;
            }
        }

        static void CarOptions()
        {
            Console.Clear();
            PrintColoredMenu();

            PrintColoredOption(1, "Tilføj bil");
            PrintColoredOption(2, "Slet bil");
            PrintColoredOption(3, "Vælg bil");
            PrintColoredOption(4, "Tænd eller sluk motoren");
            PrintColoredOption(5, "Gå tilbage");
            
            ConsoleKeyInfo tripChoice;
            tripChoice = Console.ReadKey();
            switch (tripChoice.KeyChar)
            {
                case '1': AddCar(); break;
                case '2': DeleteCar(); break;
                case '3': SelectCar(); break;
                case '4': Engine(); break;
                default: break;
            }
        }

        static void AddCar()
        {
            Console.Clear();
            userCar = CreateCar(datahandler);
            MenuReturn();
        }

        static void DeleteCar()
        {
            Console.Clear();
            if (cars.Count == 0)
            {
                Console.WriteLine("Der findes ingen biler i databasen");
            }
            else
            {
                CarList();
                Console.Write("Vælg model du gerne vil slette: ");
                string modelName = Console.ReadLine();
                datahandler.DeleteCarByModel(modelName);
                cars = datahandler.LoadCarsAndTrips(); // Opdaterer listen af biler fra filen
            }
            MenuReturn();
        }

        static void SelectCar()
        {
            Console.Clear();
            CarList();
            Console.Write("Vælg modelnavn for at bruge bilen: ");
            string modelPick = Console.ReadLine();
            userCar = ChooseCar(cars, modelPick);
            MenuReturn();
        }

        static void Engine()
        {
            Console.Clear();
            if (userCar != null)
            {
                Console.Write("\nVil du tænde eller slukke motoren? ");
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
                    Console.WriteLine("Ugyldigt valg. Tænd eller sluk moteren med tænd / sluk");
                }
            }
            else
            {
                Console.WriteLine("Opret en bil i menu 1 først..");
            }
            MenuReturn();
        }

        static void DriveCar()
        {
            Console.Clear();
            if (userCar != null)
            {
                Trip newTrip = CreateTrip();
                if (newTrip != null)
                {
                    if (newTrip.CalculateTripPrice(userCar) > 0)
                    {
                        userCar.Drive(newTrip);
                        Console.WriteLine($"Du har kørt {newTrip.Distance}km. Nyt kilometertal: {userCar.Odometer}km");

                        // Opdaterer bilen i listen, hvis modelnavnet matcher, opdateres den gamle bil i listen med userCar
                        cars = cars.Select(car => car.Model == userCar.Model ? userCar : car).ToList();

                        datahandler.AddCarsAndTrips(cars); // Gemmer opdaterede data i filen
                    }
                }
                              
            }
            else
            {
                Console.WriteLine("Vælg en bil i 'adminstrer bil' først..");
            }
            MenuReturn();
        }

        static void CheckOdometerPalindrome()
        {
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
        }

        static void PrintCarDetails()
        {
            Console.Clear();
            if (cars.Count == 0)
            {
                Console.WriteLine("Der er ingen biler i databasen");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Biler fundet i databasen: {cars.Count}\n");
                Console.ResetColor();
                for (int i = 0; i < cars.Count; i++)
                {
                    cars[i].PrintCar(i == 0);
                }
            }
            MenuReturn();
        }

        static void PrintTripDetails()
        {
            Console.Clear();
            PrintColoredMenu();
            
            PrintColoredOption(1, "Se ture på den valgte bil");
            PrintColoredOption(2, "Søg tur med dato");
            PrintColoredOption(3, "Søg efter alle ture");
            PrintColoredOption(4, "Søg efter alle ture på en bil");
            PrintColoredOption(5, "Gå tilbage");
            ConsoleKeyInfo tripChoice;
            tripChoice = Console.ReadKey();
            switch (tripChoice.KeyChar)
            {
                case '1':
                    PrintTripsForSelectedCar();
                    break;
                case '2':
                    SearchTripsByDate();
                    break;
                case '3':
                    PrintAllTripsInDatabase();
                    break;
                case '4':
                    PrintTripsForSpecificCar();
                    break;
                default:
                    break;
            }
        }

        static void PrintTripsForSelectedCar()
        {
            Console.Clear();
            if (userCar != null)
            {
                for (int i = 0; i < userCar.Trips.Count; i++)
                {
                    userCar.Trips[i].PrintTripDetails(userCar, i == 0);
                }
            }
            else
            {
                Console.WriteLine("Du har ikke valgt en bil..");
            }
            MenuReturn();            
        }

        static void SearchTripsByDate()
        {
            //Skal evt ændres så den kan finde en trip på søgt dato, på alle car trips - ikke kun det valgte objekt.
            Console.Clear();
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
            MenuReturn();
        }

        static void PrintAllTripsInDatabase()
        {
            //bool first sørger for overskrift kun printes en gang
            Console.Clear();
            bool anyTrips = false;
            bool first = true;

            foreach (Car car in cars)
            {
                if (car.Trips.Count > 0)
                {
                    anyTrips = true;

                    for (int i = 0; i < car.Trips.Count; i++)
                    {
                        car.Trips[i].PrintTripDetails(car, first);
                        first = false;
                    }
                }
            }
            if (!anyTrips)
            {
                Console.WriteLine("Ingen ture blev fundet");
            }
            MenuReturn();
        }

        static void PrintTripsForSpecificCar()
        {
            Console.Clear();
            CarList();
            Console.Write("\nVælg hvilken model du vil se ture på: ");
            string modelChoice = Console.ReadLine();
            Console.Clear();

            Car carTrips = ChooseCar(cars, modelChoice);

            for (int i = 0; i < carTrips.Trips.Count; i++)
            {
                carTrips.Trips[i].PrintTripDetails(carTrips, i == 0);
            }
            MenuReturn();
        }

        static void MenuReturn()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nEnter for at se menuen..");
            Console.ResetColor();
            Console.ReadLine();
            Console.Clear();
        }

        static void PrintColoredOption(int option, string text)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"[{option}] ");  // Brackets rundt om nummeret

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(text);

            Console.ResetColor();
        }
        static void PrintColoredMenu()
        {

            Console.WriteLine("╔══════════════════════════════════════════════╗");
            Console.Write("║              ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Vælg en mulighed:");
            Console.ResetColor();
            Console.WriteLine("               ║");
            Console.WriteLine("╚══════════════════════════════════════════════╝");

        }

        //Metode til at lave en bil
        static Car CreateCar(Datahandler datahandler)
        {
            Car newCar = null;
            while (newCar == null)
            {
                try
                {
                    Console.WriteLine("\n|---- OPRET NY BIL ----|");
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
                        default: Console.WriteLine("Ugyldigt valg, prøv igen!"); break;
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

        //Metode til at lave en tur
        static Trip CreateTrip()
        {
            try
            {
                Console.Write("\nIndtast antal kilometer for turen: ");
                double distance = Convert.ToDouble(Console.ReadLine());

                Console.Write("Indtast dato for turen (dd/MM/yyyy): ");
                DateTime tripDate = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);

                Console.Write("Indtast starttidspunkt (HH:mm): ");
                DateTime startTime = DateTime.ParseExact(Console.ReadLine(), "HH:mm", null);
                startTime = tripDate.Date + startTime.TimeOfDay; // Kombiner dato og tid

                Console.Write("Indtast sluttidspunkt (HH:mm): ");
                DateTime endTime = DateTime.ParseExact(Console.ReadLine(), "HH:mm", null);
                endTime = tripDate.Date + endTime.TimeOfDay; // Kombiner dato og tid

                return new Trip(distance, tripDate, startTime, endTime);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Fejl: Forkert format på input. Trip ikke oprettet.");
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine("Fejl: Du har ikke indtastet en værdi. Trip ikke oprettet");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ukendt fejl: {ex.Message}");
            }
            return null;
        }

        static bool Palindrom(double odometer)
        {
            string kilometerStr = odometer.ToString();
            string reverseStr = new string(kilometerStr.Reverse().ToArray());

            return kilometerStr == reverseStr;
        }

        //Display en simpel liste af cars fra listen.
        static void CarList()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Følgende biler findes i databasen\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Cyan;
            string infoHeader = string.Format("{0,-15} {1,-15} {2,-10}", "Mærke", "Model", "Årgang");
            Console.WriteLine(infoHeader);

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(new string('-', 45));

            Console.ForegroundColor = ConsoleColor.White;
            foreach (Car car in cars)
            {
                string printInfo = string.Format("{0,-15} {1,-15} {2,-10}", car.Brand, car.Model, car.Year);
                Console.WriteLine(printInfo);
            }
            Console.ResetColor();
        }

        //Søg efter bil i liste metode. Behøver i princip kun foreach loopet til det.
        static Car ChooseCar(List<Car> cars, string modelPick)
        {
            Car searchCar = null;
            while (searchCar == null)
            {
                try
                {
                    if (cars.Count == 0)
                    {
                        throw new Exception("Der er ingen biler i databasen");
                    }

                    foreach (Car car in cars)
                    {
                        if (car.Model == modelPick)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Du har valgt: {car.Brand} {car.Model}\n");
                            Console.ResetColor();
                            searchCar = car;
                            break;
                        }
                    }

                    if (searchCar == null)
                    {
                        Console.Clear();
                        throw new Exception("Ingen bil med den model blev fundet");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Prøv venligst igen.");
                    CarList();
                    Console.Write("\nVælg hvilken model du vil se ture på: ");
                    modelPick = Console.ReadLine();
                    Console.Clear();
                }
            }
            return searchCar;
        }

    }
}
