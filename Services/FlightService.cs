using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace group11_traveless.Services
{
    using System.Reflection;
    using System.Globalization;
    using group11_traveless.Models;


    public class FlightService
    {
        public List<Flight> Flights { get; } = new();
        public List<Airport> Airports { get; } = new();

        public FlightService()
        {
            LoadAirports();
            LoadFlights();
        }

        private void LoadAirports()
        {
            try
            {
                var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\Data\airports.csv");
                using var stream = File.OpenRead(filePath);
                using var reader = new StreamReader(stream);

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var parts = line?.Split(',');
                    if (parts?.Length == 2)
                    {
                        Airports.Add(new Airport
                        {
                            Code = parts[0].Trim(),
                            Name = parts[1].Trim()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading airports: {ex.Message}");
            }
        }

        public void LoadFlights()
        {
            Console.WriteLine("TESTING");
            try
            {
                var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\Data\flights.csv");
                using var stream = File.OpenRead(filePath);
                using var reader = new StreamReader(stream);

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var parts = line?.Split(',');
                    if (parts?.Length == 8)
                    {
                        Flights.Add(new Flight
                        {
                            Code = parts[0].Trim(),
                            Airline = parts[1].Trim(),
                            Origin = parts[2].Trim(),
                            Destination = parts[3].Trim(),
                            Day = parts[4].Trim(),
                            DepartureTime = TimeSpan.Parse(parts[5].Trim()),
                            AvailableSeats = int.Parse(parts[6].Trim()),
                            Cost = decimal.Parse(parts[7].Trim(), CultureInfo.InvariantCulture)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading flights: {ex.Message}");
            }
        }

        public List<Flight> FindFlights(string origin, string destination, string day)
        {
            return Flights.Where(f =>
                (string.IsNullOrEmpty(origin) || f.Origin == origin) &&
                (string.IsNullOrEmpty(destination) || f.Destination == destination) &&
                (string.IsNullOrEmpty(day) || f.Day.Equals(day, StringComparison.OrdinalIgnoreCase)) &&
                f.AvailableSeats > 0
            ).ToList();
        }
    }
}

