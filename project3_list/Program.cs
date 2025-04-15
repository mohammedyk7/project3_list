// Simple Airline Booking Console App for Beginners (Using Lists)
using System;
using System.Collections.Generic;

class Program
{
    // lists instead of the array 
    static List<string> flightCodes = new List<string>();
    static List<string> fromCities = new List<string>();
    static List<string> toCities = new List<string>();
    static List<string> departureTimes = new List<string>();
    static List<int> durations = new List<int>();
    static List<string> bookingIDs = new List<string>();
    static List<string> passengerNames = new List<string>();
    static List<string> bookedFlightCodes = new List<string>();

    static void Main(string[] args)
    {
        AddFlight("Default001", "Muscat", "Salalah", "2025-05-01 14:00", 90);
        DisplayWelcomeMessage();

        while (true)
        {
            int choice = ShowMainMenu();
            if (choice == 1)
            {
                Console.Write("Enter your name: ");
                string? name = Console.ReadLine();
                Console.Write("Enter flight code (or leave blank for default): ");
                string? code = Console.ReadLine(); //i added "?"
                BookFlight(name, string.IsNullOrWhiteSpace(code) ? "Default001" : code);
            }
            else if (choice == 2)
            {
                Console.Write("Enter booking ID to cancel: ");
                string id = Console.ReadLine();
                CancelBooking(id);
            }
            else if (choice == 3)
            {
                DisplayFlights();
            }
            else if (choice == 4)
            {
                Console.WriteLine("Thank you! Goodbye.");
                break;
            }
            else
            {
                Console.WriteLine("Invalid option.");
            }
        }
    }

    static void DisplayWelcomeMessage()
    {
        Console.WriteLine("\n=== Welcome to Easy Air ===\n");
    }

    static int ShowMainMenu()
    {
        Console.WriteLine("1. Book Flight");
        Console.WriteLine("2. Cancel Booking");
        Console.WriteLine("3. View Flights");
        Console.WriteLine("4. Exit");
        Console.Write("Choose an option: ");
        int.TryParse(Console.ReadLine(), out int choice);
        return choice;
    }

    static void AddFlight(string code, string from, string to, string time, int duration)
    {
        flightCodes.Add(code);
        fromCities.Add(from);
        toCities.Add(to);
        departureTimes.Add(time);
        durations.Add(duration);
    }

    static void DisplayFlights()
    {
        for (int i = 0; i < flightCodes.Count; i++)
        {
            Console.WriteLine($"{flightCodes[i]}: {fromCities[i]} -> {toCities[i]} at {departureTimes[i]} for {durations[i]} mins");
        }
    }

    static bool ValidateFlightCode(string code)
    {
        return flightCodes.Contains(code);
    }

    static void BookFlight(string name, string code)
    {
        if (!ValidateFlightCode(code))
        {
            Console.WriteLine("Invalid flight code.");
            return;
        }

        string bookingID = name + DateTime.Now.Ticks.ToString().Substring(10);
        bookingIDs.Add(bookingID);
        passengerNames.Add(name);
        bookedFlightCodes.Add(code);
        Console.WriteLine($"Booking successful! Your ID is: {bookingID}");
    }

    static void CancelBooking(string? id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            Console.WriteLine("Invalid booking ID.");
            return;
        }

        int index = bookingIDs.IndexOf(id);
        if (index >= 0)
        {
            Console.WriteLine($"Booking for {passengerNames[index]} canceled.");
            bookingIDs.RemoveAt(index);
            passengerNames.RemoveAt(index);
            bookedFlightCodes.RemoveAt(index);
        }
        else
        {
            Console.WriteLine("Booking not found.");
        }
        static string GenerateBookingID(string name)
        {
            return name + DateTime.Now.Ticks.ToString().Substring(10);
        }

        static void SearchBookingsByDestination(string destination)
        {
            Console.WriteLine($"\nBookings to {destination}:");
            for (int i = 0; i < bookedFlightCodes.Count; i++)
            {
                int flightIndex = flightCodes.IndexOf(bookedFlightCodes[i]);
                if (flightIndex >= 0 && toCities[flightIndex].Equals(destination, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"{passengerNames[i]} (Booking ID: {bookingIDs[i]})");
                }
            }
        }

        static void UpdateFlightTime(string code)
        {
            int index = flightCodes.IndexOf(code);
            if (index >= 0)
            {
                Console.Write("Enter new departure time (yyyy-MM-dd HH:mm): ");
                string newTime = Console.ReadLine();
                departureTimes[index] = newTime;
                Console.WriteLine("Flight time updated.");
            }
            else
            {
                Console.WriteLine("Flight not found.");
            }
        }
    }

}

