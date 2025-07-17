using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace group11_traveless.Services
{
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using group11_traveless.Models;



    public static class ReservationManager
    {
        private static List<Reservation> _reservations = new();
        private static readonly string _filePath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "reservations.json");

        private static FlightService _flightService;

        public static void Initialize(FlightService flightService)
        {
            _flightService = flightService;
            LoadReservations(_flightService);

        }

        public static string GenerateReservationCode()
        {
            var random = new Random();
            return $"{(char)('A' + random.Next(0, 26))}{random.Next(1000, 9999)}";
        }

        public static void SaveReservation(Reservation reservation)
        {
            reservation.FlightCode = reservation.Flight?.Code;
            _reservations.Add(reservation);
            Persist();
        }

        public static List<Reservation> FindReservations(string code = null, string airline = null, string name = null)
        {
            return _reservations.Where(r =>
                (string.IsNullOrEmpty(code) || r.Code.Equals(code, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(airline) || r.Flight?.Airline.Equals(airline, StringComparison.OrdinalIgnoreCase) == true) &&
                (string.IsNullOrEmpty(name) || r.TravelerName.Contains(name, StringComparison.OrdinalIgnoreCase))
            ).ToList();
        }

        private static void Persist()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };
            File.WriteAllText(_filePath, JsonSerializer.Serialize(_reservations, options));
        }

        private static void LoadReservations(FlightService _flightService)
        {
            if (!File.Exists(_filePath)) return;

            try
            {
                var json = File.ReadAllText(_filePath);
                var loaded = JsonSerializer.Deserialize<List<Reservation>>(json) ?? new();

                foreach (var res in loaded)
                {
                    res.Flight = _flightService.Flights.FirstOrDefault(f => f.Code == res.FlightCode);
                }
                _reservations = loaded;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading reservations: {ex.Message}");
                _reservations = new();
            }
        }
        public static void SelectFlight(String Code, String Airline, String Day, String Time, String Cost, String Name, String Citizenship)
        {

        }

        public static void SelectReservation(Reservation reservation)
        {
            // Implement reservation editing
        }
    }
}
