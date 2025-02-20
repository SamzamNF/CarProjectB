# CarProjektBeta - README  

## Overview  
CarProjektBeta is a simple C# console application that allows users to manage car information, simulate driving trips, calculate trip costs, and check for special conditions like odometer palindromes.  

## Features  
- View default car details  
- Input custom car details  
- Simulate a drive by entering a distance  
- Calculate trip costs based on fuel type and efficiency  
- Check if the odometer reading is a palindrome  
- Print car details in a formatted table  
- Display a list of team members' cars  

## How to Use  
1. **Run the program** in a C# environment (e.g., Visual Studio).  
2. **Choose an option from the menu** by entering a number (1-8).  
3. **Follow the prompts** to enter details or retrieve information.  
4. **Press Enter** to return to the main menu after completing an action.  
5. **Exit the program** by selecting option 8.  

## Menu Options  
1. **View default car details** – Displays information about a pre-defined car.  
2. **Enter car details** – Allows the user to input their own car's information.  
3. **Drive a trip** – Updates the odometer by adding a user-specified distance.  
4. **Calculate trip cost** – Asks for fuel type and computes the cost of the trip.  
5. **Check for odometer palindrome** – Determines if the odometer reading is the same forward and backward.  
6. **Print car details** – Displays the stored details of both the default and user-input cars.  
7. **Show team cars** – Displays a table of cars assigned to team members.  
8. **Exit the program** – Ends the application.  

## Requirements  
- .NET framework (C# compatible runtime)  
- Console input and output functionality  

## Notes  
- The program only supports fuel types **Benzin** and **Diesel** for trip cost calculations.  
- Odometer palindrome detection works by reversing the number and comparing it.  
- The application uses simple string formatting to display car data in tables.  

## Future Improvements  
- Store multiple cars dynamically instead of using static variables.  
- Improve fuel price accuracy with real-time data retrieval.  
- Enhance UI with graphical elements for better user experience.  

---

This project is a learning exercise in C# and console applications. Feel free to modify or extend its functionality! 🚗