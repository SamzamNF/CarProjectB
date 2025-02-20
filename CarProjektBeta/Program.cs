using System;
using System.Linq;

namespace CarProjektBeta
{
    internal class Program
    {

        static string inputBrand;
        static string inputModel;
        static int inputYear;
        static char inputGeartype;
        static double inputKilometer;
        static string powerSource;
        static double inputKmPerLiter;
        static double distance;

        static void Main(string[] args)
        {

            // Oprettelse af menu
            int choice;
            do
            {
                Console.WriteLine("\nVælg funktion: ");
                Console.WriteLine("1. Læs standardbils oplysninger");
                Console.WriteLine("2. Indtast biloplysninger");
                Console.WriteLine("3. Indtast detaljer til at køre en tur");
                Console.WriteLine("4. Tjek prisen på din tur");
                Console.WriteLine("5. Tjek om odometeret er et palindrom");
                Console.WriteLine("6. Print bilernes oplysninger");
                Console.WriteLine("7. Print gruppens biler");
                Console.WriteLine("8. Afslut programmet");

                choice = Convert.ToInt32(Console.ReadLine());

                // Menu valgmuligheder
                switch (choice)
                {
                    case 1:
                        CarDetails();
                        break;
                    case 2:
                        ReadCarDetails();
                        break;
                    case 3:
                        Console.Write("Enter distance to drive: ");
                        distance = Convert.ToDouble(Console.ReadLine());
                        Drive(distance);
                        break;
                    case 4:
                        if (distance == 0)
                        {
                            Console.Write("\nHvor lang er turen på i kilometer? ");
                            distance = Convert.ToDouble(Console.ReadLine());
                        }
                        Console.WriteLine($"Din tur har kostet: {TripCost(distance):F2}DKK");
                        break;
                    case 5:
                        if (Palindrom(inputKilometer))
                        {
                            Console.WriteLine("\nOdometeret er et palindrom!");
                        }
                        else
                        {
                            Console.WriteLine("Odometeret er ikke et palindrom!");
                        }
                        break;
                    case 6:
                        PrintCarDetails();
                        break;
                    case 7:
                        PrintTeamCarTabel();
                        break;
                    case 8:
                        Console.WriteLine("Afslutter programmet...");
                        break;
                    default:
                        Console.WriteLine("Ugyldigt valg, prøv igen.");
                        break;
                }

            } while (choice != 8);  // Sørg for at afslutte når valget er 6
        }

        static void CarDetails()
        {
            string brand = "Toyota";
            string model = "Corolla";
            int year = 2020;
            char gearType = 'A';
            char powerSource = 'D';
            int kilometerT = 23211;
            double kmPerLiterT = 19.5;

            Console.WriteLine("Bilens mærke er: " + brand);
            Console.WriteLine("Bilens model er: " + model);
            Console.WriteLine("Bilens årgang er: " + year);
            Console.WriteLine("Bilens geartype er: " + gearType);
            Console.WriteLine("Bilens drivmiddel er: " + powerSource);
            Console.WriteLine("Bilen har kørt: " + kilometerT);
            Console.WriteLine("Bilen kører: " + kmPerLiterT + " på literen");

            Console.Write("\nTryk på Enter for at gå tilbage til menuen...");
            Console.ReadLine();
        }
        static void ReadCarDetails()
        {
            Console.Write("\nIndtast bilens mærke: ");
            inputBrand = Console.ReadLine();
            Console.Write("Indtast bilens model: ");
            inputModel = Console.ReadLine();
            Console.Write("Indtast bilens årgang: ");
            inputYear = Convert.ToInt32(Console.ReadLine());
            Console.Write("Indtast bilens geartype(A for automat, M for manual): ");
            inputGeartype = Console.ReadLine()[0];
            Console.Write("Indtast bilens kilometer: ");
            inputKilometer = Convert.ToDouble(Console.ReadLine());
            Console.Write("Hvor mange km/l kører din bil? ");
            inputKmPerLiter = Convert.ToDouble(Console.ReadLine());

            Console.Write("\nTryk på Enter for at gå tilbage til menuen...");
            Console.ReadLine();
        }


        static void Drive(double distance)
        {
            Console.Write("\nEr bilen tændt? ");
            string svar = Console.ReadLine().ToLower();

            bool isEngineOn = svar == "ja" || svar == "yes" || svar == "yep" || svar == "ye" || svar == "yeh" || svar == "sure";
            if (isEngineOn)                                                         // True hvis man bruger en af keywords. || = Eller
            {
                Console.Write("Godt, så er er du klar til at komme afsted");

                inputKilometer += distance;                                         // += = at man tager værdien på venstre sider og +'er på højre side
                Console.WriteLine($"\nDit nye kilometertal er {inputKilometer}km");
            }
            else
            {
                Console.Write("\nDu må starte bilen først..");
            }

            Console.Write("\nTryk på Enter for at gå tilbage til menuen...");
            Console.ReadLine();
        }

