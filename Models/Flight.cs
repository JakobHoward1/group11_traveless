using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace group11_traveless.Models
{

    public class Flight
    {
        public string Code { get; set; }
        public string Airline { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string Day { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public decimal Cost { get; set; }
        public int AvailableSeats { get; set; }

        public override string ToString() =>
            $"{Code} - {Airline} | {Origin} to {Destination} | {Day} {DepartureTime:hh\\:mm} | CAD {Cost}";
    }
}

