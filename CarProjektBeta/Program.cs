using System.Runtime.Intrinsics.X86;
using System.Text.RegularExpressions;
using CarProjektBeta;

namespace BilProjektBeta
{
    internal class Program
    {
        private static ICarRepository carRepository = new FileCarRepository("cars.txt");
        private static ITripRepository tripRepository = new FileTripRepository("trips.txt");
        private static Car userCar;

        static void Main(string[] args)
        {
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
            PrintColoredOption(5, "Adminstrer ture");
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
                default: Console.WriteLine("Ugyldigt valg, prøv igen"); Thread.Sleep(1500); break;
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
            userCar = CreateCar();
            MenuReturn();
        }

        static void DeleteCar()
        {
            Console.Clear();
            if (carRepository.GetAll().Count == 0)
            {
                Console.WriteLine("Der findes ingen biler i databasen");
            }
            else
            {
                CarList();
                Console.Write("Vælg bil du gerne vil slette med nummerpladen: ");
                string licensePlate = Console.ReadLine();
                carRepository.Delete(licensePlate); // Sletter bilen direkte fra filen
            }
            MenuReturn();
        }

        static void SelectCar()
        {
            Console.Clear();
            CarList();
            PrintColoredTextWrite("\nVælg nummerplade for at bruge bilen: ");
            string licensePlate = Console.ReadLine();
            userCar = ChooseCar(licensePlate);
            MenuReturn();
        }

        
        static void Engine()
        {
            Console.Clear();
            if (userCar != null)
            {
                PrintColoredOption(1, "Tænd motor");
                PrintColoredOption(2, "Sluk motor");
                PrintColoredOption(3, "Gå tilbage");

                ConsoleKeyInfo tripChoice;
                tripChoice = Console.ReadKey();

                switch (tripChoice.KeyChar)
                {
                    case '1':
                        Console.Clear();
                        userCar.ToggleEngine(true);
                        break;
                    case '2':
                        Console.Clear();
                        userCar.ToggleEngine(false);
                        break;
                    default: return;
                }
            }           
            else
            {
                PrintColoredTextWriteLine("Vælg en bil i menuen først..");
                MenuReturn();
            }
            MenuReturn();
        }

