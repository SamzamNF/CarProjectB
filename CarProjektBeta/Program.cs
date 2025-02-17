using System;
using System.Linq;

namespace CarProjektBeta
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string brandUser = "", modelUser = "", pSource = "";
            int yearUser = 0;
            char gearTypeUser = ' ';
            double kmPerLiter = 0, fuelPrice = 0, kilometerDriven = 0, distance = 0;
            double fuelNeeded = distance / kmPerLiter, tripCost = fuelNeeded * fuelPrice;

            // Menu
            int choice;
            do
            {
                Console.WriteLine("\nVælg funktion: ");
                Console.WriteLine("1. Læs base biloplysninger");
                Console.WriteLine("2. Indtast biloplysninger: ");
                Console.WriteLine("3. Kør tur: ");
                Console.WriteLine("4. Beregn turens pris: ");
                Console.WriteLine("5. Tjek om odometer er et palindrom: ");
                Console.WriteLine("6. Udskriv alle brugerens biloplysninger: ");
                Console.WriteLine("7. Udskriv tabel af samlet biloplysninger");
                Console.WriteLine("8. Udskriv gruppens biloplysninger");
                Console.WriteLine("9. Afslut");


                choice = Convert.ToInt32(Console.ReadLine());

                // Valgmuligheder i menu
                switch (choice)
                {
                    case 1:
                        ReadCarDetails();
                        break;
                    case 2:
                        // Indlæs brugerens biloplysninger
                        (brandUser, modelUser, yearUser, gearTypeUser, kilometerDriven) = ReadUserCarDetails();
                        break;
                    case 3:
                        (kilometerDriven, distance) = Drive(kilometerDriven);
                        break;
                    case 4:
                        (tripCost, pSource, fuelPrice, fuelNeeded) = TripCost(distance, fuelPrice, kmPerLiter, pSource);
                        break;
                    case 5:
                        bool isPalindrome = Palindrom(kilometerDriven);
                        break;
                    case 6:
                        PrintUserCarDetails(brandUser, modelUser, pSource, yearUser,
                        kmPerLiter, fuelPrice, kilometerDriven, distance, tripCost, fuelNeeded);
                        break;
                    case 7:
                        PrintCarTable(brandUser, modelUser, kilometerDriven, yearUser);
                        break;
                    case 8:
                        PrintTeamCarTabel();
                        break;
                }
            }
            while (choice != 9);
        }

        static void ReadCarDetails()
        {
            // Oplysninger om en standardbil
            string brand = "Toyota";
            string model = "Corolla";
            int year = 2020;
            char gearType = 'A';
            char powerSource = 'D';
            int kilometerT = 23211;
            double kmPerLiterT = 19.5;

            Console.WriteLine("\nBilens mærke er: " + brand);
            Console.WriteLine("Bilens model er: " + model);
            Console.WriteLine("Bilens årgang er: " + year);
            Console.WriteLine("Bilens geartype er: " + gearType);
            Console.WriteLine("Bilens drivmiddel er: " + powerSource);
            Console.WriteLine("Bilen har kørt: " + kilometerT + "km");
            Console.WriteLine("Bilen kører: " + kmPerLiterT + " på literen");

            Console.Write("\nTryk på Enter for at gå tilbage til menuen...");
            Console.ReadLine();
        }

        static (string, string, int, char, double) ReadUserCarDetails()
        {
            // Indhenter brugerens biloplysninger og returner dem
            Console.Write("\nIndtast bilens mærke: ");
            string brandUser = Console.ReadLine();

            Console.Write("Indtast bilens model: ");
            string modelUser = Console.ReadLine();

            Console.Write("Indtast bilens årgang: ");
            int yearUser = Convert.ToInt32(Console.ReadLine());

            Console.Write("Indtast bilens geartype (A for automat, M for manual): ");
            char gearTypeUser = Console.ReadLine()[0];

            Console.Write("Hvor mange kilometer har din bil kørt i alt? ");
            double kilometerDriven = Convert.ToInt32(Console.ReadLine());

            Console.Write("\nTryk på Enter for at gå tilbage til menuen...");
            Console.ReadLine();

            return (brandUser, modelUser, yearUser, gearTypeUser, kilometerDriven);
        }

        static (double, double) Drive(double kilometerDriven)
        {
            Console.Write("\nDu har valgt at køre en tur, lad os se om du er klar!");

            Console.Write("\nEr bilen tændt? ");
            string svar = Console.ReadLine().ToLower();

            bool isEngineOn = svar == "ja" || svar == "yes" || svar == "yep" || svar == "ye" || svar == "yeh" || svar == "sure";

            double distance = 0;

            if (isEngineOn)  // True hvis man bruger en af keywords. || = Eller
            {
                Console.Write("Godt, så er er du klar til at komme afsted");

                Console.Write("\nHvor lang er turen på i kilometer? ");
                distance = Convert.ToDouble(Console.ReadLine());

                kilometerDriven += distance;  // += = at man tager værdien på venstre sider og +'er på højre side
                Console.WriteLine($"Dit nye kilometertal er {kilometerDriven}km");

            }
            else
            {
                Console.Write("\nDu må starte bilen først..");
            }
            Console.Write("\nTryk på Enter for at gå tilbage til menuen...");
            Console.ReadLine();

            return (kilometerDriven, distance);
        }

        static (double tripCost, string pSource, double fuelPrice, double fuelNeeded) TripCost(double distance, double fuelPrice, double kmPerLiter, string pSource)
        // Det til venstre er det som bliver returnet. Det til højre er det vi kalder på ind i metoden.
        {
            if (distance == 0)
            {
                Console.Write("\nHvor lang er turen på i kilometer? ");
                distance = Convert.ToDouble(Console.ReadLine());
            }

            Console.Write("Hvor mange km/l kører din bil tror du? ");
            kmPerLiter = Convert.ToDouble(Console.ReadLine());

            Console.Write("Hvilken type brændstof bruger din bil? Benzin eller Diesel? ");
            pSource = Console.ReadLine().ToLower();

            if (pSource == "benzin")
            {
                fuelPrice = 13.49;
                Console.WriteLine("\nModtaget, din bil kører altså på benzin til en pris på 13.49kr per liter");
            }
            else if (pSource == "diesel")
            {
                fuelPrice = 12.29;
                Console.WriteLine("\nModtaget, din bil kører altså på diesel til en pris på 12.29kr per liter");
            }
            else
            {
                Console.WriteLine("\nUgyldt, du skal vælge mellem benzin eller diesel!");
            }
            double fuelNeeded = distance / kmPerLiter, tripCost = fuelNeeded * fuelPrice;
            Console.WriteLine($"Din tur på {distance}km har altså kostet {tripCost:F2}DKK");

            Console.Write("\nTryk på Enter for at gå tilbage til menuen...");
            Console.ReadLine();

            return (tripCost, pSource, fuelPrice, fuelNeeded);

            // Sender ikke kilometer / distance tilbage til main her. Blot for at programmet ikke skal gå i stå hvis man ikke har indtastet det.
        }

        static bool Palindrom(double kilometerDriven)
        {
            string kilometerStr = kilometerDriven.ToString();

            string reverseStr = new string(kilometerStr.Reverse().ToArray());  // Vender den nydannede streng om (ToArray = man laver den samlede "tekst" om til enkelte bogstaver / tal, det er nødvendigt for at vende den om)

            bool isPalindrome = kilometerStr == reverseStr;                    // Tjekker om den omvendte streng er = den gamle.

            if (isPalindrome)
            {
                Console.WriteLine("\nOdometeret er et palindrom");
            }
            else
            {
                Console.WriteLine("\nOdometeret er ikke et palindrom");
            }
            Console.Write("\nTryk på Enter for at gå tilbage til menuen...");
            Console.ReadLine();

            return isPalindrome;
        }

        static void PrintUserCarDetails(string brandUser, string modelUser, string pSource, int yearUser,
        double kmPerLiter, double fuelPrice, double kilometerDriven, double distance, double tripCost, double fuelNeeded)
        {
            Console.WriteLine("\n--- Biloplysninger ---");
            Console.WriteLine($"Mærke: {brandUser}");
            Console.WriteLine($"Model: {modelUser}");
            Console.WriteLine($"Årgang: {yearUser}");
            Console.WriteLine($"Brændstoftype: {pSource}");
            Console.WriteLine($"Kilometertal: {kilometerDriven}km");
            Console.WriteLine($"Km/l: {kmPerLiter}");

            Console.WriteLine("\n--- Turoplysninger ---");
            Console.WriteLine($"Turens distance: {distance}km");
            Console.WriteLine($"Turns pris: {tripCost}DKK");
            Console.WriteLine($"Brændstofsprisen pr liter: {fuelPrice:F2}DKK");
            Console.WriteLine($"Brændstof brugt: {fuelNeeded:F2}l");

            Console.Write("\nTryk på Enter for at gå tilbage til menuen...");
            Console.ReadLine();
        }

        static void PrintCarTable(string brandUser, string modelUser, double kilometerDriven, int yearUser)
        {
            // Oplysninger om en standardbil
            string brand = "Toyota";
            string model = "Corolla";
            int year = 2020;
            int kilometerT = 23211;

            string infoHeader = String.Format("\n|{0,-10}|{1,-10}|{2,-10}|{3,-10}|", "Bilmærke", "Model", "Odometer", "Årgang");
            string infoCar = String.Format("\n|{0,-10}|{1,-10}|{2,-10}|{3,-10}|", brand, model, kilometerT, year);
            string infoUserCar = String.Format("\n|{0,-10}|{1,-10}|{2,-10}|{3,-10}|", brandUser, modelUser, kilometerDriven, yearUser);
            string line = String.Format("\n_____________________________________");

            Console.WriteLine(infoHeader + line + infoCar + infoUserCar);

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