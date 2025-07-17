using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace group11_traveless.Models
{

    public class Airport
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public override string ToString() => $"{Code} - {Name}";
    }
}