        //Skal nok gøre så du får fejl FØR du prøver at køre, i stedet for efter hvis engine ikke er tændt
        static void DriveCar()
        {
            Console.Clear();
            if (userCar != null)
            {
                if (userCar.IsEngineOn == false)
                {
                    Console.WriteLine("Tænd motoren først..");
                    MenuReturn();
                    return;
                }
                //Trip bliver tilføjet ved Drive metoden til listen af trips på bilen i car class
                Trip newTrip = CreateTrip();

                //CalculateTriPrice har en try-cacth der fanger og sender fejlbesked hvis der divideres med 0.
                if (newTrip != null && newTrip.CalculateTripPrice(userCar) > 0)
                {
                    //Drive metoden kaster en fejl hvis moteren ikke er tændt, som catches her.
                    try
                    {
                        userCar.Drive(newTrip);
                        carRepository.Update(userCar);
                        Console.WriteLine($"Du har kørt {newTrip.Distance}km. Nyt kilometertal: {userCar.Odometer}km");

                        tripRepository.Add(newTrip);
                        //Tilføjer turen direkte til trip fil

                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
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
            if (carRepository.GetAll().Count ==  0)
            {
                Console.WriteLine("Der er ingen biler i databasen");
            }
            else
            {
                var cars = carRepository.GetAll();
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
            PrintColoredOption(5, "Slet en tur");
            PrintColoredOption(6, "Gå tilbage");
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
                case '5':
                    DeleteTrip();
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
                var trips = tripRepository.GetAll().Where(t => t.LicensePlate == userCar.LicensePlate).ToList();

                Console.WriteLine($"Ture for den valgte bil: {userCar.LicensePlate}");

                if (trips.Count == 0)
                {
                    Console.WriteLine($"Ingen ture fundet for den valgte bil: {userCar.LicensePlate}");
                    MenuReturn();
                    return;
                }

                for (int i = 0; i < trips.Count; i++)
                {
                    trips[i].PrintTripDetails(userCar, i == 0);
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
            Console.Clear();
            Console.Write("Indtast datoen for at finde ture (dd-MM-yyyy): ");
            DateTime date = Convert.ToDateTime(Console.ReadLine());

            //Henter alle trips med GetAll(), og filtrer så, så trips listen kun gemmer dem der matcher variablen date ved at bruge "Where tripDate objekt = søgte date" linq udtryk.
            List<Trip> trips = tripRepository.GetAll().Where(t => t.TripDate.Date == date.Date).ToList();

            if (!trips.Any())
            {
                Console.WriteLine($"Ingen ture fundet for datoen: {date:dd/MM/yyyy}");
                MenuReturn();
                return;
            }

            Console.WriteLine($"Ture for datoen: {date:dd/MM/yyyy}\n");
            bool isFirst = true;
            foreach (var trip in trips)
            {
                //Finder bilen der matcher med nummerpladen fra trip filen, og sætter car objekt til denne bil så den kan bruge PrintTripDetails metoden som kræver et car objekt
                Car car = carRepository.GetByLicensePlate(trip.LicensePlate);
                if (car != null)
                {
                    trip.PrintTripDetails(car, isFirst);
                    isFirst = false;
                }
            }
            MenuReturn();
        }

        static void PrintAllTripsInDatabase()
        {
            Console.Clear();

            List<Trip> trips = tripRepository.GetAll();
            //Sortere dem så ture alle ture under de enkelte LicensePlates ligger sammen
            trips.Sort((t1, t2) => t1.LicensePlate.CompareTo(t2.LicensePlate));
            if (trips.Count == 0)
            {
                Console.WriteLine("Ingen ture blev fundet");
                MenuReturn();
                return;
            }

            bool isFirst = true;
            foreach (var trip in trips)
            {
                //Finder bilen der matcher med nummerpladen fra trip filen, og laver car objekt til denne bil.
                Car car = carRepository.GetByLicensePlate(trip.LicensePlate);
                if (car != null)
                {
                    trip.PrintTripDetails(car, isFirst);
                    isFirst = false;
                }
            }
            MenuReturn();
        }

        static void PrintTripsForSpecificCar()
        {
            Console.Clear();
            CarList();
            Console.Write("\nVælg hvilken bil du vil se ture på, indtast nummerpladen: ");
            string licensePlate = Console.ReadLine();
            Console.Clear();

            //List<Trip> carTrips = tripRepository.GetAll().Where(t => t.LicensePlate == licensePlate).ToList();
            List<Trip> carTrips = tripRepository.GetByLicensePlate(licensePlate);

            if (carTrips.Count == 0)
            {
                Console.WriteLine($"Ingen ture fundet for bilen med nummerplade: {licensePlate}");
                MenuReturn();
                return;
            }

            Car car = carRepository.GetByLicensePlate(licensePlate);
            if (car == null)
            {
                Console.WriteLine($"Bilen med nummerplade {licensePlate} blev ikke fundet.");
                MenuReturn();
                return;
            }
            Console.WriteLine($"{car.Brand} {car.Model} fundet med {carTrips.Count} tur(e)");
            for (int i = 0; i < carTrips.Count; i++)
            {
                carTrips[i].PrintTripDetails(car, i == 0);
            }
            MenuReturn();
        }
        static void DeleteTrip()
        {
            Console.Clear();
            CarList();
            PrintColoredTextWriteLine("Indtast nummerplade på en trip du vil fjerne:");
            string licensePlate = Console.ReadLine();
            var tripCount = tripRepository.GetByLicensePlate(licensePlate);

            if (string.IsNullOrEmpty(licensePlate) || tripCount.Count == 0)
            {
                PrintColoredTextWriteLine($"Ingen trip blev fundet med nummerpladen: {licensePlate}");                
            }
            else
            {
                tripRepository.Delete(licensePlate);
                PrintColoredTextWriteLine($"Du har slettet en trip fra bilen med nummerpladen: {licensePlate}");
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
        public static void PrintColoredTextWriteLine(string text)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{text}");
            Console.ResetColor();
        }
        public static void PrintColoredTextWrite(string text)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"{text}");
            Console.ResetColor();
        }

        //Metode til at lave en bil
        static Car CreateCar()
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
                    Console.Write("Indtast bilens nummerplade (7 tegn): ");
                    string licensePlate = Console.ReadLine().ToUpper();

                    FuelType fuelSource = FuelType.None;
                    switch (fuelInput)
                    {
                        case "diesel": fuelSource = FuelType.Diesel; break;
                        case "benzin": fuelSource = FuelType.Benzin; break;
                        case "el": fuelSource = FuelType.El; break;
                        case "hybrid": fuelSource = FuelType.Hybrid; break;
                        default: Console.WriteLine("Ugyldigt valg, prøv igen!"); break;
                    }

                    newCar = new Car(brand, model, year, odometer, fuelSource, kmPerLiter, licensePlate);

                    carRepository.Add(newCar);
                    Console.WriteLine("Bilen blev oprettet og gemt i filen.");

                }
                catch (Exception ex) // Fanger alle generelle exceptions - altså fanger ikke en specifik en.
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

                return new Trip(distance, tripDate, startTime, endTime, userCar.LicensePlate);
            }
            //Throw new arguement findes under tripclass og distance property.
            catch (InvalidDistanceException ex)
            {
                Console.WriteLine($"Fejl: {ex.Message}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Fejl: Forkert format på input. Trip ikke oprettet.");
            }
            catch (ArgumentNullException)
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
            string infoHeader = string.Format("{0,-15} {1,-15} {2,-10} {3,-13}", "Mærke", "Model", "Årgang", "Nummerplade");
            Console.WriteLine(infoHeader);

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(new string('-', 55));

            Console.ForegroundColor = ConsoleColor.White;
            foreach (Car car in carRepository.GetAll())
            {
                string printInfo = string.Format("{0,-15} {1,-15} {2,-10} {3,-13}", car.Brand, car.Model, car.Year, car.LicensePlate);
                Console.WriteLine(printInfo);
            }
            Console.ResetColor();
        }

        //Søg efter bil i liste metode. Behøver i princip kun foreach loopet til det.
        static Car ChooseCar(string licensePlate)
        {
            try
            {
                if (carRepository.GetAll().Count == 0)
                {
                    throw new CarNotFoundException("Der er ingen biler i databasen");
                }

                // Find den første bil i listen, hvor modelnavnet matcher 'licensePlate'.
                Car searchCar = carRepository.GetByLicensePlate(licensePlate);

                if (searchCar != null)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\nDu har valgt: {searchCar.Brand} {searchCar.Model}\nNummerplade: {searchCar.LicensePlate}\n");
                    Console.ResetColor();
                    return searchCar;
                }
                else
                {
                    Console.Clear();
                    throw new CarNotFoundException("Ingen bil med den nummerplade blev fundet");
                }
            }
            catch (CarNotFoundException ex)
            {
                Console.Clear();
                Console.WriteLine(ex.Message);
                Console.WriteLine("Du bliver nu sendt tilbage til hovedmenuen...");
                return null;
            }
        }

    }
}
