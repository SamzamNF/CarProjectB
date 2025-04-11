# CarProjektBeta - README

## Overview
CarProjektBeta is a simple C# console application that allows users to manage car information, simulate driving trips, calculate trip costs, and check for special conditions like odometer palindromes.

## Features
- Input custom car details
- Simulate a drive by entering trip details
- Calculate trip costs based on fuel type and efficiency
- Check if the odometer reading is a palindrome
- Print car details in a formatted table
- Display a list of all cars and their trips
- Search for trips by date
- Toggle the car engine on or off

## How to Use
1. Run the program in a C# environment (e.g., Visual Studio).
2. Choose an option from the menu by entering a number (1-6).
3. Follow the prompts to enter details or retrieve information.
4. Press Enter to return to the main menu after completing an action.
5. Exit the program by selecting option 6.

## Menu Options
1. **Administer car** – Add, delete, select, or toggle the engine of a car.
2. **Drive a trip** – Simulate a drive by entering trip details.
3. **Check if odometer is a palindrome** – Check if the car's odometer reading is a palindrome.
4. **Print car details** – Display the details of all cars in the database.
5. **Print trip details** – Display trip details for selected car, search trips by date, or print all trips.
6. **Exit the program** – Exit the application.

## Requirements
- .NET 8 framework
- Console input and output functionality

## Notes
- The program supports multiple fuel types: **Benzin**, **Diesel**, **El**, and **Hybrid**.
- Odometer palindrome detection works by reversing the number and comparing it.
- The program uses string formatting to display car and trip data in tables.
- Trips are stored and loaded from a file named `CarsAndTrips.txt`.

## Future Improvements
- **Fuel price accuracy**: Improve fuel price accuracy with real-time data retrieval.
- **UI Enhancement**: Enhance the user interface with graphical elements for better user experience.
- **Error Handling**: Improve error handling and user input validation.

## Classes and Methods

### Car
- **Attributes**: Brand, Model, Year, FuelSource, FuelPrice, Odometer, KmPerLiter, IsEngineOn, Trips
- **Methods**: 
  - `ToggleEngine(bool value)`
  - `Drive(Trip newTrip)`
  - `PrintCar(bool First = false)`
  - `GetTripsByDate(DateTime date)`
  - `ToString()`
  - `FromString(string data)`

### Trip
- **Attributes**: Distance, TripDate, StartTime, EndTime
- **Methods**: 
  - `CalculateTripPrice(Car car)`
  - `CalculateDuration()`
  - `FuelConsumed(Car car)`
  - `PrintTripDetails(Car car, bool first = false)`
  - `ToString()`
  - `FromString(string data)`

### Datahandler
- **Attributes**: FilePath
- **Methods**: 
  - `AddCarsAndTrips(List<Car> cars)`
  - `LoadCarsAndTrips()`

### Program
- **Methods**: 
  - `Main(string[] args)`
  - `ShowMainMenu()`
  - `PrintMainMenu()`
  - `HandleMenuChoice(char choice)`
  - `CarOptions()`
  - `AddCar()`
  - `DeleteCar()`
  - `SelectCar()`
  - `ToggleEngine()`
  - `DriveCar()`
  - `CheckOdometerPalindrome()`
  - `PrintCarDetails()`
  - `PrintTripDetails()`
  - `PrintTripsForSelectedCar()`
  - `SearchTripsByDate()`
  - `PrintAllTripsInDatabase()`
  - `PrintTripsForSpecificCar()`
  - `MenuReturn()`
  - `PrintColoredOption(int option, string text)`
  - `PrintColoredMenu()`
  - `CreateCar(Datahandler datahandler)`
  - `CreateTrip()`
  - `Palindrom(double odometer)`
  - `CarList()`
  - `ChooseCar(List<Car> cars, string modelPick)`