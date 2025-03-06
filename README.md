# CarProjektBeta - README

## Overview
CarProjektBeta is a simple C# console application that allows users to manage car information, simulate driving trips, calculate trip costs, and check for special conditions like odometer palindromes.


## Features
- Input custom car details
- Simulate a drive by entering a distance
- Calculate trip costs based on fuel type and efficiency
- Check if the odometer reading is a palindrome
- Print car details in a formatted table
- Display a list of team members' cars


## How to Use
1. Run the program in a C# environment (e.g., Visual Studio).
2. Choose an option from the menu by entering a number (1-8).
3. Follow the prompts to enter details or retrieve information.
4. Press Enter to return to the main menu after completing an action.
5. Exit the program by selecting option 8.


## Menu Options
1. **Enter car details** – Input your car's information.
2. **Turn the engine on/off** – Toggle the car engine.
3. **Enter distance for a drive** – Simulate a drive by entering a distance.
4. **Calculate trip cost** – Calculate the cost of a trip based on fuel type and efficiency.
5. **Check if odometer is a palindrome** – Check if the car's odometer reading is a palindrome.
6. **Print car details** – Display the details of the car you created.
7. **Print team cars** – Display a list of cars assigned to team members.
8. **Exit the program** – Exit the application.


## Requirements
- .NET framework (C# compatible runtime)
- Console input and output functionality


## Notes
- The program supports only two fuel types for trip cost calculations: **Benzin** and **Diesel**.
- Odometer palindrome detection works by reversing the number and comparing it.
- The program uses string formatting to display car data in tables.


## Future Improvements
- **Dynamic car storage**: Store multiple cars dynamically instead of using static variables.
- **Fuel price accuracy**: Improve fuel price accuracy with real-time data retrieval.
- **UI Enhancement**: Enhance the user interface with graphical elements for better user experience.