        static double TripCost(double distance)
        {
            Console.Write("\nHvilken type brændstof bruger din bil? Benzin eller Diesel? ");
            powerSource = Console.ReadLine().ToLower();
            double fuelPrice = 0;


            if (powerSource == "benzin")
            {
                fuelPrice = 13.49;
                Console.WriteLine("\nModtaget, din bil kører altså på benzin til en pris på 13.49kr per liter");
            }
            else if (powerSource == "diesel")
            {
                fuelPrice = 12.29;
                Console.WriteLine("\nModtaget, din bil kører altså på diesel til en pris på 12.29kr per liter");
            }
            else
            {
                Console.WriteLine("\nUgyldt, du skal vælge mellem benzin eller diesel!");
            }
            
            Console.Write("\nTryk på Enter for at gå tilbage til menuen...");
            Console.ReadLine();

            return (distance / inputKmPerLiter) * fuelPrice;

        }
        static bool Palindrom(double inputKilometer)
        {
            string kilometerStr = inputKilometer.ToString();
            string reverseStr = new string(kilometerStr.Reverse().ToArray());   // Vender den nydannede streng om (ToArray = man laver den samlede "tekst" om til enkelte bogstaver / tal, det er nødvendigt for at vende den om)

            Console.Write("\nTryk på Enter for at gå tilbage til menuen...");
            Console.ReadLine();

            return kilometerStr == reverseStr;                                  // Tjekker om den omvendte streng er = den gamle.
          
        }

        static void PrintCarDetails()
        {
            // Oplysninger om en standardbil
            string brand = "Toyota";
            string model = "Corolla";
            int year = 2020;
            int kilometerT = 23211;
            double kmPerLiterT = 19.5;
            string powerSourceT = "Diesel";

            string infoHeader = String.Format("\n|{0,-10}|{1,-10}|{2,-10}|{3,-10}|", "Bilmærke", "Model", "Odometer", "Km/l", "Brændstofstype", "Årgang");
            string infoCar = String.Format("\n|{0,-10}|{1,-10}|{2,-10}|{3,-10}|", brand, model, kilometerT, kmPerLiterT, powerSourceT, year);
            string infoUserCar = String.Format("\n|{0,-10}|{1,-10}|{2,-10}|{3,-10}|", inputBrand, inputModel, inputKilometer, inputKmPerLiter, powerSource, inputYear);
            string line = String.Format("\n____________________________________________");

            Console.WriteLine(infoHeader + line + infoCar + infoUserCar);

            Console.WriteLine($"Din tur har kostet på {distance}km {TripCost(distance):F2}DKK");

            Console.Write("\nTryk på Enter for at gå tilbage til menuen...");
            Console.ReadLine();
        }

        static void PrintTeamCarTabel()
        {
            // Teammedlemmers oplysninger bliver oprettet i arrays:
            // Jeg laver min tabel med infoHeader + line
            // De forskellige variabler der er lavet til Arrays, bliver smidt ind i for-loopen når de bliver kaldet på. Vi ser fx brand[i]
            // når de alle er 0, så vælger den fx Aymans række og sætte ind, og derefter printer den tabellen. Derefter siger i++ at den ligger 1 til,
            // så den vælger altsi brand[1] i stedet for brand[0], og det samme med de andre variabler. 
            // Den fortsætter indtil den er nået længden af arrayet "name". Altså den tæller fra 0..1..2..3..4 og så stop. Array starter altid ved 0

            string[] name = { "Ayman", "Freja", "Alex", "Steffen", "Laura" };
            string[] brand = { "Audi", "Tesla", "Honda", "VW", "Fiat" };
            string[] model = { "A8", "Model 3", "Civic", "Golf", "500" };
            string[] odometer = { "5212", "12345", "9876", "95500", "83121" };
            string[] years = { "2020", "2021", "2016", "2013", "2014" };

            string infoHeader = String.Format("\n|{0,-10}|{1,-10}|{2,-10}|{3,-10}|{4,-10}|", "Navn", "Bilmærke", "Model", "Odometer", "Årgang");
            string line = String.Format("\n_______________________________________________________");

            Console.WriteLine(infoHeader + line);

            for (int i = 0; i < name.Length; i++)
            {
                string printTeam = string.Format("\n|{0,-10}|{1,-10}|{2,-10}|{3,-10}|{4,-10}|", name[i], brand[i], model[i], odometer[i], years[i]);

                Console.WriteLine(printTeam);
            }

            Console.Write("\nTryk på Enter for at gå tilbage til menuen...");
            Console.ReadLine();
        }



    }

}