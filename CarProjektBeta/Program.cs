namespace CarProjektBeta
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Opgave 3

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
            Console.WriteLine("Bilen kører: " + kmPerLiterT + "på literen");



            //Opgave 4

            Console.Write("\nIndtast bilens mærke: ");
            string brandUser = Console.ReadLine();

            Console.Write("Indtast bilens model: ");
            string modelUser = Console.ReadLine();

            Console.Write("Indtast bilens årgang: ");
            int yearUser = Convert.ToInt32(Console.ReadLine());

            Console.Write("Indtast bilens geartype(A for automat, M for manual): ");
            char gearTypeUser = Console.ReadLine()[0];

            Console.WriteLine($"\nDin bils mærke er en {brandUser} {modelUser} fra år {yearUser} med geartype {gearTypeUser}");


            // Opgavesæt 2, Opgave 1 Del 1

            Console.Write("\nHvor mange kilometer har din bil kørt i alt? ");
            int kilometerDriven = Convert.ToInt32(Console.ReadLine());

            Console.Write("Hvor mange km/l kører din bil tror du? ");
            double kmPerLiter = Convert.ToDouble(Console.ReadLine());

            Console.Write("Hvilken type brændstof bruger din bil? Benzin eller Diesel? ");
            string pSource = Console.ReadLine().ToLower();
            double fuelPrice = 0;

            if (pSource == "benzin")
            {
                fuelPrice = 13.49;
                Console.WriteLine("\nModtaget, din bil kører altså på benzin til en pris på 13.49kr per liter");
            }
            else if (pSource == "diesel")
            {
                fuelPrice = 12.29;
                Console.WriteLine("\nModtaget, din bil kører altså på diesel til en pris på 12.29 per liter");
            }
            else
            {
                Console.WriteLine("\nUgyldt, du skal vælge mellem benzin eller diesel!");
            }

            Console.Write("\nGodt, hvis du skulle køre en tur til et sted du havde lyst til at besøge, hvor lang ville turen så være i km? ");
            double distance = Convert.ToDouble(Console.ReadLine());

            double newKilometerDriven = distance + kilometerDriven;
            double fuelNeeded = distance / kmPerLiter;
            double tripCost = fuelNeeded * fuelPrice;

            Console.Write("\nGodt, er du så klar til at komme af sted på turen? ");
            string svar = Console.ReadLine().ToLower();

            while (svar != "ja" && svar != "yes" && svar != "yep" && svar != "ye" && svar != "yeh" && svar != "sure")
            {
                Console.WriteLine("Nå, jamen så må vi jo vente til du er klar. ");
                Console.Write("Er du klar nu til at komme afsted? ");
                svar = Console.ReadLine().ToLower();
            }

            Console.WriteLine("\nSå lad os få et overblik af alle dine oplysninger: ");

            Console.WriteLine("Mærke: " + brandUser);
            Console.WriteLine("Model: " + modelUser);
            Console.WriteLine("Årgang: " + yearUser);
            Console.WriteLine("Geartype: " + gearTypeUser);
            Console.WriteLine("Kilometertal: " + kilometerDriven + "km");
            Console.WriteLine("Km/L: " + kmPerLiter);
            Console.WriteLine("Brændstofstype: " + pSource);
            Console.WriteLine("Pris på " + pSource + " er " + fuelPrice + "kr pr liter");
            Console.WriteLine("Distance på tur: " + distance);
            Console.WriteLine("Turens pris: " + tripCost.ToString("F2"));
            Console.WriteLine("Nyt kilometertal: " + newKilometerDriven);
            Console.WriteLine("Liter brændstof turen kræver: " + fuelNeeded);

            Console.WriteLine(string.Format("\nBrændstofudgifter for at køre {0}km er altså {1:F2} DKK", distance, tripCost));

            Console.WriteLine("\nBilmærke".PadRight(15) + "Model".PadRight(15) + "Kilometertal".PadLeft(10));
            Console.WriteLine("_____________________________________________________________________");
            Console.WriteLine(brand.PadRight(15) + model.PadRight(15) + kilometerT.ToString().PadLeft(5));
            Console.WriteLine(brandUser.PadRight(15) + modelUser.PadRight(15) + newKilometerDriven.ToString().PadLeft(5));

        }
    }
}
