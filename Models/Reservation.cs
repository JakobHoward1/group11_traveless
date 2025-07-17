using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace group11_traveless.Models
{
    using System.Text.Json.Serialization;


    public class Reservation
    {
        public string Code { get; set; }

        [JsonIgnore]
        public Flight Flight { get; set; }
        public string FlightCode { get; set; }
        public string TravelerName { get; set; }
        public string Citizenship { get; set; }
        public bool IsActive { get; set; } = true;

        public override string ToString() =>
            $"{Code} - {TravelerName} | {Flight?.Code} | {(IsActive ? "Active" : "Inactive")}";
    }
}

