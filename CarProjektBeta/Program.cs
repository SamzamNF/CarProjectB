using CarProjektBeta;

namespace BilProjektBeta
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int choice;
            Car userCar = null;

            do
            {
                Console.WriteLine("\nVælg funktion: ");
                Console.WriteLine("1. Indtast biloplysninger");
                Console.WriteLine("2. Tænd eller sluk moteren");
                Console.WriteLine("3. Indtast detaljer til at køre en tur");
                Console.WriteLine("4. Tjek prisen på din tur");
                Console.WriteLine("5. Tjek om odometeret er et palindrom");
                Console.WriteLine("6. Print bilernes oplysninger");
                Console.WriteLine("7. Print gruppens biler");
                Console.WriteLine("8. Afslut programmet");

                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        userCar = CreateCar();
                        break;
                    case 2:
                        if (userCar != null)
                        {
                            Console.Write("Vil du tænde eller slukke moteren? ");
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
                            Console.Write("Indtast antal kilometer du vil køre: ");
                            double distance = Convert.ToDouble(Console.ReadLine());
                            userCar.Drive(distance);
                        }
                        else
                        {
                            Console.WriteLine("Opret en bil i menu 1 først..");
                        }
                        break;
                    case 4:
                        if (userCar != null)
                        {
                            Console.Write("Indtast antal kilometer for turen: ");
                            double distance = Convert.ToDouble(Console.ReadLine());
                            Console.WriteLine($"Prisen for din tur på {distance} km er {userCar.CalculateTripPrice(distance):F2} DKK.");
                        }
                        else
                        {
                            Console.WriteLine("Opret en bil i menu 1 først..");
                        }
                        break;
                    case 5:
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
                    case 6:
                        if (userCar != null)
                            userCar.PrintCar(true);
                        break;
                    case 7:
                        PrintTeamCarTabel();
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
                    Console.Write("Indtast bilens brændstof (benzin/diesel): ");
                    string fuelSource = Console.ReadLine().ToLower();
                    Console.Write("Hvor mange km/l kører din bil? ");
                    double kmPerLiter = Convert.ToDouble(Console.ReadLine());

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
                new Car("Audi", "A8", 2020, 15212, "benzin", 14.3),
                new Car("BMW", "X5", 2021, 21000, "diesel", 13.5),
                new Car("Honda", "Civic", 2016, 125020, "benzin", 21.3),
                new Car("VW", "Golf", 2013, 95500, "diesel", 17.4),
                new Car("Fiat", "500", 2014, 83121, "benzin", 16.5)
            };


            for (int i = 0; i < teamCars.Length; i++)
            {
                teamCars[i].PrintCar(i == 0);
            }


        }
    }
}
